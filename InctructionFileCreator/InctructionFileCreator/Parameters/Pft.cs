using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InctructionFileCreator
{

    class Pft : IPft
    {
       public string Name { get; set; }




        // Common pft parameters
        public bool Include { get; set; }
        public double Lambda_max { get; set; }
        public int EMax { get; set; }
        public double ReprFrac { get; set; }
        public double Wscal_Min { get; set; }
        public double Res_Outtake { get; set; }
        public LifeFormType LifeForm { get; set; }
        public LandcoverType Landcover { get; set; }

        public double FireResist { get; set; }
        public PhenologyType Phenology { get; set; }
        public double CrownArea_Max { get; set; }
        public double LToR_Max  { get; set; }
        public double Turnover_Root { get; set; }
        public double Turnover_Leaf { get; set; }
        public double[] Rootdist { get; set; }
        public double K_allom1 { get; set; }
        public double K_allom2 { get; set; }
        public double K_allom3 { get; set; }
        public double K_rp { get; set; }
        public double K_LaToSa { get; set; }
        public double WoodDens { get; set; }
        public double CtoN_root { get; set; }
        public double CtoN_sap { get; set; }
        public double NUpToRoot { get; set; }
        public double Km_Volume { get; set; }
        public double FNStorage { get; set; }
        public PathwayType PathWay { get; set; }
        public double RespCoeff { get; set; }
        public double Kest_Repr { get; set; }
        public double Kest_bg { get; set; }
        public double Kest_pres { get; set; }
        public double K_chilla { get; set; }
        public double K_chillb { get; set; }
        public double K_chillk { get; set; }
        public double LitterMe { get; set; }
        public int Longevity { get; set; }
        public double LeafLong { get; set; }
        public LeafPhysiognomyType LeafPhysiognomy { get; set; }
        public double GMin { get; set; }
        public double IntC { get; set; }
        public double GA { get; set; }
        public double Est_max { get; set; }
        public double Parff_min { get; set; }
        public double Alphar { get; set; }
        public double Greff_min { get; set; }
        public double Turnover_sap { get; set; }
        public int Phengdd5ramp { get; set; }
        public double Drought_tolerance { get; set; }
        public double Tcmin_surv { get; set; }
        public double Tcmin_est { get; set; }
        public double Tcmax_est { get; set; }
        public double Twmin_est { get; set; }
        public double Gdd5min_est { get; set; }
        public double Pstemp_min { get; set; }
        public double Pstemp_low { get; set; }
        public double Pstemp_high { get; set; }
        public double Pstemp_max { get; set; }

        public double TwMinusC { get; set; }


        public double Eps_iso { get; set; }
        public double Seas_iso { get; set; }
        public double Eps_mon { get; set; }
        public double Storfrac_mon { get; set; }


        public double Harv_eff { get; set; }
        public double Turnover_harv_prod { get; set; }
        public double Harvest_slow_frac { get; set; }

        public Pft(string name)
        {
            Name = name;
        }

    }
}
