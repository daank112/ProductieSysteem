﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductieSysteemV1._0.Models
{
    public class DayViewModel
    {
        public IEnumerable<DayProduction> dagproductie {get; set;}
        public DayProduction dagProduction { get; set; }
    }
}