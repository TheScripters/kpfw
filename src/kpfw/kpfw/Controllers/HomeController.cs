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
    public class HomeController : Controller
    {
        private DataContext _context;
        public HomeController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var now = DateTime.UtcNow;
            var events = _context.Timeline.Where(x => x.Date.Day == now.Day && x.Date.Month == now.Month);
            List<TimelineViewModel> l = new List<TimelineViewModel>();
            foreach (var t in events)
                l.Add(new TimelineViewModel { Year = t.Date.Year, Message = t.Message });

            return View("Home", new HomeViewModel { Timeline = l });
        }

        public IActionResult Error(string code)
        {
            return View("Error", code);
        }
    }
}