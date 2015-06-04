using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProductieSysteemV1._0.Models
{
    public class DbContextClass : DbContext
    {

        public DbContextClass()
            : base("DefaultConnection")
        {
        }

          //Data context de verbinding tussen de modellen en de tabellen in de database. 
          public virtual DbSet<RolesModel> RolesModel { get; set; }
          public virtual DbSet<UserModel> UserModel { get; set; }
          public virtual DbSet<DayProduction> DayProduction { get; set; }
          public virtual DbSet<WeekProduction> WeekProduction { get; set; }
          public virtual DbSet<G_Rule> G_Rule { get; set; }
         

        

    }

}