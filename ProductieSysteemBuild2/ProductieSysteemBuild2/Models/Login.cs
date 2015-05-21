namespace ProductieSysteemBuild2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tbl_.Login")]
    public partial class Login
    {
        [Key]
        public int gebruikersId { get; set; }

        [Required]
        [StringLength(255)]
        public string gebruikersnaam { get; set; }

        [StringLength(255)]
        public string wachtwoord { get; set; }
    }
}
