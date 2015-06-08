using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductieSysteemV1._0.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        // GET: Dashboard
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        public ActionResult Contact()
        {
            return View();
        }
    }
}