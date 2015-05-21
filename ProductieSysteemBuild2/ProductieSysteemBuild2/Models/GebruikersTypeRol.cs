namespace ProductieSysteemBuild2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tbl_.GebruikersTypeRol")]
    public partial class GebruikersTypeRol
    {
        [Key]
        [Column(Order = 0)]
        public string UserId { get; set; }

        [Key]
        [Column(Order = 1)]
        public string RoleId { get; set; }

        [StringLength(128)]
        public string IdentityUser_Id { get; set; }

        public virtual Gebruikers Gebruikers { get; set; }

        public virtual GebruikersType GebruikersType { get; set; }
    }
}
