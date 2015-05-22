using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProductieSysteemBuild2.Models
{
    public partial class aspnet_Roles
    {
        public aspnet_Roles()
        {
            aspnet_Users = new HashSet<Gebruikers>();
        }

        public Guid ApplicationId { get; set; }

        [Key]
        public Guid RoleId { get; set; }

        [Required]
        [StringLength(256)]
        public string RoleName { get; set; }

        [Required]
        [StringLength(256)]
        public string LoweredRoleName { get; set; }

        [StringLength(256)]
        public string Description { get; set; }

       // public virtual aspnet_Applications aspnet_Applications { get; set; }

        public virtual ICollection<Gebruikers> aspnet_Users { get; set; }
    }
}
