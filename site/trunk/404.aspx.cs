using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _404 : System.Web.UI.Page
{
    protected override void OnLoad(EventArgs e)
    {
        Response.TrySkipIisCustomErrors = true;
        Response.Status = "404 Not Found";
        Response.StatusCode = 404;
        base.OnLoad(e);
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
}