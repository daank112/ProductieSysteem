namespace ProductieSysteemBuild2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tbl_.Weekproductie")]
    public partial class Weekproductie
    {
        [Key]
        public int weekId { get; set; }

        [StringLength(255)]
        public string maandag { get; set; }

        [StringLength(255)]
        public string dinsdag { get; set; }

        [StringLength(255)]
        public string woensdag { get; set; }

        [StringLength(255)]
        public string donderdag { get; set; }

        [StringLength(255)]
        public string vrijdag { get; set; }

        [StringLength(255)]
        public string zaterdag { get; set; }

        [StringLength(255)]
        public string zondag { get; set; }

        public int? productType { get; set; }
    }
}
