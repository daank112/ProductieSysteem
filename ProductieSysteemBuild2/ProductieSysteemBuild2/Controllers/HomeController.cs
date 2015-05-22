using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProductieSysteemBuild2.Models;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ProductieSysteemBuild2.Controllers
{
    public class HomeController : Controller
    {
        
        
        
        // GET: Home
        [Authorize(Roles="Admin")]
        public ActionResult Index()
        {
            
            Roles.GetRolesForUser(User.Identity.Name);
            User.Identity.AuthenticationType.ToString();
            ViewBag.Name = User.Identity.Name.ToString();
            return View();
        }
    }
}