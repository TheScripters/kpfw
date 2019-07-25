using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kpfw.Models
{
    public class KpfwSettings
    {
        public string AuthyApiKey { get; set; }
        public string ReCaptchaSecretKey { get; set; }
        public string ReCaptchaSiteKey { get; set; }
        public string SESAccessKey { get; set; }
        public string SESSecret { get; set; }
        public string ConnectionString { get; set; }
    }
}
