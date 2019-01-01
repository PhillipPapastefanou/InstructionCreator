using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InctructionFileCreator
{
    enum PftType
    {
        Trunk, Hydraulics
    }

    enum LandcoverType
    {
        Natural,
        Forest,
    }

    enum LifeFormType
    {
        Grass,
        Tree
    }

    enum PathwayType
    {
        C3,
        C4
    }

    enum LeafPhysiognomyType
    {
        Broadleaf,
        Needleleaf
    }


    enum PhenologyType
    {
        Evergreen,
        Summergreen,
        Raingreen,
        Any
    }

    interface IPft
    {
        string Name { get; set; }


        // Common pft parameters
        bool Include { get; set; }
        double Lambda_max { get; set; }
        int EMax { get; set; }
        double ReprFrac { get; set; }
        double Wscal_Min { get; set; }
        double Res_Outtake { get; set; }
        LifeFormType LifeForm { get; set; }
        LandcoverType Landcover { get; set; }

        double FireResist { get; set; }
        PhenologyType Phenology { get; set; }
        double CrownArea_Max { get; set; }
        double LToR_Max { get; set; }
        double Turnover_Root { get; set; }
        double Turnover_Leaf { get; set; }
        double[] Rootdist { get; set; }
        double K_allom1 { get; set; }
        double K_allom2 { get; set; }
        double K_allom3 { get; set; }
        double K_rp { get; set; }
        double K_LaToSa { get; set; }
        double WoodDens { get; set; }
        double CtoN_root { get; set; }
        double CtoN_sap { get; set; }
        double NUpToRoot { get; set; }
        double Km_Volume { get; set; }
        double FNStorage { get; set; }
        PathwayType PathWay { get; set; }
        double RespCoeff { get; set; }
        double Kest_Repr { get; set; }
        double Kest_bg { get; set; }
        double Kest_pres { get; set; }
        double K_chilla { get; set; }
        double K_chillb { get; set; }
        double K_chillk { get; set; }
        double LitterMe { get; set; }
        int Longevity { get; set; }
        double LeafLong { get; set; }
        LeafPhysiognomyType LeafPhysiognomy { get; set; }
        double GMin { get; set; }
        double IntC { get; set; }
        double GA { get; set; }
        double Est_max { get; set; }
        double Parff_min { get; set; }
        double Alphar { get; set; }
        double Greff_min { get; set; }
        double Turnover_sap { get; set; }
        int Phengdd5ramp { get; set; }
        double Drought_tolerance { get; set; }
        double Tcmin_surv { get; set; }
        double Tcmin_est { get; set; }
        double Tcmax_est { get; set; }
        double Twmin_est { get; set; }
        double Gdd5min_est { get; set; }
        double Pstemp_min { get; set; }
        double Pstemp_low { get; set; }
        double Pstemp_high { get; set; }
        double Pstemp_max { get; set; }
        double TwMinusC { get; set; }


        double Eps_iso { get; set; }
        double Seas_iso { get; set; }
        double Eps_mon { get; set; }
        double Storfrac_mon { get; set; }


        double Harv_eff { get; set; }
        double Turnover_harv_prod { get; set; }
        double Harvest_slow_frac { get; set; }

        object Clone();
        void Compare(IPft other);

    } 
}
