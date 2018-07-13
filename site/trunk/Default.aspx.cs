using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace kpfw
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<Timeline> timeline = Timeline.ListToday();
            rptToday.Visible = timeline.Count > 0;
            pNoEvents.Visible = timeline.Count == 0;

            rptToday.DataSource = timeline;
            rptToday.DataBind();
        }
    }
}