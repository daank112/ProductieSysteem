using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System;

namespace ProductieSysteemV1._0.Models
{
    public class ApplicationUser : IdentityUser
    {
        //Voeg het model UserInfo toe aan de standaard user identity. Dit geeft de mogelijkheid om extra informatie van de gebruikers op te slaan
        public virtual Userinfo userInfo { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
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
        public virtual DbSet<RolesModel> RolesModel { get; set; }

        public virtual DbSet<UserModel> UserModel { get; set; }
        public virtual DbSet<DayProduction> DayProduction { get; set; }
        public virtual DbSet<WeekProduction> WeekProduction { get; set; }
        public virtual DbSet<G_Rule> G_Rule { get; set; }
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