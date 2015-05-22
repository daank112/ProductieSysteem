namespace ProductieSysteemBuild2.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;

    public partial class DataContext : DbContext
    {
        public DataContext()
            : base("name=DefaultConnection")
        {
        }

        public virtual DbSet<G_Regel> G_Regel { get; set; }
        public virtual DbSet<Gebruikers> Gebruikers { get; set; }
        public virtual DbSet<GebruikersType> GebruikersType { get; set; }
        public virtual DbSet<GebruikersTypeRol> GebruikersTypeRol { get; set; }
        public virtual DbSet<Login> Login { get; set; }
        public virtual DbSet<Producten> Producten { get; set; }
        public virtual DbSet<Weekproductie> Weekproductie { get; set; }
        public virtual DbSet<aspnet_Roles> Roles { get; set; }

           
    }
    
    
}
