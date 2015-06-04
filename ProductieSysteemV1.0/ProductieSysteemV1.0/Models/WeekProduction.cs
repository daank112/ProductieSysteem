using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProductieSysteemV1._0.Models
{
    [Table("tbl_.weekProduction")]
    public class WeekProduction
    {
        [Key]
        [Column(Order = 0)]
        public int weekId { get; set; }

        [Key]
        [Column(Order = 1)]
        public string userId { get; set; }

        [Range(0, 101)]
        public int? monday { get; set; }
        [Range(0, 101)]
        public int? tuesday { get; set; }
        [Range(0, 101)]
        public int? wednesday { get; set; }
        [Range(0, 101)]
        public int? thursday { get; set; }
        [Range(0, 101)]
        public int? friday { get; set; }
        [Range(0, 101)]
        public int? saturday { get; set; }
        [Range(0, 101)]
        public int? sunday { get; set; }
        [Range(0, 101)]
        public int? productType { get; set; }
    }

    public enum DaysOfWeek
    {
        Maandag = 1,
        Dinsdag = 2,
        Woensdag = 3,
        Donderdag = 4,
        Vrijdag = 5,
        Zaterdag = 6,
        Zondag = 7
    }
}