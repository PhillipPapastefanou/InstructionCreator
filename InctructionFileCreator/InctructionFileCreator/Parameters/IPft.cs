using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace InctructionFileCreator
{
    public enum PftType
    {
        Trunk, Hydraulics, Trunk41, Hydraulics41, Hydraulics41mp
    }

    public enum LandcoverType
    {
        Natural,
        Forest,
    }

    public enum LifeFormType
    {
        Grass,
        Tree
    }

    public enum PathwayType
    {
        C3,
        C4
    }

    public enum LeafPhysiognomyType
    {
        Broadleaf,
        Needleleaf
    }


    public enum PhenologyType
    {
        Evergreen,
        Summergreen,
        Raingreen,
        Any
    }

    public class Export : Attribute
    {

    }

    [AttributeUsage(AttributeTargets.All)]
    public class Found : Attribute
        {
        public bool HasFound { get; set; }
        public Found()
        {
            HasFound = false;
        }
        }

    public abstract class IPft
    {
        [Export]
        public string Name { get; set; }

        // Common pft parameters
        [Found]
        public bool Include { get; set; }
        [Found]
        public double Lambda_max { get; set; }
        [Found]
        public int EMax { get; set; }
        [Found]
        public double ReprFrac { get; set; }
        [Found]
        public double Wscal_Min { get; set; }
        [Found]
        public double Res_Outtake { get; set; }
        [Found]
        public LifeFormType LifeForm { get; set; }
        [Found]
        public LandcoverType Landcover { get; set; }
        [Found]
        public double FireResist { get; set; }
        [Found]
        public PhenologyType Phenology { get; set; }
        [Found]
        public double CrownArea_Max { get; set; }
        [Found]
        public double LToR_Max { get; set; }
        [Found]
        public double Turnover_Root { get; set; }
        [Found]
        public double Turnover_Leaf { get; set; }
        [Found]
        public double[] Rootdist { get; set; }
        [Found]
        public double K_allom1 { get; set; }
        [Found]
        public double K_allom2 { get; set; }
        [Found]
        public double K_allom3 { get; set; }
        [Found]
        public double K_rp { get; set; }
        [Found]
        public double K_LaToSa { get; set; }
        [Found]
        public double WoodDens { get; set; }
        [Found]
        public double CtoN_root { get; set; }
        [Found]
        public double CtoN_sap { get; set; }
        [Found]
        public double NUpToRoot { get; set; }
        [Found]
        public double Km_Volume { get; set; }
        [Found]
        public double FNStorage { get; set; }
        [Found]
        public PathwayType PathWay { get; set; }
        [Found]
        public double RespCoeff { get; set; }
        [Found]
        public double Kest_Repr { get; set; }
        [Found]
        public double Kest_bg { get; set; }
        [Found]
        public double Kest_pres { get; set; }
        [Found]
        public double K_chilla { get; set; }
        [Found]
        public double K_chillb { get; set; }
        [Found]
        public double K_chillk { get; set; }
        [Found]
        public double LitterMe { get; set; }
        [Found]
        public int Longevity { get; set; }
        [Found]
        public double LeafLong { get; set; }
        [Found]
        public LeafPhysiognomyType LeafPhysiognomy { get; set; }
        [Found]
        public double GMin { get; set; }
        [Found]
        public double IntC { get; set; }
        [Found]
        public double GA { get; set; }
        [Found]
        public double Est_max { get; set; }
        [Found]
        public double Parff_min { get; set; }
        [Found]
        public double Alphar { get; set; }
        [Found]
        public double Greff_min { get; set; }
        [Found]
        public double Turnover_sap { get; set; }
        [Found]
        public int Phengdd5ramp { get; set; }
        [Found]
        public double Drought_tolerance { get; set; }
        [Found]
        public double Tcmin_surv { get; set; }
        [Found]
        public double Tcmin_est { get; set; }
        [Found]
        public double Tcmax_est { get; set; }
        [Found]
        public double Twmin_est { get; set; }
        [Found]
        public double Gdd5min_est { get; set; }
        [Found]
        public double Pstemp_min { get; set; }
        [Found]
        public double Pstemp_low { get; set; }
        [Found]
        public double Pstemp_high { get; set; }
        [Found]
        public double Pstemp_max { get; set; }
        [Found]
        public double TwMinusC { get; set; }
        [Found]
        public double Eps_iso { get; set; }
        [Found]
        public double Seas_iso { get; set; }
        [Found]
        public double[] Eps_mon { get; set; }
        [Found]
        public double[] Storfrac_mon { get; set; }
        [Found]
        public double Harv_eff { get; set; }
        [Found]
        public double Turnover_harv_prod { get; set; }
        [Found]
        public double Harvest_slow_frac { get; set; }
        [Found]
        public double Sla { get; set; }
        public double Cton_leaf_min { get; set; }

        public abstract object Clone();
        public abstract void Compare(IPft other);

    }


        public class PftList : List<IPft>
        {
            public IPft this[string name]
            {
                get
                {
                    foreach (IPft pft in this)
                    {
                        if (pft.Name == name)
                        {
                            return pft;
                        }
                    }
                    return null;
                }
            }
            public List<string> AllNames
            {
                get
                {
                    List<string> names = new List<string>();
                    foreach (IPft pft in this)
                    {
                        names.Add(pft.Name);
                    }
                    return names;
                }
            }
        }
    }







