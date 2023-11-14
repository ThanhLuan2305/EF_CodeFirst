using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EF_CodeFirst.ViewModel;
using EF_CodeFirst.Identity;
using System.Web.Helpers;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using EF_CodeFirst.Models;

namespace EF_CodeFirst.Controllers
{
    public class AcountController : Controller
    {
        // GET: Acount
        [ActionName("Register")] // 1 số trường hợp actionname có thể khác 
        public ActionResult RegisterUser()
        {
            //throw new Exception("Error");
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterVM rg)
        {
            if(ModelState.IsValid)
            {
                var appDbContext = new AppDbContext();
                var userStore = new AppUserStore(appDbContext);
                var userManager = new AppUserManager(userStore);
                var passwdHash = Crypto.HashPassword(rg.Password);
                var user = new AppUser()
                {
                    Email = rg.Email,
                    UserName = rg.Username,
                    PasswordHash = passwdHash,
                    City = rg.City,
                    Birthday = rg.DateOfBirth,
                    Addess = rg.Address,
                    PhoneNumber = rg.Mobile
                };
                IdentityResult identityResult = userManager.Create(user);
                if(identityResult.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Customer");

                    this.LoginUser(userManager, user);
                }
            }
            else
            {
                ModelState.AddModelError("New Error", "Invalid Data");
                return View();
            }
            return RedirectToAction("Index", "Home");
        }
        //[OverrideExceptionFilters]
        public ActionResult Login()
        {
            throw new Exception("Error");
            return View();
        }
        public string nameUser;
        [HttpPost]
        public ActionResult Login(LoginVM lg)
        {
            nameUser = lg.Username;
            var appDbContext = new AppDbContext();
            var userStore = new AppUserStore(appDbContext);
            var userManager = new AppUserManager(userStore);
            var user = userManager.Find(lg.Username, lg.Password);
            if(user != null)
            {
                this.LoginUser(userManager,user);
                if(userManager.IsInRole(user.Id,"Admin"))
                {
                    return RedirectToAction("Index", "Home", new { area = "Admin"});
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
             else
             {
                 ModelState.AddModelError("New Error", "Invalid UserName and Password");
                 return View();
             }
        }
        [NonAction]
        public void LoginUser(AppUserManager userManager, AppUser user)
        {
            var authenManager = HttpContext.GetOwinContext().Authentication;
            var userIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
            authenManager.SignIn(new AuthenticationProperties(), userIdentity);
        }
        public ActionResult Logout()
        {
            var authenManager = HttpContext.GetOwinContext().Authentication;
            authenManager.SignOut();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Profile()
        {
            var appDbContext = new AppDbContext();
            var userStore = new AppUserStore(appDbContext);
            var userManager = new AppUserManager(userStore);
            var user = userManager.FindById(User.Identity.GetUserId());
            var profile = new AppUser()
            {
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Birthday = user.Birthday,
                Addess = user.Addess,
                City = user.City,
            };
            return View(profile);
        }
    }
}