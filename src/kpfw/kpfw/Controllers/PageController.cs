using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kpfw.DataModels;
using kpfw.Models;
using Microsoft.AspNetCore.Mvc;

namespace kpfw.Controllers
{
    public class PageController : Controller
    {
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

            return View(new PageModel { PageName = "Test Page", Url = RouteData.Values["PageUrl"].ToString() });
        }
    }
}