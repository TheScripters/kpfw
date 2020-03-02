using Amazon;
using Amazon.Runtime;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using kpfw.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace kpfw
{
    /// <summary>
    /// Summary description for Notification
    /// </summary>
    public static class Notification
    {
        static internal KpfwSettings Settings;
        static public async Task<bool> SendEmail(string to, string replyTo, string subject, string body)
        {
            Message msg = new Message(new Content(subject), new Body() { Html = new Content(body) });
            SendEmailRequest req = new SendEmailRequest("contact@kpfanworld.com", new Destination(new List<string> { to }), msg) { ReturnPath = "staff@kpfanworld.com" };
            if (!String.IsNullOrWhiteSpace(replyTo))
                req.ReplyToAddresses.Add(replyTo);
            using (AmazonSimpleEmailServiceClient client = new AmazonSimpleEmailServiceClient(new BasicAWSCredentials(Settings.SESAccessKey, Settings.SESSecret), RegionEndpoint.USWest2))
            {
                try
                {
                    var resp = await client.SendEmailAsync(req);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        static public async Task<bool> SendError(HttpContext context, Exception ex, string note = "")
        {
            Message msg = new Message(new Content("[KP Fan World] Web application error" + (note.Length > 0 ? " (" + note + ")" : "")), new Body(new Content(GetFullError(context, ex))));
            SendEmailRequest req = new SendEmailRequest("contact@kpfanworld.com", new Destination(new List<string> { "staff@kpfanworld.com" }), msg);
            using (AmazonSimpleEmailServiceClient client = new AmazonSimpleEmailServiceClient(new BasicAWSCredentials(Settings.SESAccessKey, Settings.SESSecret), RegionEndpoint.USWest2))
            {
                try
                {
                    var resp = await client.SendEmailAsync(req);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
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
                "Client Ip:   " + context.Connection.RemoteIpAddress + "\n"+
                "Display URL: " + context.Request.GetDisplayUrl() + "\n" +
                "Request Url: " + context.Request.Path + "\n" +
                "Headers: \n" + sHeaderList +
                "Posted Data: \n" + sPostedData +
                "Exception: \n" +"";
                    //Robo.ExcDetails.Get(ex);
        }
    }
}