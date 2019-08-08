using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kpfw.Models
{
    public class KpfwSettings
    {
        public string Antiforgery { get; set; }
        public string AuthyApiKey { get; set; }
        public string ConnectionString { get; set; }
        public string CookieName { get; set; }
        public string MailChimpApiKey { get; set; }
        public string MailChimpListID { get; set; }
        public string ReCaptcha3SecretKey { get; set; }
        public string ReCaptcha3SiteKey { get; set; }
        public string ReCaptchaSecretKey { get; set; }
        public string ReCaptchaSiteKey { get; set; }
        public string SESAccessKey { get; set; }
        public string SESSecret { get; set; }
        public bool UseHsts { get; set; }
    }
}
