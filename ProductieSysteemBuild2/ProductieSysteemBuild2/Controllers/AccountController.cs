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
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {

            Roles.GetRolesForUser(User.Identity.Name);
            User.Identity.AuthenticationType.ToString();
            ViewBag.Name = User.Identity.Name.ToString();
            return View();
        }
        [Authorize(Roles = "Admin")]
        public ActionResult CreateUser()
        {
            List<SelectListItem> items = new List<SelectListItem>();

          //  ViewBag.Books = new SelectList(books);

           

            aspnet_Roles roles = new aspnet_Roles();
            foreach (string role in Roles.GetAllRoles())
            {
                items.Add(new SelectListItem { Text = role.ToString(), Value = "0" });
            }


            ViewBag.Roles = new SelectList(items);
           
            
            return View();
            
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]        
        public ActionResult CreateUser(Gebruikers model, string GetRole)
        {
            //.GetRoleBook book = FetchYourBookFromTheId(selectedBookId);
            string a = GetRole;

            //ViewBag.Roles = items;
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
               // string a = form["Roles"].ToString();
               // a.ToString();
                Roles.AddUserToRole(model.UserName,"Admin");

                //manager.AddToRoleAsync(newUser.Id, "Admin");

                //manager.AddClaimAsync(newUser.Id, claim: new Claim(ClaimTypes.Role.ToString(), "Admin"));
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
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(Gebruikers model, string returnUrl)
        {
           
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new IdentityDataContext()));

            

            if (ModelState.IsValid)
            {
                var user = await manager.FindAsync(model.UserName, model.PasswordHash);
                if (user != null)
                {
                    //ClaimsIdentity identity = manager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    FormsAuthentication.SetAuthCookie(model.UserName,true);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/") && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Account");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Foutief wachtwoord of gebruikersnaam!");
                }
            }

            return View(model);
         }

        //private async Task SingInAsync(ApplicationUser user, bool isPersisten)
        //{
        //    var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new IdentityDataContext()));

        //    AuthenticationManager.Unregister(DefaultAuthenticationTypes.ExternalCookie);
        //    var identity =  await manager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
        //    AuthenticationManager.Register(new AuthenticationProperties() { IsPersistent = isPersisten }, identity);

        //}
        [Authorize(Roles = "Admin")]
        public ActionResult RolesView()
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