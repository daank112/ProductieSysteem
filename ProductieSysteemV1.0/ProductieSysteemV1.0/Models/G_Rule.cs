using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProductieSysteemV1._0.Models
{
    [Table("tbl_.g_Rule")]
    public class G_Rule
    {
        [Key]
        [Column(Order = 0)]
        public string UserId { get; set; }
        [Key]
        [Column(Order = 1)]
        public int weekId { get; set; }

        [Required(ErrorMessage="Dit veldt is verplicht")]
        public int weekProduction { get; set; }
    }
}