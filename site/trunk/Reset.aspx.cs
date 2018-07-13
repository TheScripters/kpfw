using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace kpfw
{
    public partial class ResetPage : System.Web.UI.Page
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
                        plhResetPassword.Visible = false;
                    }
                    else
                    {
                        var req = kpfw.User.GetResetRequest(confirmation);
                        if (req == null || req.Value.reset || req.Value.requestDate < DateTime.UtcNow.AddHours(-48))
                        {
                            plhError.Visible = true;
                            plhResetPassword.Visible = false;
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

        protected void btnResetPassword_Click(object sender, EventArgs e)
        {
            if (!IsValid)
                return;

            if (Page.RouteData.Values["EmailConfirmation"] != null)
            {
                if (!Guid.TryParse(Page.RouteData.Values["EmailConfirmation"].ToString(), out Guid confirmation))
                {
                    plhError.Visible = true;
                    plhResetPassword.Visible = false;
                }
                else
                {
                    var req = kpfw.User.GetResetRequest(confirmation);
                    if (req == null || req.Value.reset || req.Value.requestDate < DateTime.UtcNow.AddHours(-48))
                    {
                        plhError.Visible = true;
                        plhResetPassword.Visible = false;
                    }
                    else
                    {
                        Auth auth = new Auth(txtPassword.Text);
                        kpfw.User.UpdatePassword(req.Value.userId, auth.GetNewHash());
                        kpfw.User.Activate(req.Value.userId);

                        plhResetPassword.Visible = false;
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