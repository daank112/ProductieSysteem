namespace ProductieSysteemBuild2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tbl_.Producten")]
    public partial class Producten
    {
        [Key]
        public int productNr { get; set; }

        [Required]
        [StringLength(255)]
        public string productType { get; set; }
    }
}
