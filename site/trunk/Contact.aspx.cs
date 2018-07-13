using Robo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Contact : System.Web.UI.Page
{
    private const string reCaptchaSecret = "6LcgoVQUAAAAAOC0-qGtIp-qvFcWYEeLAsW0Z8p0";
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (!IsValid)
            return;
        if (!VerifyReCaptcha())
            return;
        if (ContactReason.Text != "")
            return;

        MailMessage m = new MailMessage("contact@kpfanworld.com", "contact@kpfanworld.com");
        m.ReplyToList.Add(new MailAddress(txtEmail.Text));
        m.Subject = "[KPFanWorld] " + txtSubject.Text;
        m.Body = GetBody();
        m.IsBodyHtml = true;

        SmtpClient c = new SmtpClient();
        try
        {
            c.Send(m);
        }
        catch { }

        plhContact.Visible = false;
        plhSuccess.Visible = true;

        //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "formSubmitted", "dataLayer.push({'event': 'formSubmitted', 'category': 'forms', 'action': 'Submission', 'label': 'Contact Form'});", true);
    }

    private string GetBody()
    {
        string val = $"<p><strong>Name:</strong> {txtName.Text}<br />";
        val += $"<strong>Email:</strong> {txtEmail.Text}<br />";
        val += $"<strong>Message:</strong><br />{txtMessage.Text.Nl2Br()}</p><br /><br />";

        val += "--<br />KPFW Staff<br />contact@kpfanworld.com";

        return val;
    }

    private bool VerifyReCaptcha()
    {
        HttpWebRequest req = (HttpWebRequest)WebRequest.Create("https://www.google.com/recaptcha/api/siteverify");
        req.Method = "POST";
        req.ContentType = "application/x-www-form-urlencoded";

        string postData = $"secret={reCaptchaSecret}&response={Request["g-recaptcha-response"]}&remoteip={Request.UserHostAddress}";
        byte[] send = System.Text.Encoding.Default.GetBytes(postData);
        req.ContentLength = send.Length;

        Stream stream = req.GetRequestStream();
        stream.Write(send, 0, send.Length);
        stream.Flush();
        stream.Close();

        HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
        StreamReader sr = new StreamReader(resp.GetResponseStream());
        string returnValue = sr.ReadToEnd();

        Dictionary<string, object> val = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(returnValue);

        return (bool)val["success"];
    }
}