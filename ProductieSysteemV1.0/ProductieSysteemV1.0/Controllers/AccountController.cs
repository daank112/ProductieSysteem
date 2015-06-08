using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using ProductieSysteemV1._0.Models;
using System.Web.Security;
using System.Net.Mail;


namespace ProductieSysteemV1._0.Controllers
{
 [Authorize]
    public class AccountController : Controller
    {
        
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
            //Controleer of alle velden zijn ingevuld. 
            if (ModelState.IsValid)
            {
                //Zoek de gebruiker met de gebruikersnaam en het wachtwoord.
                var user = await UserManager.FindAsync(model.Email, model.Password);
                //Als deze is gevonden log de user in.
                if (user != null)
                {
                    await SignInAsync(user, model.RememberMe);
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    //Wanneer dit niet het geval is geeft de error
                    ModelState.AddModelError("", "Ongeldige gebruikersnaam of wachtwoord");
                }
            }

            return View(model);
        }


        public ActionResult Registreren()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Registreren(RegisterViewModel model)
        {
            //Controleer of alle velden zijn ingevuld. 
            if (ModelState.IsValid)
            {
                //Maak een variable user aan. Hier komen vervolgende de gegevens van de nieuwe user in. 
                var user = new ApplicationUser()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    userInfo = new Userinfo
                    {
                        //De gegevens voor de zelf toegevoegde table userinfo
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

                //voeg de user toe aan het systeem
                IdentityResult result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    //Voeg de gebruiker toe aan een rol uit de selectlist
                    Roles.AddUserToRole(model.UserName, model.userRoles.RoleName);

                    //Het versturen van de gebruikersnaam en wachtwoord via de maiul
                    var body = "<p>Dear User,</p><p> You can login now with Username:{0} Password: {1}</p>";
                    var message = new MailMessage();
                    message.To.Add(new MailAddress(model.Email));  // replace with valid value 
                    message.From = new MailAddress("daankleindop@outlook.com");  // replace with valid value
                    message.Subject = "Your email subject";
                    message.Body = string.Format(body, model.Email.ToString(), model.Password.ToString());
                  
                    message.IsBodyHtml = true;

                    using (var smtp = new SmtpClient())
                    {
                        //Verstuur mail bericht naar email
                        await smtp.SendMailAsync(message);
                        //Keer terug naar het dashboar
                        return RedirectToAction("Index", "Dashboard");
                    }
                }
                else
                {
                    //Zijn er errors ? laat deze zien.
                    AddErrors(result);
                }
            }
                ModelState.AddModelError("", "Er ging iets niet helemaal goed. Probeer het opnieuw");
                return View(model);
        }
        public ActionResult Logout()
        {     
            //Verwijder de login cookie
            AuthenticationManager.SignOut();
            //Keer terug naar de login pagina
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
                return RedirectToAction("Index", "Dashboard");
            }
        }

       
    }
}