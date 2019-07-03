using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using kpfw.DataModels;
using kpfw.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace kpfw.Controllers
{
    public class AccountController : Controller
    {
        private int NumTries
        {
            get
            {
                string c = Request.Cookies["InvalidTries"];
                if (c == null)
                    return 0;
                else return Convert.ToInt32(c);
            }
            set
            {
                string c = Request.Cookies["InvalidTries"];
                if (c == null)
                {
                    Response.Cookies.Append("InvalidTries", c, new CookieOptions { Expires = DateTime.Now.AddHours(1) });
                }
                else
                {
                    Response.Cookies.Append("InvalidTries", value.ToString(), new CookieOptions { Expires = DateTime.Now.AddHours(1) });
                }
            }
        }
        private DataContext _context;
        private SignInManager<User> _signInManager;
        private UserManager<User> _userManager;
        public AccountController(DataContext context, UserManager<User> u, SignInManager<User> s)
        {
            _context = context;
            _userManager = u;
            _signInManager = s;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginModel m)
        {
            if (!ModelState.IsValid)
            {
                TempData["LoginModel"] = JsonConvert.SerializeObject(m);
                TempData["LoginError"] = true;
            }
            else
            {
                // Login
                if (!new Regex(@"^[a-zA-Z0-9_]+$").IsMatch(m.UserName) && !new Regex(@"^(([A-Za-z0-9]+_+)|([A-Za-z0-9]+\-+)|([A-Za-z0-9]+\.+)|([A-Za-z0-9]+\++))*[A-Za-z0-9]+@((\w+\-+)|(\w+\.))*\w{1,63}\.[a-zA-Z]{2,6}$").IsMatch(m.UserName))
                {
                    TempData["LoginModel"] = JsonConvert.SerializeObject(m);
                    TempData["LoginError"] = true;
                    TempData["ErrorMessage"] = "Invalid username. Your username must contain only letters, numbers, and the underscore character or must be an email address.";
                    NumTries = NumTries + 1;
                }
                else
                {

                    var user = _context.Users.Where(x => x.UserName == m.UserName).FirstOrDefault();
                    if (user == null)
                    {
                        TempData["LoginModel"] = JsonConvert.SerializeObject(m);
                        TempData["LoginError"] = true;
                        TempData["ErrorMessage"] = "Sorry, Wade couldn't find anyone on file with those credentials. <a href=\"/Contact\">Contact us</a> if you feel this is in error.";
                    }
                    else
                    {
                        await _signInManager.SignInAsync(new User(), false);
                        
                    }
                }
            }

            return Redirect(Request.Form["loginPage"]);
        }
    }
}