using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace ProductieSysteemBuild2.Models
{
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            
            // Add custom user claims here
            return userIdentity;
        }
    }
    public class IdentityDataContext : IdentityDbContext<ApplicationUser>
    {
      
       
        public IdentityDataContext()
            : base("DefaultConnection")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUser>()
                .ToTable("Gebruikers", "tbl_");

            modelBuilder.Entity<IdentityRole>()
                .ToTable("GebruikersType", "tbl_");

            modelBuilder.Entity<IdentityUserRole>()
                .ToTable("GebruikersTypeRol", "tbl_");

            modelBuilder.Entity<IdentityUserClaim>()
                .ToTable("UserClaims", "tbl_");

            modelBuilder.Entity<IdentityUserLogin>()
                .ToTable("UserLogins", "tbl_");
        }
    }
}