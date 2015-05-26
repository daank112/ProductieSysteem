using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProductieSysteemBuild2.Models;
using System.Globalization;


namespace ProductieSysteem.Controllers
{
    public class ProductieController : Controller
    {
        DataContext db = new DataContext();
        // GET: Dashboard
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ProductieOpgeven(Weekproductie model)
        {
            ViewData["Message"] = "Productie opgeven";
            // return View();
            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem { Text = "  ", Value = "0" });
            items.Add(new SelectListItem { Text = "Komkommer", Value = "1" });
            items.Add(new SelectListItem { Text = "Tomaat", Value = "2" });
            items.Add(new SelectListItem { Text = "Paprika", Value = "3" });

            ViewBag.Dagdelen = items;

            string week = DateTime.Now.ToLongDateString();
            

            string aantal;
            int weekid = GetWeekNumber(DateTime.Today);
            ViewBag.Date = weekid;
            model.weekId = weekid;
            return View();
        }

        public static int GetWeekNumber(DateTime dtPassed)
        {
            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            int weekNum = ciCurr.Calendar.GetWeekOfYear(dtPassed, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
             return weekNum;
        }

     /*   public ActionResult WeekProductie()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult WeekProductie([Bind(Include="")*/
        public ActionResult Week()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            for(int i = 0; i <= 100; i++){
                items.Add(new SelectListItem
                {
                    Text = i.ToString(),
                    Value = i.ToString()
                });
            }


            ViewBag.Procenten = items;
            
            @ViewBag.CurrentWeek = GetWeekNumber(DateTime.Today);
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Week([Bind(Include = "weekId,maandag,dinsdag,woensdag,donderdag,vrijdag,zaterdag,zondag,productType")] Weekproductie weekproductie)
        {
            weekproductie.maandag = "1";
            db.Weekproductie.Add(weekproductie);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        
       
        public ActionResult ProductieBijstellen()
        {

            ViewData["Message"] = "Productie bijstellen";
            return View();
        }


    }
}