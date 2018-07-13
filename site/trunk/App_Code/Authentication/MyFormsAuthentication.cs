using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Data;

/// <summary>
/// Summary description for MyFormsAuthentication
/// </summary>
namespace kpfw
{
    public class MyFormsAuthentication
    {
        public static void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
}