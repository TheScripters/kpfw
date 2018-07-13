using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Profile;

namespace kpfw
{
    public class MyProfileProvider : ProfileProvider
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

        public override SettingsPropertyValueCollection
            GetPropertyValues(SettingsContext context,
                  SettingsPropertyCollection ppc)
        {
            string login = (string)context["UserName"];
            bool isAuthenticated = (bool)context["IsAuthenticated"];

            if (login.Length <= 0)
                throw new Exception("Login empty");

            SettingsPropertyValueCollection svc =
                new SettingsPropertyValueCollection();

            //DataRow r = DBUser.GetByLogin(login);
            //foreach (SettingsProperty prop in ppc)
            //{
            //    SettingsPropertyValue pv = new SettingsPropertyValue(prop);

            //    if (r != null)
            //        pv.PropertyValue = r[prop.Name];
            //    else
            //        pv.PropertyValue = "";

            //    svc.Add(pv);
            //}

            return svc;
        }

        public override void SetPropertyValues(SettingsContext context,
                         SettingsPropertyValueCollection properties)
        {
            string login = (string)context["UserName"];
            bool isAuthenticated = (bool)context["IsAuthenticated"];

            if (login.Length <= 0)
                throw new Exception("Login empty");

            //DBUser.SetOrCreate(login, properties);
        }

        public override int DeleteInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate)
        {
            throw new NotSupportedException();
        }

        public override int DeleteProfiles(string[] usernames)
        {
            throw new NotSupportedException();
        }

        public override int DeleteProfiles(ProfileInfoCollection profiles)
        {
            throw new NotSupportedException();
        }

        public override ProfileInfoCollection FindInactiveProfilesByUserName(ProfileAuthenticationOption authenticationOption, string usernameToMatch, DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotSupportedException();
        }

        public override ProfileInfoCollection FindProfilesByUserName(ProfileAuthenticationOption authenticationOption, string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotSupportedException();
        }

        public override ProfileInfoCollection GetAllInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotSupportedException();
        }

        public override ProfileInfoCollection GetAllProfiles(ProfileAuthenticationOption authenticationOption, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotSupportedException();
        }

        public override int GetNumberOfInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate)
        {
            throw new NotSupportedException();
        }
    }
}