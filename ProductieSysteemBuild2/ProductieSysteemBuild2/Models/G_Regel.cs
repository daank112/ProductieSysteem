namespace ProductieSysteemBuild2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tbl_.G_Regel")]
    public partial class G_Regel
    {
        [Key]
        public int gebruikersId { get; set; }

        public int weekId { get; set; }

        public int weekproductie { get; set; }
    }
}
