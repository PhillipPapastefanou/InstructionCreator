using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InctructionFileCreator.Parameters;

namespace InctructionFileCreator
{

    public enum VegetationMode
    {
        Population,
        Cohort
    }

    public enum WaterUptake
    {
        Smart,
        Rootdist,
        Wcont
    }

    public abstract class IGeneralParameters
    {
        public string Title { get; set; }
        public string Outputdirectory { get; set; }
        public VegetationMode Vegmode { get; set; }
        public int Nyear_spinup { get; set; }
        public bool IfCalcSLA { get; set; }
        public bool IfCalcCtoN { get; set; }
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
        public WaterUptake WaterUptake { get; set; }
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
        public string State_path { get; set; }
        public int State_year { get; set; }


        public abstract object Clone();
        public abstract void Compare(IGeneralParameters other);
    }
}
