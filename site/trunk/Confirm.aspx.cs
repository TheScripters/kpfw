using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace kpfw
{
    public partial class ConfirmPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Page.RouteData.Values["EmailConfirmation"] != null)
                {
                    if (!Guid.TryParse(Page.RouteData.Values["EmailConfirmation"].ToString(), out Guid confirmation))
                    {
                        plhError.Visible = true;
                        plhSuccess.Visible = false;
                    }
                    else
                    {
                        if (!kpfw.User.ConfirmationExists(confirmation))
                        {
                            plhError.Visible = true;
                            plhSuccess.Visible = false;
                        }
                        else
                        {
                            string userName = kpfw.User.GetUser(confirmation);
                            kpfw.User.Activate(confirmation);

                            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(userName, false, 30);
                            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                            cookie.Expires = authTicket.Expiration;
                            HttpContext.Current.Response.Cookies.Set(cookie);

                            plhError.Visible = false;
                            plhSuccess.Visible = true;
                        }
                    }
                }
                else
                {
                    plhError.Visible = true;
                    plhSuccess.Visible = false;
                }
            }
        }
    }
}