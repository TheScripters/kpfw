using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using kpfw.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace kpfw.Controllers
{
    public class DonateController : Controller
    {
        private readonly KpfwSettings Settings;

        public DonateController(IConfiguration configuration)
        {
            Settings = configuration.GetSection("Kpfw").Get<KpfwSettings>();
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewData["StripePK"] = Settings.StripePublishableKey;
            return View();
        }

        [HttpPost]
        public IActionResult Index(object model)
        {

            var grecaptcha = VerifyReCaptcha();
            if (grecaptcha == null || !(bool)grecaptcha["success"] || (double)grecaptcha["score"] < 0.5 || (string)grecaptcha["action"] != "contact")
            {
                ModelState.AddModelError("Captcha", "Could not validate as human");
                return View();
            }

            return View();
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
