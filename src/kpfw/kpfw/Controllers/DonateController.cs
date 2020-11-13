using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kpfw.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace kpfw.Controllers
{
    public class DonateController : Controller
    {
        private readonly KpfwSettings Settings;

        public DonateController(IConfiguration configuration)
        {
            Settings = configuration.GetSection("Kpfw").Get<KpfwSettings>();
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewData["StripePK"] = Settings.StripePublishableKey;
            return View();
        }

        [HttpPost]
        public IActionResult Index(object model)
        {

            return View();
        }
    }
}
