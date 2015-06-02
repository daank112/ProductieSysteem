using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System;

namespace ProductieSysteemV1._0.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public virtual Userinfo userInfo { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        
        
    }
    

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public virtual DbSet<Userinfo> userInfo { get; set; }
        
      
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        //Om de modellen aan de preflix die vanuit school gesteld wordt te laten te voldoen worden hier de namen van de 
        //tabellen aangepast. Dit gebeurdt wanneer er wordt gecontroleerd of de tabbellen die benodigd zijn voor het functioneren
        //van het user authentication system. 
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUser>()
                .ToTable("users", "tbl_");

            modelBuilder.Entity<IdentityRole>()
                .ToTable("userType", "tbl_");

            modelBuilder.Entity<IdentityUserRole>()
                .ToTable("userTypeRole", "tbl_");

            modelBuilder.Entity<IdentityUserClaim>()
                .ToTable("userClaims", "tbl_");

            modelBuilder.Entity<IdentityUserLogin>()
                .ToTable("userLogins", "tbl_");

            modelBuilder.Entity<Userinfo>()
                .ToTable("userInfo", "tbl_");
        }
    }
}