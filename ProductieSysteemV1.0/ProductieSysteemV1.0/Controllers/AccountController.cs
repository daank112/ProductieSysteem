using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Owin;
using ProductieSysteemV1._0.Models;
using System.Web.Security;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace ProductieSysteemV1._0.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindAsync(model.Email, model.Password);
                if (user != null)
                {
                    await SignInAsync(user, model.RememberMe);
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }

            return View(model);
        }


        [Authorize(Roles="Administrator, Veiling")]
        public ActionResult Registreren()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Registreren(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {

                var user = new ApplicationUser()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    userInfo = new Userinfo
                    {

                        FirstName = model._UserInfo.FirstName,
                        LastName = model._UserInfo.LastName,
                        CompanyName = model._UserInfo.CompanyName,
                        City = model._UserInfo.City,
                        PhoneNumber = model._UserInfo.PhoneNumber,
                        ZipCode = model._UserInfo.ZipCode,
                        HouseNumber = model._UserInfo.HouseNumber,
                        Street = model._UserInfo.Street
                    }
                };
                user.userInfo.Id = user.Id;

                Roles.AddUserToRole(model.UserName, model.userRoles.RoleName);

                IdentityResult result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await SignInAsync(user, isPersistent: false);

                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    AddErrors(result);
                   
                }

            }
                return View(model);
           
        }
        public ActionResult Logout()
        {
            try
            {
                FormsAuthentication.SignOut();
            }
            catch(Exception e)
            {
                e.ToString();
            }
            return RedirectToAction("Login");
        }


       

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, await user.GenerateUserIdentityAsync(UserManager));
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

       

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

       
    }
}