using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using kpfw.Models;
using kpfw.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace kpfw.Controllers
{
    public class ContactController : Controller
    {
        private KpfwSettings Settings { get; }
        public ContactController(IConfiguration configuration)
        {
            Settings = configuration.GetSection("Kpfw").Get<KpfwSettings>();
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(ContactModel m)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/Contact/Index.cshtml", m);
            }

            var grecaptcha = VerifyReCaptcha();
            if (grecaptcha == null || !(bool)grecaptcha["success"] || (double)grecaptcha["score"] < 0.5 || (string)grecaptcha["action"] != "contact")
            {
                ModelState.AddModelError("", "Could not validate as human");
                return View("~/Views/Contact/Index.cshtml");
            }

            Notification.Settings = Settings;
            Notification.SendEmail("staff@kpfanworld.com", m.Email, $"[KPFanWorld] {m.Subject}", GetBody(m));

            return View("~/Views/Contact/Index.cshtml");
        }

        private string GetBody(ContactModel m)
        {
            string val = $"<p><strong>Name:</strong> {m.Name}<br />";
            val += $"<strong>Email:</strong> {m.Email}<br />";
            val += $"<strong>Message:</strong><br />{m.Message.Nl2Br()}</p><br /><br />";

            val += "--<br />KPFW Staff<br />staff@kpfanworld.com";

            return val;
        }

        private Dictionary<string, object> VerifyReCaptcha()
        {
            FormCollection form = (FormCollection)Request.Form;
            if (form["g-recaptcha-verify"].Count == 0)
                return null;

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create("https://www.google.com/recaptcha/api/siteverify");
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";

            string postData = $"secret={Settings.ReCaptcha3SecretKey}&response={form["g-recaptcha-verify"][0].Trim(',')}&remoteip={Request.HttpContext.Connection.RemoteIpAddress}";
            byte[] send = System.Text.Encoding.Default.GetBytes(postData);
            req.ContentLength = send.Length;

            Stream stream = req.GetRequestStream();
            stream.Write(send, 0, send.Length);
            stream.Flush();
            stream.Close();

            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            StreamReader sr = new StreamReader(resp.GetResponseStream());
            string returnValue = sr.ReadToEnd();

            Dictionary<string, object> val = JsonConvert.DeserializeObject<Dictionary<string, object>>(returnValue);

            return val;
        }
    }
}