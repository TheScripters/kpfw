using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kpfw.Models;
using Microsoft.AspNetCore.Mvc;

namespace kpfw.Controllers
{
    public class PageController : Controller
    {
        public IActionResult Index()
        {
            return View(new PageModel { PageName = "Test Page", Url = RouteData.Values["PageUrl"].ToString() });
        }
    }
}