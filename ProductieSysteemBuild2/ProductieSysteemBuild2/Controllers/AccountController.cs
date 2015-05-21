using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Owin;
using ProductieSysteemBuild2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ProductieSysteemBuild2.Controllers
{
    public class AccountController : Controller
    {
        IdentityDataContext context = new IdentityDataContext();
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CreateUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateUser(Gebruikers model)
        {
            try
            {
                var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new IdentityDataContext()));

                var newUser = new ApplicationUser()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    EmailConfirmed = false,
                };
                manager.Create(newUser, model.PasswordHash);
                //manager.AddToRoleAsync(newUser.Id, "Admin");

                manager.AddClaimAsync(newUser.Id, claim: new Claim(ClaimTypes.Role.ToString(), "Admin"));
                //UserManager.AddClaimAsync(user, claim: new Claim(ClaimTypes.Role.ToString(), "Admin"));


            }
            catch(Exception e)
            {
                e.ToString();
            }
                
            return View(model);
        }
        
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Login(Gebruikers model, string returnUrl)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new IdentityDataContext()));
            
            if (ModelState.IsValid)
            {
                var user = await manager.FindAsync(model.UserName, model.PasswordHash);
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName,true);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/") && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Foutief wachtwoord of gebruikersnaam!");
                }
            }

            return View(model);
         }

        
        public ActionResult Roles()
        {
            var roles = context.Roles.ToList(); 
            return View(roles);
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
       
    }
}