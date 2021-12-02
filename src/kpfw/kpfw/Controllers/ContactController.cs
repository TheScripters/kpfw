using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
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
                ModelState.AddModelError("Captcha", "Could not validate as human");
                return View("~/Views/Contact/Index.cshtml");
            }

            Notification.Settings = Settings;
            _ = Notification.SendEmail("staff@kpfanworld.com", m.Email, $"[KPFanWorld] {m.Subject}", GetBody(m)).Result;

            TempData["Success"] = true;

            return View();
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

            Dictionary<string, string> Values = new Dictionary<string, string>
            {
                { "secret", Settings.ReCaptcha3SecretKey },
                { "response", form["g-recaptcha-verify"][0].Trim(',') },
                { "remoteip", Request.HttpContext.Connection.RemoteIpAddress?.ToString() ?? "" }
            };
            Dictionary<string, object> resp = new Dictionary<string, object>();
            using (HttpClient client = new HttpClient())
            {
                using (var postContent = new FormUrlEncodedContent(Values))
                using (HttpResponseMessage response = client.PostAsync("https://www.google.com/recaptcha/api/siteverify", postContent).Result)
                {
                    response.EnsureSuccessStatusCode();
                    using (HttpContent content = response.Content)
                    {
                        string result = content.ReadAsStringAsync().Result;
                        resp = JsonConvert.DeserializeObject<Dictionary<string, object>>(result);
                    }
                }
            }

            return resp;
        }
    }
}