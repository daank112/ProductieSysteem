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
        public DateTime dt;

        // GET: Dashboard
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ProductieOpgeven(Weekproductie model, int deWeek)
        {
            if (deWeek != null)
            {
                ViewBag.Date = deWeek;
                model.weekId = deWeek;
            }
            else
            {
                int weekid = GetWeekNumber(DateTime.Today);
                ViewBag.Date = weekid;
                model.weekId = weekid;

            }
            ViewData["Message"] = "Productie opgeven";
            // return View();
            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem { Text = "  ", Value = "0" });
            items.Add(new SelectListItem { Text = "Komkommer", Value = "1" });
            items.Add(new SelectListItem { Text = "Tomaat", Value = "2" });
            items.Add(new SelectListItem { Text = "Paprika", Value = "3" });

            ViewBag.Dagdelen = items;
            return ViewBag;
        }

           // string week = DateTime.Now.ToLongDateString();
            

//string aantal;

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


       // public int cWeek = Convert.ToInt16(eCore.SqlHelper.ExecuteScalar(conn, CommandType.Text, sSqlSelect));
        public int CurrentWeek;

       // public DateTime CurrentSetDate = DateTime.Now;
        public ActionResult Week (string deWeek)
        {
            if (dt.ToString() == "1-1-0001 00:00:00")
            {
                dt = DateTime.Now;
            }

            
            if (deWeek == "VorigeWeek")
            {
               // @ViewBag.CurrentWeek = GetWeekNumber(CurrentSetDate.AddDays(+7));
               // CurrentSetDate = CurrentSetDate.AddDays(+7);
                //@ViewBag.CurrentWeek = CurrentWeek -= 1;
               // @ViewBag.CurrentWeek = a;
                //DateTime LastMonday = DateTime.Now.AddDays(-1);
                //while (LastMonday.DayOfWeek != DayOfWeek.Monday)
                //    LastMonday = LastMonday.AddDays(-1);
                dt.AddDays(7);
                dt.ToString();
                //@ViewBag.currentWeek = LastMonday.AddDays(-1); 
                //@ViewBag.currentWeek = LastMonday.AddDays(-1); 
                   
            }

            if (deWeek == "VolgendeWeek")
            {
               // @ViewBag.GetWeekNumber += 1;
                //int nextWeek = CurrentWeek += 1;
               //@ViewBag.CurrentWeek = GetWeekNumber(DateTime.Now) + 1;
                //@ViewBag.CurrentWeek = GetWeekNumber(CurrentSetDate.AddDays(+7));
                //CurrentSetDate = CurrentSetDate.AddDays(+7);
                DateTime nextMonday = DateTime.Now.AddDays(1);
                while (nextMonday.DayOfWeek != DayOfWeek.Monday)
                    nextMonday = nextMonday.AddDays(1);
                //@ViewBag.currentWeek = next; 
            }

            else
            {
                CurrentWeek = GetWeekNumber(DateTime.Today);
                @ViewBag.CurrentWeek = GetWeekNumber(DateTime.Today);
            }

            List<SelectListItem> items = new List<SelectListItem>();
            for(int i = 0; i <= 100; i++){
                items.Add(new SelectListItem
                {
                    Text = i.ToString(),
                    Value = i.ToString()
                });
            }


            ViewBag.Procenten = items;
          


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