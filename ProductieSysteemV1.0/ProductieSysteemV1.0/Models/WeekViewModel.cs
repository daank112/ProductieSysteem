﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductieSysteemV1._0.Models
{
    public class WeekViewModel
    {
        public G_Rule g_Rule { get; set; }
        public int CurrentWeek { get; set; }
        public WeekProduction weekProduction { get; set; }

    }
}