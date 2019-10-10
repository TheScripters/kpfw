using kpfw.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace kpfw
{
    /// <summary>
    /// Summary description for Notification
    /// </summary>
    public static class Notification
    {
        static internal KpfwSettings Settings;
        public static void SendEmail(string to, string subject, string body)
        {
            using (MailMessage m = new MailMessage("contact@kpfanworld.com", to)
            {
                Body = body,
                Subject = subject,
                IsBodyHtml = true
            })
            {

                string smtpPassword = "";
                byte[] key = Encoding.UTF8.GetBytes(Settings.SESSecret);
                using (var sha = new HMACSHA256(key))
                {
                    byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes("SendRawEmail"));
                    byte ver = 0x02;
                    var s = hash.ToList();
                    s.Insert(0, ver);
                    smtpPassword = Convert.ToBase64String(s.ToArray());
                }

                using (SmtpClient c = new SmtpClient()
                {
                    Host = "email-smtp.us-west-2.amazonaws.com",
                    Port = 587,
                    EnableSsl = true,
                    Credentials = new NetworkCredential(Settings.SESAccessKey, smtpPassword)
                })
                {
                    try
                    {
                        c.Send(m);
                    }
                    catch { }
                }
            }
        }

        static public void SendError(HttpContext context, Exception ex, string note = "")
        {
            MailMessage m = new MailMessage();
            try
            {
                // that send process does not include default bcc copying
                m = new MailMessage("contact@kpfanworld.com", "adam@kpfanworld.com");
            }
            catch { }
            m.Subject = "[KP Fan World] Web application error" + (note.Length > 0 ? " (" + note + ")" : "");
            m.Body = GetFullError(context, ex);

            SmtpClient c = new SmtpClient()
            {
                Credentials = new NetworkCredential("", "")
            };
            try
            {
                c.Send(m);
            }
            catch (Exception)
            { }
            m.Dispose();
        }

        static private string GetFullError(HttpContext context, Exception ex)
        {
            //HttpContext context = HttpContext.Current;
            string sPostedData = "";
            foreach (string sKey in context.Request.Form.Keys)
            {
                if (sKey.StartsWith("__VIEW") || sKey.StartsWith("__EVENT"))
                    continue;
                sPostedData += "    " + sKey + " = " + context.Request.Form[sKey] + "\n";
            }

            string sHeaderList = "";
            foreach (string sKey in context.Request.Headers.Keys)
                sHeaderList += "    " + sKey + " = " + context.Request.Headers[sKey] + "\n";

            string header = "";
            if (ex != null)
            {
                Exception headerEx = ex;
                if (headerEx.InnerException != null)
                    headerEx = headerEx.InnerException;
                if (headerEx != null)
                    header = headerEx.Message + "\r\n\r\n";
            }

            return
                header +
                "Error Type:  Web Application Error\n" +
                "Server Name: " + Environment.MachineName + "\n" +
                "Client Ip:   " + context.Request.GetDisplayUrl() + "\n" +
                "Request Url: " + context.Request.Path + "\n" +
                "Headers: \n" + sHeaderList +
                "Posted Data: \n" + sPostedData +
                "Exception: \n" +"";
                    //Robo.ExcDetails.Get(ex);
        }
    }
}