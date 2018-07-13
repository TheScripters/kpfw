using System;
using System.Collections.Specialized;
using System.Web.Security;
using System.Data;
using System.Collections.Generic;
using System.Web;

namespace kpfw
{
    public class MyRoleProvider : RoleProvider
    {
        public override string ApplicationName
        {
            get
            {
                return "";
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        public override void Initialize(string name, NameValueCollection config)
        {
            base.Initialize(name, config);
            char[] seps = { ',' };

            AdminLogins = config["adminLogin"].Split(seps);
        }

        public override void AddUsersToRoles(string[] usernames, string[] rolenames)
        {
            throw new NotSupportedException();
        }

        public override void CreateRole(string rolename)
        {
            throw new NotSupportedException();
        }

        public override bool DeleteRole(string rolename, bool throwOnPopulatedRole)
        {
            throw new NotSupportedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotSupportedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            List<string> groups = new List<string>();

            foreach (string s in AdminLogins)
                if (username == s)
                    groups.Add("Administrator");
            //if (username == "Administrator")
            //{
            //    groups.Add("SuperAdmin");
            //    groups.Add("Administrator");
            //}

            //if (RequestContext.UserIdNullable != null)
            //{
            //    if (HttpContext.Current.Session != null)
            //    {
            //        // loop through groups in database and add them if assigned to user
            //        foreach (string group in UserGroups)
            //            groups.Add(group);
            //    }
            //    else
            //    {
            //        DataTable t = DBGroups.Groups_ListForUser(RequestContext.UserId);
            //        List<string> settings = new List<string>();
            //        foreach (DataRow r in t.Rows)
            //        {
            //            if (r["Name"] != DBNull.Value)
            //                groups.Add(r["Name"].ToString());
            //        }
            //    }
            //}

            if (groups.Count > 0)
                return groups.ToArray();

            return new string[0];
        }

        public override string[] GetUsersInRole(string rolename)
        {
            throw new NotSupportedException();
        }

        public override bool IsUserInRole(string username, string rolename)
        {
            throw new NotSupportedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] rolenames)
        {
            throw new NotSupportedException();
        }

        public override bool RoleExists(string rolename)
        {
            throw new NotSupportedException();
        }

        public override string[] FindUsersInRole(string rolename, string usernameToMatch)
        {
            throw new NotSupportedException();
        }

        public static void SetUserGroups(Nullable<int> userId)
        {
            //DataTable t = DBGroups.Groups_ListForUser(userId != null ? userId.Value : RequestContext.UserId);
            //List<string> settings = new List<string>();
            //foreach (DataRow r in t.Rows)
            //{
            //    if (r["Name"] != DBNull.Value)
            //        settings.Add(r["Name"].ToString());
            //}
            //UserGroups = settings;
        }

        static public List<string> UserGroups
        {
            get
            {
                if (HttpContext.Current.Session["UserGroups"] != null)
                {
                    return HttpContext.Current.Session["UserGroups"] as List<string>;
                }
                else
                {
                    SetUserGroups(null);
                    return HttpContext.Current.Session["UserGroups"] as List<string>;
                }
            }
            set
            {
                if (HttpContext.Current.Session["UserGroups"] != null)
                    HttpContext.Current.Session.Remove("UserGroups");
                HttpContext.Current.Session.Add("UserGroups", value);
            }
        }

        private string[] AdminLogins;
    }
}