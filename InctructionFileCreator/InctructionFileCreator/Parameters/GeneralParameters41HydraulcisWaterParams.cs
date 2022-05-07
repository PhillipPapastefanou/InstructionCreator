using System;
using System.Linq;

namespace InctructionFileCreator.Parameters
{
    public class GeneralParameters41HydraulcisWaterParams: GeneralParameters41Hydraulics
    {
        public double Soildepth_upper { get; set; }
        public double Soildepth_lower { get; set; }
        public double Soil_wilting_point { get; set; }

        public GeneralParameters41HydraulcisWaterParams()
        {
        }


        public override object Clone()
        {
            GeneralParameters41HydraulcisWaterParams generalParameters = new GeneralParameters41HydraulcisWaterParams();

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
            GeneralParameters41HydraulcisWaterParams otherT = other as GeneralParameters41HydraulcisWaterParams;

            var properties = this.GetType().GetProperties();

            foreach (var prop in properties)
            {
                Console.WriteLine(prop.Name + " " + prop.GetValue(this) + " " + prop.GetValue(otherT));
            }
        }
    }
}
