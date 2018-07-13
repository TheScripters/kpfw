using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Admin : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        plhBundles.Controls.Add(new LiteralControl(Scripts.Render("~/bundle/js").ToHtmlString()));
        plhBundles.Controls.Add(new LiteralControl(Styles.Render("~/bundle/css").ToHtmlString()));
    }

    protected void lbLogout_Click(object sender, EventArgs e)
    {
        FormsAuthentication.SignOut();
        Response.Redirect("/", true);
    }
}
