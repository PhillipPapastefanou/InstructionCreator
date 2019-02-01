using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InctructionFileCreator
{
    enum HydraulicSystemType
    {
        STANDARD,
        MONTEITH_BASED_GC,
        VPD_BASED_GC
    }

    class GeneralParametersHydraulics: GeneralParametersTrunk
    {
        public HydraulicSystemType Hydraulic_system { get; set; }
        public double Alphaa_nlim { get; set; }

        public override object Clone()
        {
            IGeneralParameters generalParameters = new GeneralParametersHydraulics();

            var properties = this.GetType().GetProperties();

            foreach (var p in properties.Where(prop => prop.CanRead && prop.CanWrite))
            {
                object copyValue = p.GetValue(this);
                p.SetValue(generalParameters, copyValue);
            }

            return generalParameters;
        }

        public override void Compare(IGeneralParameters other)
        {
            GeneralParametersHydraulics otherT = other as GeneralParametersHydraulics;

            var properties = this.GetType().GetProperties();

            foreach (var prop in properties)
            {
                Console.WriteLine(prop.Name + " " + prop.GetValue(this) + " " + prop.GetValue(otherT));
            }
        }

    }
}
