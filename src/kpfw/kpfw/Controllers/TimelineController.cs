using kpfw.DataModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace kpfw.Controllers
{
    public class TimelineController(DataContext dataContext) : Controller
    {
        public IActionResult Index()
        {
            var timelineEntries = dataContext.Timeline.OrderBy(t => t.Date).ToList();
            return View(timelineEntries);
        }
    }
}
