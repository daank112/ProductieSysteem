using System;
using System.ComponentModel.DataAnnotations;

namespace ProductieSysteemV1._0.Models
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Gebruikersnaam")]
        public string UserName { get; set; }

        [Required]
        
        [Display(Name = "Wachtwoord")]
        public string Password { get; set; }

        [Display(Name = "Bevestig Wachtwoord")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Gebruikerstype")]
        public RolesModel userRoles {get; set;}

        public virtual Userinfo _UserInfo  { get; set; }
    }
    public class RolesModel
    {
        public Guid ApplicationId { get; set; }

        [Key]
        public Guid RoleId { get; set; }

        [Required]
        [StringLength(256)]
        public string RoleName { get; set; }

        [Required]
        [StringLength(256)]
        public string LoweredRoleName { 
            get {
                return RoleName;
            }
            }

        [StringLength(256)]
        public string Description { get; set; }
    }
    
}
