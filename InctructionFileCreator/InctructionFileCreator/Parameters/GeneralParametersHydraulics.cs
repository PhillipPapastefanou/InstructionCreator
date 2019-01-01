using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InctructionFileCreator
{
    enum HydraulicSystemType
    {
        Standard,
        Monteith_based_gc,
        vpd_based_gc
    }

    class GeneralParametersHydraulics: GeneralParametersTrunk
    {
        public HydraulicSystemType Hydraulic_system { get; set; }
        public double Alphaa_nlim { get; set; }
        
    }
}
