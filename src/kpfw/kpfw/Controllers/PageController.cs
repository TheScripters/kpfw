using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kpfw.DataModels;
using kpfw.Models;
using kpfw.Services;
using Markdig;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace kpfw.Controllers
{
    public class PageController : Controller
    {
        protected MarkdownPipeline pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().UseCaps().Build();
        readonly DataContext context;

        public PageController(DataContext _context)
        {
            context = _context;
        }

        public IActionResult Index()
        {
            if (RouteData.Values["PageUrl"].ToString().StartsWith("transcript/"))
            {
                string[] transcriptInfo = RouteData.Values["PageUrl"].ToString().Split("/");
                var ep = context.Episodes.Where(x => x.Id == Convert.ToInt32(transcriptInfo[1]));
                if (ep.Count() > 0)
                {
                    var episode = ep.First();
                    return RedirectPermanent($"/Guides/{episode.UrlLabel}/transcript");
                    //return View(new PageModel { PageName = $"Transcript for {episode.Title}", Url = RouteData.Values["PageUrl"].ToString() });
                }
            }

            Page p = context.Pages.Where(x => x.Url == RouteData.Values["PageUrl"].ToString()).FirstOrDefault();
            if (p == null)
                return StatusCode(404);

            return View(new PageModel { PageName = p.Name, Url = p.Url, Title = p.Title, Content = Markdown.ToHtml(p.Content, pipeline) });
        }

        [Route("/HandleError/{code:int}")]
        public IActionResult HandleError(int code)
        {
            switch (code)
            {
                case 404:
                    string url = Request.GetDisplayUrl();
                    return View("~/Views/Shared/404.cshtml", url);
                default:
                    return View("~/Views/Shared/HandleError.cshtml");
            }
        }
    }
}