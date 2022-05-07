using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InctructionFileCreator.Parameters
{

    public enum RootDistribution
    {
        Fixed,
        Jackson
    }

    public enum WeatherGeneratorType
    {
        GWGEN
    }


    public enum FireModelType
    {
        Blaze,
        Globfirm,
        Nofire
    }

    public class GeneralParametersTrunk41 : IGeneralParameters
    {
        public RootDistribution RootDistribution { get; set; }
        public WeatherGeneratorType WeatherGenerator { get; set; }
        public FireModelType FireModel { get; set; }
        public bool Ifntransform { get; set; }
        public double F_denitri_max { get; set; }
        public double F_denitri_gas_max { get; set; }
        public double F_nitri_max { get; set; }
        public double F_nitri_gas_max { get; set; }
        public double K_N { get; set; }
        public double K_C { get; set; }
        public bool Iftwolayersoil { get; set; }
        public bool Ifmultilayersnow { get; set; }
        public bool Ifinundationstress { get; set; }
        public bool Ifcarbonfreeze { get; set; }
        public bool Wetland_runon { get; set; }
        public bool Ifmethane { get; set; }
        public bool Iforganicsoilproperties { get; set; }
        public bool Ifsaturatewetlands { get; set; }
        public bool Ifdetrendspinuptemp { get; set; }
        public double Frac_labile_carbon { get; set; }



        public override object Clone()
        {
            IGeneralParameters generalParameters = new GeneralParametersTrunk41();

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
            GeneralParametersTrunk otherT = other as GeneralParametersTrunk;

            var properties = this.GetType().GetProperties();

            foreach (var prop in properties)
            {
                Console.WriteLine(prop.Name + " " + prop.GetValue(this) + " " + prop.GetValue(otherT));
            }
        }
    }
}
