using Amazon.SimpleNotificationService.Util;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace kpfw.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailApiController : ControllerBase
    {
        // POST: api/EmailApi
        [HttpPost]
        public async Task Post()
        {
            string body = "";
            try
            {
                using (StreamReader sr = new StreamReader(Request.Body))
                    body = await sr.ReadToEndAsync();

                Message msg = Message.ParseMessage(body);
                if (msg.IsMessageSignatureValid())
                {
                    if (msg.IsNotificationType)
                    {
                        dynamic m = JsonConvert.DeserializeObject(msg.MessageText);
                        dynamic mail = m.mail;
                        if (m.notificationType == "Bounce")
                        {
                            dynamic b = m.bounce;
                            if (b.bounceSubType != "General" && b.bounceSubType != "Suppressed" && b.bounceSubType != "MailboxFull")
                            {
                                Notification.SendEmail("staff@kpfanworld.com", null, "KPFW Unknown Notification", body).Wait();
                                goto END;
                            }
                            if (b.bounceType == "Transient") // "Soft" bounce
                            {
                                string emails = "";
                                foreach (dynamic recipient in b.bouncedRecipients)
                                {
                                    if (emails.Length > 0)
                                        emails += $", {recipient.emailAddress}";
                                    else emails = recipient.emailAddress;
                                    //int num = 0;
                                    //try
                                    //{
                                    //    num = DB.GetBounces((string)recipient.emailAddress);
                                    //}
                                    //catch { }
                                    //if (num >= 4) // this will be bounce #5
                                    //{
                                    //    int person = DB.GetPersonId((string)recipient.emailAddress);
                                    //    if (person > 0)
                                    //    {
                                    //        DB.DigitalUnsubscribeEmail(person, (string)recipient.emailAddress);
                                    //    }
                                    //    else
                                    //    {
                                    //        int[] people = DB.GetPersons((string)recipient.emailAddress);
                                    //        foreach (int p in people)
                                    //        {
                                    //            DB.DigitalUnsubscribeEmail(p, (string)recipient.emailAddress);
                                    //        }
                                    //    }
                                    //}
                                    //else
                                    //{
                                    //    DB.AddBounce((string)recipient.emailAddress);
                                    //}
                                }
                                Notification.SendEmail("staff@kpfanworld.com", null, "KPFW Transient Bounce Notification", emails + "<br /><br />" + body).Wait();
                            }
                            else if (b.bounceType == "Permanent")
                            {
                                string emails = "";
                                foreach (dynamic recipient in b.bouncedRecipients)
                                {
                                    if (emails.Length > 0)
                                        emails += $", {recipient.emailAddress}";
                                    else emails = recipient.emailAddress;
                                    //int person = DB.GetPersonId((string)recipient.emailAddress);
                                    //if (person > 0)
                                    //{
                                    //    // deactivate email (log entry is recorded for person history)
                                    //    DB.DigitalUnsubscribeEmail(person, (string)recipient.emailAddress);
                                    //}
                                    //else
                                    //{
                                    //    int[] people = DB.GetPersons((string)recipient.emailAddress);
                                    //    foreach (int p in people)
                                    //    {
                                    //        DB.DigitalUnsubscribeEmail(p, (string)recipient.emailAddress);
                                    //    }
                                    //}
                                }
                                Notification.SendEmail("staff@kpfanworld.com", null, "KPFW Permanent Bounce Notification", emails).Wait();
                            }
                        }
                        else if (m.notificationType == "Complaint")
                        {
                            try
                            {
                                dynamic c = m.complaint;

                                string complaintBody = "<p>Following are the details of the email complaint. <span style='font-weight:bold;color:#f00;'>Automatic action is not available at this time.</span> Please take the appropriate action (eg - disable email, reach out to customer, etc).</p>";
                                string complainedEmails = "";
                                foreach (dynamic em in c.complainedRecipients)
                                {
                                    if (complainedEmails.Length > 0)
                                        complainedEmails += "," + (string)em.emailAddress;
                                    else complainedEmails = (string)em.emailAddress;
                                }
                                complaintBody += $"<p><strong>Complaint Type:</strong> {c.complaintFeedbackType}</p>";
                                complaintBody += $"<p><strong>Email(s):</strong> {complainedEmails}</p>";
                                complaintBody += $"<p><strong>Original Subject:</strong> {(string)mail.commonHeaders.subject}</p>";
                                complaintBody += $"<p><strong>Date Sent:</strong> {(string)mail.commonHeaders.date}</p>";

                                complaintBody += "<h2 style='margin-top:30px;'>Headers</h2><ul>";

                                foreach (dynamic h in mail.headers)
                                    complaintBody += $"<li><strong>{h.name}:</strong> {h.value}</li>";

                                complaintBody += "</ul>";


                                Notification.SendEmail("staff@kpfanworld.com", null, "KPFW Complaint Notification", complaintBody).Wait();
                            }
                            catch (Exception ex)
                            {
                                Notification.SendError(Request.HttpContext, ex).Wait();
                            }
                        }
                        else
                        {
                            Notification.SendEmail("staff@kpfanworld.com", null, "KPFW Non-Bounce Notification", body).Wait();
                        }
                    }
                    else
                    {
                        Notification.SendEmail("staff@kpfanworld.com", null, "KPFW Non-NotificationType", body).Wait();
                    }
                }
                else
                {
                    Notification.SendEmail("staff@kpfanworld.com", null, "KPFW SES Failed Signature Check", body).Wait();
                }
            }
            catch (Exception ex)
            {
                Notification.SendError(Request.HttpContext, ex).Wait();
            }
        END:
            return;
        }
    }
}
