using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ProductieSysteemV1._0.Models
{
    [Table("tbl_.dayProduction")]
    public class DayProduction
    {
        [Key]
        [Column(Order = 0)]
        public int dayId { get; set; }

        
        [Column(Order = 1)]
        public int weekId { get; set; }

        public string userId { get; set; }

        public string day { get; set; }

        [Column("-350")]
        [Display(Name = "< 350")]
        public int? C_350 { get; set; }

        [Column("350 - 400")]
        [Display(Name = "350 - 400")]
        public int? C350___400 { get; set; }

        [Column("400 - 500")]
        [Display(Name = "400 - 500")]
        public int? C400___500 { get; set; }

        [Column("500 - 650")]
        [Display(Name = "500 - 650")]
        public int? C500___650 { get; set; }

        [Column("650 - 750")]
        [Display(Name = "650 - 750")]
        public int? C650___750 { get; set; }

        [Column("750+")]
        [Display(Name = "750 +")]
        public int? C750_ { get; set; }

        
        public int? Total
        {
            get
            {
                return C_350.Value + C350___400.Value + C400___500.Value + C500___650.Value + C650___750.Value + C750_.Value;
            }
        }
    }
}