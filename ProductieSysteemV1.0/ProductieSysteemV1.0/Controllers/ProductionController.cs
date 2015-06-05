using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ProductieSysteemV1._0.Models;
using System.Web.Security;
using System.Globalization;

namespace ProductieSysteemV1._0.Controllers
{
    [Authorize(Roles="Teler")]
    public class ProductionController : Controller
    {
        DbContextClass db = new DbContextClass();
        
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Week()
        {
            //Maak een nieuwe variable aan voor de week view model
            WeekViewModel week = new WeekViewModel();

            //Haal het huidige weeknummer op.
            var currentCulture = CultureInfo.CurrentCulture;
            var weekNo = currentCulture.Calendar.GetWeekOfYear(DateTime.Today, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
           
            //Plaats het huidge weeknummer in de weekview model.
            week.CurrentWeek = weekNo;

            return View(week);
        }

        //Deze functie wordt aangeroepen wanneer de gebruiker het form submit. 
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Week(WeekViewModel model)
        {
            //Controleer of er geldige waarde zijn ingevoerd.
            if(ModelState.IsValid)
            {
                string UserId = User.Identity.GetUserId().ToString();
                int _weekId = Convert.ToInt32("" + model.CurrentWeek + DateTime.Now.Year);
                //Haal alle ingevoerde weken van de gebruiker op. 
                var weekInserted = from w in db.G_Rule
                                   where w.UserId == UserId
                                   select w.weekId;
                //Komt het weeknummer al voor in de database ? return een bericht. 
                if (weekInserted.Contains(_weekId) == true)
                {
                    ModelState.AddModelError("", "U heeft al een verwachting opgegeven voor deze week. Selecteer een andere week");
                    return View(model);
                }
                else
                {
                    //Haal de userid op en plaats deze in model g_regel als gebruikers id.
                    model.g_Rule.UserId = UserId;
                    model.weekProduction.userId = User.Identity.GetUserId().ToString();
                    ////Haal het weeknummer op. Bij het weeknummer wordt het jaartal bv 2015 toegevoegd. Zo onstaat er voor elke gebruiker een unieke week
                    //Voeg in de model g regel en weekproduction het weeknummer in.
                    model.g_Rule.weekId = _weekId;
                    model.weekProduction.weekId = _weekId;
                    //Voeg het model toe aan de database
                    db.WeekProduction.Add(model.weekProduction);
                    db.G_Rule.Add(model.g_Rule);
                    //Sla de gegevens op in de database
                    db.SaveChanges();
                    //Navigeer naar de volgende pagina met in de url het weekid
                    return RedirectToAction("Day", "Production", new { week = _weekId });
                }
            }
            else
            {
                ModelState.AddModelError("", "Erg ging iets fout... probeer het nogmaals");
            }
            return View(model);
        }

        public ActionResult Day(int? week)
        {
            if (week == null)
            {
                return RedirectToAction("Week", "Production");
            }
            else
            {
                return View();
            }

        }

        public ActionResult EditDay(DayViewModel model, int? week)
        {
            string userID = User.Identity.GetUserId();


           

            return View();


        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Day(IList<DayProduction> model, int? week)
        {
            string userID = User.Identity.GetUserId();

            if (ModelState.IsValid)
            {
                
               
                if (week == null)
                {
                    ModelState.AddModelError("", "Erg ging iets fout... probeer het nogmaals");
                    return View();
                }
              
                      
                   
                    
               
                else
                {
                    var dayExitsts = from d in db.DayProduction
                                     where (d.userId == userID) && (d.weekId == week)
                                     select d.weekId;
                    //Komt het weeknummer al voor in de database ? return een bericht. 
                    if (dayExitsts.Contains(week.Value) == true)
                    {
                        ModelState.AddModelError("", "Er is al een verwachting voor deze week opgegeven!");
                        return View(model);
                    }
                    else
                    {
                        for (int i = 0; i < model.Count; i++)
                        {
                            if(model[i].Total > 0)
                            {
                                model[i].weekId = week.Value;
                                model[i].userId = userID;
                                db.DayProduction.Add(model[i]);
                                db.SaveChanges();
                            }
                            
                        }
                        return RedirectToAction("Index");
                    }
                }
            }
            return View();
        }

    }
}