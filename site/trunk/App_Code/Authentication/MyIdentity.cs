using System;
using System.Collections.Specialized;
using System.Security.Principal;
using System.Web.Security;

namespace kpfw
{
    [Serializable]
    public class MyIdentity : IIdentity
    {
        public string AuthenticationType
        {
            get
            {
                return "Cookie";
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                return false;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public MyIdentity(string name)
        {
            _name = name;
        }

        private string _name;
    }
}