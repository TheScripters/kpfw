using MailChimp.Net;
using MailChimp.Net.Interfaces;
using MailChimp.Net.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kpfw
{
    /// <summary>
    /// Summary description for MailChimpApi
    /// </summary>
    public static class MailChimpApi
    {
        private const string ApiKey = "";
        private const string ListID = "";
        static public bool CheckIfSubscribed(string email)
        {
            MailChimpManager manager = new MailChimpManager(ApiKey);
            var exists = manager.Members.ExistsAsync(ListID, email);
            var e = exists.Result;
            return e;
        }
        static public void Subscribe(string email)
        {
            MailChimpManager manager = new MailChimpManager(ApiKey);
            var member = new Member { EmailAddress = email, StatusIfNew = Status.Pending };
            var newMember = manager.Members.AddOrUpdateAsync(ListID, member);
            var n = newMember.Result;
        }
        static public void Activate(string email)
        {
            MailChimpManager manager = new MailChimpManager(ApiKey);
            var member = new Member { EmailAddress = email, Status = Status.Subscribed };
            var newMember = manager.Members.AddOrUpdateAsync(ListID, member);
            var n = newMember.Result;
        }
        static public void Deactivate(string email)
        {
            MailChimpManager manager = new MailChimpManager(ApiKey);
            var member = new Member { EmailAddress = email, Status = Status.Unsubscribed };
            var newMember = manager.Members.AddOrUpdateAsync(ListID, member);
            var n = newMember.Result;
        }
        static public void Delete(string email)
        {
            MailChimpManager manager = new MailChimpManager(ApiKey);
            var newMember = manager.Members.DeleteAsync(ListID, email);
            newMember.Wait();
        }
        static public List<string> ListLists()
        {
            List<string> l = new List<string>();
            MailChimpManager manager = new MailChimpManager(ApiKey);
            var lists = manager.Lists.GetAllAsync();
            foreach(var list in lists.Result)
                l.Add(list.Name + ": " + list.Id);

            return l;
        }
        static public void Test()
        {
            MailChimpManager manager = new MailChimpManager(ApiKey);
            var lists = manager.Lists.GetAllAsync();
            var l = lists.Result;
        }
    }
}