using Markdig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace kpfw
{
    public partial class PageHandler : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ContentPage cp = ContentPage.GetByUrl(Page.RouteData.Values["PageUrl"]?.ToString());
            if (cp == null)
            {
                Response.StatusCode = 404;
                return;
            }
            hTitle.InnerText = cp.Name;
            Title = "Kim Possible Fan World .:::. " + cp.Name;

            string html;
            MarkdownPipeline pipline;
            pipline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
            html = Markdown.ToHtml(cp.Content, pipline);

            ltlContent.Text = html;
        }
    }
}