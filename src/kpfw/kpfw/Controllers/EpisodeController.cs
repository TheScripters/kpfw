using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using kpfw.DataModels;
using kpfw.Models;
using Microsoft.AspNetCore.Mvc;

namespace kpfw.Controllers
{
    public class EpisodeController : Controller
    {
        private DataContext _context;
        public EpisodeController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            if (RouteData.Values["Episode"] == null)
            {
                var eps = _context.Episodes.ToList();
                List<EpisodeViewModel> model = new List<EpisodeViewModel>();
                foreach (var ep in eps)
                    model.Add(new EpisodeViewModel(ep));

                return View("EpisodeIndex", model);
            }
            else
            {
                var ep = _context.Episodes.Where(x => x.UrlLabel == RouteData.Values["Episode"].ToString()).FirstOrDefault();
                if (ep == null)
                    return Redirect("/Guides");

                return View("Episode", new EpisodeViewModel(ep));
            }
        }
    }
}