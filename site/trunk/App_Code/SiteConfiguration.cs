using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace kpfw
{
    /// <summary>
    /// Summary description for SiteConfiguration
    /// </summary>
    public static class SiteConfiguration
    {
        public static string AuthyApiKey
        {
            get
            {
                return ConfigurationManager.AppSettings["AuthyApiKey"];
            }
        }
        public static string MailChimpApiKey
        {
            get
            {
                return ConfigurationManager.AppSettings["MailChimpApiKey"];
            }
        }
        public static string MailChimpListID
        {
            get
            {
                return ConfigurationManager.AppSettings["MailChimpListID"];
            }
        }
        public static string ReCaptchaSecretKey
        {
            get
            {
                return ConfigurationManager.AppSettings["ReCaptchaSecretKey"];
            }
        }
        public static string ReCaptchaSiteKey
        {
            get
            {
                return ConfigurationManager.AppSettings["ReCaptchaSiteKey"];
            }
        }
        public static string SESAccessKey
        {
            get
            {
                return ConfigurationManager.AppSettings["SESAccessKey"];
            }
        }
        public static string SESSecret
        {
            get
            {
                return ConfigurationManager.AppSettings["SESSecret"];
            }
        }
        public static string SmtpPassword
        {
            get
            {
                byte[] key = Encoding.UTF8.GetBytes(SESSecret);
                var sha = new HMACSHA256(key);
                byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes("SendRawEmail"));
                byte ver = 0x02;
                var s = hash.ToList();
                s.Insert(0, ver);
                return Convert.ToBase64String(s.ToArray());
            }
        }
    }
}