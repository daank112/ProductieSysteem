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
          public virtual DbSet<RolesModel> RolesModel { get; set; }
          public virtual DbSet<UserModel> UserModel { get; set; }

        //public virtual DbSet<G_Regel> G_Regel { get; set; }
        //public virtual DbSet<Gebruikers> Gebruikers { get; set; }
        //public virtual DbSet<GebruikersType> GebruikersType { get; set; }
        //public virtual DbSet<GebruikersTypeRol> GebruikersTypeRol { get; set; }
        //public virtual DbSet<Login> Login { get; set; }
        //public virtual DbSet<Producten> Producten { get; set; }
        //public virtual DbSet<Weekproductie> Weekproductie { get; set; }
        //public virtual DbSet<aspnet_Roles> Roles { get; set; }

    }

}