using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace InctructionFileCreator
{

    class GeneralParametersTrunk : IGeneralParameters
    {
        public string Title { get; set; }
        public string Outputdirectory { get; set; }
        public VegetationMode Vegmode { get; set; }
        public int Nyear_spinup { get; set; }
        public bool IfCalcSLA { get; set; }
        public bool IfCalcCtoN { get; set; }
        public bool IfFire { get; set; }
        public int NPatch { get; set; }
        public int PatchArea { get; set; }
        public int EstInterval { get; set; }
        public bool IfDisturb { get; set; }
        public int DistInterval { get; set; }
        public bool IfBgEstab { get; set; }
        public bool IfsMe { get; set; }
        public bool IfStochEstab { get; set; }
        public bool IfStochMort { get; set; }
        public bool IfCDebt { get; set; }
        public WaterUptakeType WaterUptake { get; set; }
        public bool Textured_Soil { get; set; }
        public bool IfSmoothGreffMort { get; set; }
        public bool IfDroughtLimitedEstab { get; set; }
        public bool IfRainOnWetDaysOnly { get; set; }
        public bool IfBvoc { get; set; }
        public bool Run_Landcover { get; set; }
        public bool IfNLim { get; set; }
        public bool IfCentury { get; set; }
        public int FreeNYears { get; set; }
        public double NFix_a { get; set; }
        public double NFix_b { get; set; }
        public double NRelocFrac { get; set; }
        public bool Restart { get; set; }
        public bool Save_State { get; set; }
        public double Soildepth_upper { get; set; }
        public double Soildepth_lower { get; set; }

        public virtual object Clone()
        {
            IGeneralParameters generalParameters = new GeneralParametersTrunk();

            var properties = this.GetType().GetProperties();

            foreach (var p in properties.Where(prop => prop.CanRead && prop.CanWrite))
            {
                object copyValue = p.GetValue(this);
                p.SetValue(generalParameters, copyValue);
            }

            return generalParameters;
        }

        public virtual void Compare(IGeneralParameters other)
        {
            GeneralParametersTrunk otherT = other as GeneralParametersTrunk;

            var properties = this.GetType().GetProperties();

            foreach (var prop in properties)
            {
                Console.WriteLine( prop.Name +  " " + prop.GetValue(this) + " " +prop.GetValue(otherT));
            }
        }
    }
}
