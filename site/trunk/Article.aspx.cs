using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace kpfw
{
    public partial class ArticlePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                rptArticleList.DataSource = pager.ProcessList(Article.List());
                rptArticleList.DataBind();
            }
        }

        protected void pager_PageNumberChange(object sender, EventArgs e)
        {
            rptArticleList.DataSource = pager.ProcessList(Article.List());
            rptArticleList.DataBind();
        }
    }
}