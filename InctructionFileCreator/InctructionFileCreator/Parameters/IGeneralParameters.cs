using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InctructionFileCreator
{
    public enum VegetationMode
    {
        Population,
        Cohort
    }

    public enum WaterUptakeType
    {
        Smart,
        Rootdist,
        Wcont
    }
    public interface IGeneralParameters
    {
        string Title { get; set; }
        string Outputdirectory { get; set; }
        VegetationMode Vegmode { get; set; }
        int Nyear_spinup { get; set; }
        bool IfCalcSLA { get; set; }
        bool IfCalcCtoN { get; set; }
        bool IfFire { get; set; }
        int NPatch { get; set; }
        int PatchArea { get; set; }
        int EstInterval { get; set; }
        bool IfDisturb { get; set; }
        int DistInterval { get; set; }
        bool IfBgEstab { get; set; }
        bool IfsMe { get; set; }
        bool IfStochEstab { get; set; }
        bool IfStochMort { get; set; }
        bool IfCDebt { get; set; }
        WaterUptakeType WaterUptake { get; set; }
        bool Textured_Soil { get; set; }
        bool IfSmoothGreffMort { get; set; }
        bool IfDroughtLimitedEstab { get; set; }
        bool IfRainOnWetDaysOnly { get; set; }
        bool IfBvoc { get; set; }
        bool Run_Landcover { get; set; }




        bool IfNLim { get; set; }
        bool IfCentury { get; set; }
        int FreeNYears { get; set; }
        double NFix_a { get; set; }
        double NFix_b { get; set; }
        double NRelocFrac { get; set; }
        double Soildepth_upper { get; set; }
        double Soildepth_lower { get; set; }


        bool Restart { get; set; }
        bool Save_State { get; set; }


        object Clone();

        void Compare(IGeneralParameters other);



    }
}
