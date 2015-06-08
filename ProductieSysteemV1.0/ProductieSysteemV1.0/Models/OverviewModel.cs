using System.Collections.Generic;


namespace ProductieSysteemV1._0.Models
{
    public class OverviewModel
    {
        public G_Rule rule { get; set; }
        public WeekProduction weekProduction { get; set; }
        public DayProduction dayProduction { get; set; }

        public List<DayProduction> dayProductionList { get; set; }
    }
}