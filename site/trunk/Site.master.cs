using Google.Authenticator;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Optimization;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace kpfw
{
    public partial class Site : System.Web.UI.MasterPage
    {
        private const string reCaptchaSecret = "6LcgoVQUAAAAAOC0-qGtIp-qvFcWYEeLAsW0Z8p0";
        private int NumTries
        {
            get
            {
                HttpCookie c = Request.Cookies.Get("InvalidTries");
                if (c == null)
                    return 0;
                else return Convert.ToInt32(c.Value);
            }
            set
            {
                HttpCookie c = Request.Cookies.Get("InvalidTries");
                if (c == null)
                {
                    c = new HttpCookie("InvalidTries");
                    c.Value = value.ToString();
                    c.Expires = DateTime.UtcNow.AddDays(30);
                    Response.Cookies.Add(c);
                }
                else
                {
                    c.Value = value.ToString();
                    c.Expires = DateTime.UtcNow.AddDays(30);
                    Response.Cookies.Add(c);
                }
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            plhBundles.Controls.Add(new LiteralControl(Scripts.Render("~/bundle/js").ToHtmlString()));
            plhBundles.Controls.Add(new LiteralControl(Styles.Render("~/bundle/css").ToHtmlString()));

            plhLogin.Visible = !Page.User.Identity.IsAuthenticated;
            plhAccount.Visible = Page.User.Identity.IsAuthenticated;

            if (!IsPostBack)
            {
                if (Page.User.Identity.IsAuthenticated)
                {
                    if (User.NeedsPasswordChanged(Page.User.Identity.Name))
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "changePassword", "$.magnificPopup.open({ items: { src: '#changePasswordModal' }, prependTo:'form#aspnetForm', closeOnBgClick: false });", true);
                }
                plhLoginCaptcha.Visible = NumTries >= 3;
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
			if (NumTries > 4)
				return;
            // need to start displaying a CAPTCHA (ReCAPTCHA?) if number of attempts reaches a certain threshold
            FailureText.Visible = false;
            if (!Page.IsValid)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "LoginError", "$.magnificPopup.open({ items: { src: '#loginModalPopup' }, prependTo:'form#aspnetForm', closeOnBgClick: false });", true);
                return;
            }
            if (NumTries > 2)
            {
                if (!VerifyReCaptcha())
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "LoginError", "$.magnificPopup.open({ items: { src: '#loginModalPopup' }, prependTo:'form#aspnetForm', closeOnBgClick: false });", true);
                    FailureText.Visible = false;
                    return;
                }
            }
            plhLoginCaptcha.Visible = NumTries >= 2;
            if (!new Regex(@"^[a-zA-Z0-9_]+$").IsMatch(txtUserName.Text) && !new Regex(@"^(([A-Za-z0-9]+_+)|([A-Za-z0-9]+\-+)|([A-Za-z0-9]+\.+)|([A-Za-z0-9]+\++))*[A-Za-z0-9]+@((\w+\-+)|(\w+\.))*\w{1,63}\.[a-zA-Z]{2,6}$").IsMatch(txtUserName.Text))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "LoginError", "$.magnificPopup.open({ items: { src: '#loginModalPopup' }, prependTo:'form#aspnetForm', closeOnBgClick: false });", true);
                FailureText.InnerText = "Invalid username. Your username must contain only letters, numbers, and the underscore character or must be an email address.";
                FailureText.Visible = true;
                NumTries = NumTries + 1;
                return;
            }

            var u = User.GetPassword(txtUserName.Text);
            if (u == null)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "LoginError", "$.magnificPopup.open({ items: { src: '#loginModalPopup' }, prependTo:'form#aspnetForm', closeOnBgClick: false });", true);
                FailureText.InnerHtml = "Sorry, Wade couldn't find anyone on file with those credentials. <a href=\"/Contact\">Contact us</a> if you feel this is in error.";
                FailureText.Visible = true;
                NumTries = NumTries + 1;
                return;
            }

            Auth auth = new Auth(txtPassword.Text, u.Value.password);
            if (!auth.Compare(out bool md5))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "LoginError", "$.magnificPopup.open({ items: { src: '#loginModalPopup' }, prependTo:'form#aspnetForm', closeOnBgClick: false });", true);
                FailureText.InnerHtml = "Sorry, Wade couldn't find anyone on file with those credentials. <a href=\"/Contact\">Contact us</a> if you feel this is in error.";
                FailureText.Visible = true;
                NumTries = NumTries + 1;
                return;
            }
            if (!String.IsNullOrWhiteSpace(u.Value.TwoFactorAuth))
            {
                // user has 2FA enabled so show that before logging them in.
                ViewState["2FAUser"] = u.Value.userId + "|" + u.Value.userName + "|" + u.Value.displayName + "|" + u.Value.TwoFactorAuth + "|" + md5 + "|" + u.Value.isActive + "|" + auth.GetNewHash();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "changePassword", "$.magnificPopup.open({ items: { src: '#twoFAModal' }, prependTo:'form#aspnetForm', closeOnBgClick: false });", true);
                return;
            }

            if ((md5 && !u.Value.isActive) || u.Value.isActive)
            {
                FormsAuthentication.RedirectFromLoginPage(u.Value.userName, false);
                if (NumTries > 0)
                {
                    HttpCookie c = Request.Cookies.Get("InvalidTries");
                    c.Expires = DateTime.UtcNow.AddDays(-1);
                    Response.Cookies.Add(c);
                }

                if (md5)
                {
                    User.UpdatePassword(u.Value.userId, auth.GetNewHash());
                    User.AssignEmailConfirmation(u.Value.userId, Guid.NewGuid(), true);
                }
                else
                    Response.Redirect(Request.RawUrl, true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "LoginError", "$.magnificPopup.open({ items: { src: '#loginModalPopup' }, prependTo:'form#aspnetForm', closeOnBgClick: false });", true);
                FailureText.InnerHtml = "Sorry, Wade couldn't find anyone on file with those credentials. <a href=\"/Contact\">Contact us</a> if you feel this is in error.";
                FailureText.Visible = true;
                NumTries = NumTries + 1;
                return;
            }
        }

        protected void btnSignUp_Click(object sender, EventArgs e)
        {
            SignupFailureText.Visible = false;
            if (!VerifyReCaptcha())
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "SignUpError", "$.magnificPopup.open({ items: { src: '#signupModalPopup' }, prependTo:'form#aspnetForm', closeOnBgClick: false });", true);
                SignupFailureText.InnerText = "Invalid CAPTCHA.";
                SignupFailureText.Visible = true;
                return;
            }
            if (FavoriteColor.Text != "")
                return;
            if (!Page.IsValid)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "SignUpError", "$.magnificPopup.open({ items: { src: '#signupModalPopup' }, prependTo:'form#aspnetForm', closeOnBgClick: false });", true);
                return;
            }
            if (!new Regex(@"^[a-zA-Z0-9_]+$").IsMatch(txtSignUpUser.Text) && !new Regex(@"^(([A-Za-z0-9]+_+)|([A-Za-z0-9]+\-+)|([A-Za-z0-9]+\.+)|([A-Za-z0-9]+\++))*[A-Za-z0-9]+@((\w+\-+)|(\w+\.))*\w{1,63}\.[a-zA-Z]{2,6}$").IsMatch(txtSignUpUser.Text))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "SignUpError", "$.magnificPopup.open({ items: { src: '#signupModalPopup' }, prependTo:'form#aspnetForm', closeOnBgClick: false });", true);
                SignupFailureText.InnerText = "Invalid username. Your username must contain only letters, numbers, and the underscore character or must be an email address.";
                SignupFailureText.Visible = true;
                return;
            }

            if (User.EmailExists(txtSignUpEmail.Text.Trim()))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "SignUpError", "$.magnificPopup.open({ items: { src: '#signupModalPopup' }, prependTo:'form#aspnetForm', closeOnBgClick: false });", true);
                SignupFailureText.InnerHtml = "Wade tells us that email address already exists in our database. Please <a href=\"#loginModalPopup\" class=\"sign-in\">log in</a> or <a href=\"#resetPasswordModal\" class=\"reset-password\">reset your password</a> to continue using that account.";
                SignupFailureText.Visible = true;
                return;
            }
            if (User.LoginExists(txtSignUpUser.Text.Trim()))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "SignUpError", "$.magnificPopup.open({ items: { src: '#signupModalPopup' }, prependTo:'form#aspnetForm', closeOnBgClick: false });", true);
                SignupFailureText.InnerHtml = "Wade tells us that user name already exists in our database. Please <a href=\"#loginModalPopup\" class=\"sign-in\">log in</a> or <a href=\"#resetPasswordModal\" class=\"reset-password\">reset your password</a> to continue using that account. <a href=\"/Contact\">Contact us</a> if you don't have access to that email address.";
                SignupFailureText.Visible = true;
                return;
            }

            Auth auth = new Auth(txtSignUpPassword.Text.Trim());

            Guid emailConfirmation = Guid.NewGuid();
            while (User.ConfirmationExists(emailConfirmation))
                emailConfirmation = Guid.NewGuid();

            User u = new User
            {
                UserName = txtSignUpUser.Text.Trim(),
                Email = txtSignUpEmail.Text.Trim(),
                TimeZone = TimeZoneInfo.FindSystemTimeZoneById("UTC")
            };
            u.Add(auth.GetNewHash(), Request.UserHostAddress, emailConfirmation);

            string body = "<p>Hi! Welcome to Kim Possible Fan World!</p>";
            body += "<p>We're happy to have you, but we need to make sure you're a human and not one of Drakken's little bots trying to gain access. He loves the spotlight, after all.</p>";
            body += "<p>Just click or copy/paste the link below and you'll be ready to start editing and submitting information!</p>";
            body += $@"<p><a href=""https://dev.kpfanworld.com/confirm/{emailConfirmation.ToString()}"" target=""_blank"">https://dev.kpfanworld.com/confirm/{emailConfirmation.ToString()}</a></p>";
            body += "<p>Thanks again for joining! We hope to see you back frequently!</p>";
            body += "<p>----------<br />KPFW Staff<br />contact@kpfanworld.com</p>";

            // send email for confirmation
            Notification.SendEmail(u.Email, "[KPFanWorld] Sign Up Confirmation", body);
            MailChimpApi.Subscribe(u.Email);

            // bring up the signup screen again, but display success message telling them to check their email.
            plhSignUp.Visible = false;
            plhSignupSuccess.Visible = true;
            signupStatus.InnerHtml = "You have successfully signed up! We just need one more thing before Wade can finalize your account. We've sent an email with a special link that will let you gain access. Once you click that link you'll be ready to roll! <a href=\"/Contact\">Contact us</a> if you have problems or don't get the email within a few minutes.";
        }

        protected void btnResetPassword_Click(object sender, EventArgs e)
        {
            resetStatus.Visible = false;
            resetFailureText.Visible = false;
            if (!VerifyReCaptcha())
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "resetError", "$.magnificPopup.open({ items: { src: '#resetPasswordModal' }, prependTo:'form#aspnetForm', closeOnBgClick: false });", true);
                resetFailureText.InnerText = "Invalid CAPTCHA.";
                resetFailureText.Visible = true;
                return;
            }
            if (!Page.IsValid)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "resetError", "$.magnificPopup.open({ items: { src: '#resetPasswordModal' }, prependTo:'form#aspnetForm', closeOnBgClick: false });", true);
                return;
            }
            if (!new Regex(@"^[a-zA-Z0-9_]+$").IsMatch(txtUserEmail.Text) && !new Regex(@"^(([A-Za-z0-9]+_+)|([A-Za-z0-9]+\-+)|([A-Za-z0-9]+\.+)|([A-Za-z0-9]+\++))*[A-Za-z0-9]+@((\w+\-+)|(\w+\.))*\w{1,63}\.[a-zA-Z]{2,6}$").IsMatch(txtUserEmail.Text))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "resetError", "$.magnificPopup.open({ items: { src: '#resetPasswordModal' }, prependTo:'form#aspnetForm', closeOnBgClick: false });", true);
                resetFailureText.InnerText = "Invalid username. Your username must contain only letters, numbers, and the underscore character or must be an email address.";
                resetFailureText.Visible = true;
                return;
            }

            Guid reset = Guid.NewGuid();
            while (User.GetResetRequest(reset) != null)
                reset = Guid.NewGuid();

            var u = User.GetByLogin(txtUserEmail.Text);
            if (u != null)
            {
                string body = "<p>Hi!</p>";
                body += "<p>Someone (hopefully you) recently requested a password reset.</p>";
                body += "<p>We need to make sure you're a human and not one of Drakken's little bots trying to gain access.</p>";
                body += "<p>Just click or copy/paste the link below and you'll be taken to a page where you can set a new password. This link expires in 48 hours, though.</p>";
                body += "<p>If someone other than you requested it or you changed your mind just ignore this message. Your password will not be changed unless you go to the URL below within 48 hours.</p>";
                body += $@"<p><a href=""https://dev.kpfanworld.com/reset/{reset.ToString()}"" target=""_blank"">https://dev.kpfanworld.com/reset/{reset.ToString()}</a></p>";
                body += "<p>Contact us if you have any questions or problems.</p>";
                body += "<p>----------<br />KPFW Staff<br />contact@kpfanworld.com</p>";

                User.AddResetRequest(reset, u.Id);
                Notification.SendEmail(u.Email, "[KPFanWorld] Password Reset Request", body);
            }

            plhResetPassword.Visible = false;
            resetStatus.Visible = true;
            resetStatus.InnerHtml = "If Wade finds an account he'll send a message to the email address on file with instructions to reset your password. If you continue to have issues, feel free to <a href=\"/Contact\">contact us</a>.";

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "resetError", "$.magnificPopup.open({ items: { src: '#resetPasswordModal' }, prependTo:'form#aspnetForm', closeOnBgClick: false });", true);
        }

        protected void lbLogout_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("/", true);
        }

        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;
            if (!Page.User.Identity.IsAuthenticated)
                return;

            var u = User.GetByLogin(Page.User.Identity.Name);
            if (u == null)
                return;

            Auth auth = new Auth(txtPassword.Text);
            User.UpdatePassword(u.Id, auth.GetNewHash());
            User.Activate(u.Id);

            plhChangePassword.Visible = false;
            plhChangePasswordSuccess.Visible = true;

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "changePassword", "$.magnificPopup.open({ items: { src: '#changePasswordModal' }, prependTo:'form#aspnetForm', closeOnBgClick: false });", true);
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

        protected void btnConfirm2FA_Click(object sender, EventArgs e)
        {
            // 0 - userId
            // 1 - userName
            // 2 - displayName
            // 3 - Authy ID
            // 4 - md5
            // 5 - isActive
            // 6 - NewHash
            string[] u = ((string)ViewState["2FAUser"]).Split('|');
            bool md5 = Convert.ToBoolean(u[4]), isActive = Convert.ToBoolean(u[5]);
            //var u = User.GetPassword(txtUserName.Text);
            bool tfaValid = false;

            if (!Regex.IsMatch(u[3], @"^[\d]+$"))
            {
                TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
                if (tfa.ValidateTwoFactorPIN(u[3], txt2FACode.Text))
                    tfaValid = true;
            }
            else
            {
                var client = new AuthyClient(ConfigurationManager.AppSettings["AuthyApiKey"]);
                if (client.VerifyToken(Convert.ToInt32(u[3]), Convert.ToInt32(txt2FACode.Text)))
                    tfaValid = true;
            }

            if (tfaValid)
            {
                if ((md5 && !isActive) || isActive)
                {
                    FormsAuthentication.RedirectFromLoginPage(u[1], false);
                    if (NumTries > 0)
                    {
                        HttpCookie c = Request.Cookies.Get("InvalidTries");
                        c.Expires = DateTime.UtcNow.AddDays(-1);
                        Response.Cookies.Add(c);
                    }

                    if (md5)
                    {
                        User.UpdatePassword(Convert.ToInt32(u[0]), u[6]);
                        User.AssignEmailConfirmation(Convert.ToInt32(u[0]), Guid.NewGuid(), true);
                    }
                    else
                        Response.Redirect(Request.RawUrl, true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "LoginError", "$.magnificPopup.open({ items: { src: '#loginModalPopup' }, prependTo:'form#aspnetForm', closeOnBgClick: false });", true);
                    FailureText.InnerHtml = "Sorry, there's a problem with your account. <a href=\"/Contact\">Contact us</a> to get it resolved.";
                    FailureText.Visible = true;
                    NumTries = NumTries + 1;
                    return;
                }
            }
            else
            {
                // Display error and re-display popup
                TwoFactorError.InnerText = "Invalid token. Please try again.";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "changePassword", "$.magnificPopup.open({ items: { src: '#twoFAModal' }, prependTo:'form#aspnetForm', closeOnBgClick: false });", true);
            }
        }
    }
}