using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace kpfw.Controllers
{
    public class CapsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}