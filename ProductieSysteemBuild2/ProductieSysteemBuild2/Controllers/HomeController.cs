using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductieSysteemBuild2.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        [Authorize(Roles="Admin")]
        public ActionResult Index()
        {

            
            User.Identity.AuthenticationType.ToString();
            ViewBag.Name = User.Identity.Name.ToString();
            return View();
        }
    }
}