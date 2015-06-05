using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

   
namespace ProductieSysteemV1._0.Models
{
    [Table("tbl_.Users")]
    public class UserModel
    {
        [Key]
        public string Id { get; set; }

        [StringLength(256)]
        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        [DisplayName("Wachtwoord")]
        public string PasswordHash { get; set; }

        public string SecurityStamp { get; set; }

        public string PhoneNumber { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public bool TwoFactorEnabled { get; set; }

        public DateTime? LockoutEndDateUtc { get; set; }

        public bool LockoutEnabled { get; set; }

        public string Role { get; set; }

        public int AccessFailedCount { get; set; }

        [Required]
        [DisplayName("Gebruikersnaam")]
        [StringLength(256)]
        public string UserName { get; set; }

       
    }
    public class Userinfo
    {
        [Key]
        public string Id {get;set;}

        [DisplayName("Voornaam")]
        public string FirstName { get; set; }
        [DisplayName("Achternaam")]
        public string LastName { get; set; }
        [DisplayName("Bedrijfsnaam")]
        public string CompanyName { get; set; }
        [DisplayName("Straat")]
        public string Street { get; set; }
        [DisplayName("Huisnummer")]
        public int HouseNumber { get; set; }
        [DisplayName("Stad")]
        public string City { get; set; }
        [DisplayName("Postcode")]
        public string ZipCode { get; set; }
        [DisplayName("Telefoon nummer")]
        public string PhoneNumber { get; set; }
    }
    
   
    
}