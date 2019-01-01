using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InctructionFileCreator
{

    enum OutputTimeRangeType
    {
        Custom,
        Scenario,
        Spinup,
        Full
    }
    class SmartOutParameters
    {
        public bool Suppress_daily_output { get; set; }
        public bool Suppress_monthly_output { get; set; }
        public bool Suppress_annually_output { get; set; }
        public OutputTimeRangeType Output_time_range { get; set; }
        public int Year_begin { get; set; }
        public int Year_end { get; set; }
    }
}
