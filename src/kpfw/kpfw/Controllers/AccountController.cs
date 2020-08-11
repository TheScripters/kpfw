using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Google.Authenticator;
using kpfw.DataModels;
using kpfw.Models;
using kpfw.Services;
using kpfw.Services.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace kpfw.Controllers
{
    public class AccountController : Controller
    {
        //private readonly IConfiguration Configuration;
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
                    Response.Cookies.Append("InvalidTries", value.ToString(), new CookieOptions { Expires = DateTime.Now.AddHours(1) });
                }
                else
                {
                    Response.Cookies.Append("InvalidTries", value.ToString(), new CookieOptions { Expires = DateTime.Now.AddHours(1) });
                }
            }
        }
        private DataContext _context;
        private readonly KpfwSettings settings;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        public AccountController(DataContext context, UserManager<User> u, SignInManager<User> s, IConfiguration configuration)
        {
            settings = configuration.GetSection("Kpfw").Get<KpfwSettings>();
            _context = context;
            _userManager = u;
            _signInManager = s;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel m)
        {
            if (TempData["Show2FA"] != null)
                TempData.Remove("Show2FA");
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
                        NumTries = NumTries + 1;
                    }
                    else
                    {
                        // check password
                        Auth auth = new Auth(m.Password, user.UserPassword);
                        if (!auth.Compare(out bool isMd5))
                        {
                            TempData["LoginModel"] = JsonConvert.SerializeObject(m);
                            TempData["LoginError"] = true;
                            TempData["ErrorMessage"] = "Sorry, Wade couldn't find anyone on file with those credentials. <a href=\"/Contact\">Contact us</a> if you feel this is in error.";
                            NumTries = NumTries + 1;
                        }
                        if (!String.IsNullOrWhiteSpace(user.TwoFactor))
                        {
                            // user has 2FA enabled so show that before logging them in.
                            TempData["2FAUser"] = $"{user.Id}|{user.UserName}|{user.DisplayName}|{isMd5}|{user.IsActive}|{auth.GetNewHash()}".ToBase64();
                            TempData["Show2FA"] = true;
                        }
                        else
                            await _signInManager.SignInAsync(user, false);
                    }
                }
            }

            string url = Request.Form["loginPage"];
            if (Url.IsLocalUrl(url))
                return Redirect(url);
            else
                return Redirect("/");
        }

        [HttpPost]
        public async Task<ActionResult> Verify2FA()
        {
            // 0 - userId
            // 1 - userName
            // 2 - displayName
            // 3 - md5 (4)
            // 4 - isActive (5)
            // 5 - NewHash (6)
            string[] u = Request.Form["2FAUser"][0].FromBase64().Split('|');
            bool md5 = Convert.ToBoolean(u[3]), isActive = Convert.ToBoolean(u[4]);
            var user = _context.Users.Where(x => x.Id == Convert.ToInt32(u[0])).Single();
            //var u = User.GetPassword(txtUserName.Text);
            bool tfaValid = false;

            if (!Regex.IsMatch(user.TwoFactor, @"^[\d]+$"))
            {
                TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
                if (tfa.ValidateTwoFactorPIN(user.TwoFactor, Request.Form["TwoFactorCode"][0]))
                    tfaValid = true;
            }
            else
            {
                var client = new AuthyClient(settings.AuthyApiKey);
                if (client.VerifyToken(Convert.ToInt32(user.TwoFactor), Convert.ToInt32(Request.Form["TwoFactorCode"][0])))
                    tfaValid = true;
            }

            if (tfaValid)
            {
                if ((md5 && !isActive) || isActive)
                {
                    await _signInManager.SignInAsync(user, false);
                    //FormsAuthentication.RedirectFromLoginPage(u[1], false);
                    if (NumTries > 0)
                    {
                        Response.Cookies.Delete("InvalidTries");
                    }

                    if (md5)
                    {
                        user.UserPassword = u[5];
                        user.EmailConfirmation = Guid.NewGuid();
                        user.IsActive = true;
                        _context.Users.Update(user);
                    }
                }
                else
                {
                    TempData["2FAError"] = true;
                    //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "LoginError", "$.magnificPopup.open({ items: { src: '#loginModalPopup' }, prependTo:'form#aspnetForm', closeOnBgClick: false });", true);
                    TempData["2FAErrorMessage"] = "Sorry, there's a problem with your account. <a href=\"/Contact\">Contact us</a> to get it resolved.";
                    //FailureText.Visible = true;
                    NumTries = NumTries + 1;
                }
            }
            else
            {
                // Display error and re-display popup
                TempData["2FAError"] = true;
                TempData["2FAErrorMessage"] = "Invalid token. Please try again.";
                //TwoFactorError.InnerText = "Invalid token. Please try again.";
                //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "changePassword", "$.magnificPopup.open({ items: { src: '#twoFAModal' }, prependTo:'form#aspnetForm', closeOnBgClick: false });", true);
            }

            string url = Request.Form["loginPage"];
            if (Url.IsLocalUrl(url))
                return Redirect(url);
            else
                return Redirect("/");
        }

        public async Task<ActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return Redirect("~/");
        }
    }
}