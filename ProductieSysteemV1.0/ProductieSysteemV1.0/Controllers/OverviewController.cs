using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProductieSysteemV1._0.Models;
using System.Web.Security;
using Microsoft.AspNet.Identity;

namespace ProductieSysteemV1._0.Controllers
{
    public class OverviewController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();


        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId().ToString();

            if(User.IsInRole("teler"))
            {
                //Selecteer alle weken waar de gebruikersid in voorkomt. 
                //Neem er hier 10 van en sorteer deze van hoog naar laag.
                var allWeek = (from s in db.G_Rule
                               where s.UserId == userId
                               select s)
                              .Take(10)
                              .OrderByDescending(x => x.weekId);
                return View(allWeek);
            }
            if (User.IsInRole("veiling"))
            {
                var allWeek = (from s in db.G_Rule
                               select s)
                              .Take(10)
                              .OrderByDescending(x => x.weekId);
                return View(allWeek);
            }
            return View();
            
        }
        public ActionResult Telers()
        {
            string userId = User.Identity.GetUserId().ToString();
            var roles = db.RolesModel.All(x => x.RoleName == "teler");
            //var role = db.Roles.SingleOrDefault(m => m.Name == "teler");
            
            //var usersInRole = db.Users.Where(m => m.Roles.Any(r => r.RoleId == role.Id));
           
            
            return View();
        }
        public ActionResult Veiling(int week)
        {            
            var result = (from g in db.G_Rule
                          join wp in db.WeekProduction on g.weekId equals wp.weekId
                          where (g.weekId == week) && (wp.weekId == week)
                          select new
                          {
                              WeekProduction = wp,
                              
                              G_Rule = g
                          });

            int totalvalues = result.Count();

            List<OverviewModel> list = new List<OverviewModel>(){
                    new OverviewModel{
                        weekProduction = new WeekProduction{
                                        monday = calcPercent((result.Sum(x => x.G_Rule.weekProduction)),(result.Sum(x => x.WeekProduction.monday) / totalvalues)),
                                        tuesday = calcPercent((result.Sum(x => x.G_Rule.weekProduction)),(result.Sum(x => x.WeekProduction.tuesday) / totalvalues)),
                                        wednesday = calcPercent((result.Sum(x => x.G_Rule.weekProduction)),(result.Sum(x => x.WeekProduction.wednesday) / totalvalues)),
                                        thursday = calcPercent((result.Sum(x => x.G_Rule.weekProduction)),(result.Sum(x => x.WeekProduction.thursday) / totalvalues)),
                                        friday = calcPercent((result.Sum(x => x.G_Rule.weekProduction)),(result.Sum(x => x.WeekProduction.friday) / totalvalues)),
                                        saturday = calcPercent((result.Sum(x => x.G_Rule.weekProduction)),(result.Sum(x => x.WeekProduction.saturday) / totalvalues)),
                                        sunday = calcPercent((result.Sum(x => x.G_Rule.weekProduction)),(result.Sum(x => x.WeekProduction.sunday) / totalvalues)),
                        },
                        rule = new G_Rule{
                            weekProduction = result.Sum(x => x.G_Rule.weekProduction)
                        },
                        //Maak een lege list aan als item [03]. Hier plaatsen ze we zomenteen de dag gegevens in.
                        dayProductionList = new List<DayProduction>{
                            
                        }

                    }

                };
            var dayTotal = (from g in db.DayProduction
                            where g.weekId == week
                            select new
                            {
                                DayProduction = g
                            }).GroupBy(x => x.DayProduction.day);

            
          

            foreach (var key in dayTotal)
            {
                int numberOfRecords = key.Count();
                list[0].dayProductionList.Add(new DayProduction { C_350 = (key.Sum(x => x.DayProduction.C_350) / numberOfRecords), C350___400 = (key.Sum(x => x.DayProduction.C350___400) / numberOfRecords), C400___500 = (key.Sum(x => x.DayProduction.C400___500) / numberOfRecords), C500___650 = (key.Sum(x => x.DayProduction.C500___650) / numberOfRecords), C650___750 = (key.Sum(x => x.DayProduction.C650___750) / numberOfRecords), C750_ = (key.Sum(x => x.DayProduction.C750_) / numberOfRecords), day = key.FirstOrDefault().DayProduction.day.ToString() });
            }
                ViewBag.CurrentWeek = week;
                return View(list);
               

            return View(list);
        }

        

        public ActionResult ProductionTeler(OverviewModel model, int week)
        {
            if (week == null)
            {
                return RedirectToAction("index", "production");
            }
            else
            {

            
            string userId = User.Identity.GetUserId().ToString();

            //Haal alle weken op die de gebruiker heeft ingevoerd.
            var weekExists = from w in db.G_Rule
                             where w.UserId == userId
                             select w.weekId;
            // kijk of de week die word gevraagd bestaat.
            if (weekExists.Contains(week) == false)
            {
                //wanneer dit niet het geval is keer terug naar de hompage.
                return RedirectToAction("index", "overview");
            }
            else
            {
                //Haal van de gebruiker alle ingevoerde weekgegevens op uit de database. en plaats deze in de variable result
                var result = (from g in db.G_Rule
                              join wp in db.WeekProduction on g.weekId equals wp.weekId
                              join t4 in db.DayProduction on new { Userid = userId, Week = week } equals new { Userid = t4.userId, Week = t4.weekId }
                              where (g.UserId == userId) && (g.weekId == week) && (wp.userId == userId)

                              select new
                              {
                                  WeekProduction = wp,
                                  DayProduction = t4,
                                  G_Rule = g
                              });

                // Controleer of result gegevens opgehaald heeft.
               if(result.Any())
               {
                //Maak een list aan obv van het overview model. plaats hier vervolgende de gegebens uit linq in. 
                List<OverviewModel> list = new List<OverviewModel>(){
                    new OverviewModel{
                        weekProduction = new WeekProduction{
                                        monday = calcPercent(result.FirstOrDefault().G_Rule.weekProduction, result.FirstOrDefault().WeekProduction.monday.Value),
                                        tuesday = calcPercent(result.FirstOrDefault().G_Rule.weekProduction, result.FirstOrDefault().WeekProduction.tuesday.Value), 
                                        wednesday = calcPercent(result.FirstOrDefault().G_Rule.weekProduction, result.FirstOrDefault().WeekProduction.wednesday.Value), 
                                        thursday = calcPercent(result.FirstOrDefault().G_Rule.weekProduction, result.FirstOrDefault().WeekProduction.thursday.Value), 
                                        friday = calcPercent(result.FirstOrDefault().G_Rule.weekProduction, result.FirstOrDefault().WeekProduction.friday.Value), 
                                        saturday = calcPercent(result.FirstOrDefault().G_Rule.weekProduction, result.FirstOrDefault().WeekProduction.saturday.Value), 
                                        sunday = calcPercent(result.FirstOrDefault().G_Rule.weekProduction, result.FirstOrDefault().WeekProduction.sunday.Value) 
                        },
                        rule = new G_Rule{
                            UserId = result.FirstOrDefault().G_Rule.UserId,
                            weekId = result.FirstOrDefault().G_Rule.weekId,
                            weekProduction = result.FirstOrDefault().G_Rule.weekProduction
                        },
                        //Maak een lege list aan als item [03]. Hier plaatsen ze we zomenteen de dag gegevens in.
                        dayProductionList = new List<DayProduction>{
                        }

                    }

                };
                //Met een foreach loop voeg ik de gegevens van de dagen toe. 
                foreach (var key in result)
                {
                    
                    list[0].dayProductionList.Add(new DayProduction { C_350 = key.DayProduction.C_350, C350___400 = key.DayProduction.C350___400, C400___500 = key.DayProduction.C400___500, C500___650 = key.DayProduction.C500___650, C650___750 = key.DayProduction.C650___750, C750_ = key.DayProduction.C750_, day = key.DayProduction.day, userId = key.DayProduction.userId });
                }
                ViewBag.CurrentWeek = week;
                return View(list);
               }
               else
               {
                   return RedirectToAction("index", "overview");
               }
            }

            }

        }
       
        public int calcPercent(int? a, int?  b)
        {
            int total = (a.Value / 100) * b.Value;
            return total;
        }
    }
}