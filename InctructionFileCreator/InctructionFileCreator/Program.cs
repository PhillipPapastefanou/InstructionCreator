using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InctructionFileCreator.Parameters;
using InctructionFileCreator.V1._7;
using InctructionFileCreator.V1._7.ClusterSetups;
using InctructionFileCreator.V1._7.HomeSetups;
using SensitivitySetup;
using InctructionFileCreator.Factories;
using InctructionFileCreator.V1.ClusterSetups;
using InctructionFileCreator.V1.ClusterSetupsß;

namespace InctructionFileCreator
{
    class Program
    {
        static void Main(string[] args)
        {

            //string[] subdirectoryEntries = Directory.GetDirectories(Directory.GetCurrentDirectory());

            //foreach (string dir in subdirectoryEntries)
            //{
            //    if (dir.Contains("run"))
            //    {
                    
            //    }
            //}
            if (Directory.Exists("Insfiles"))
                Directory.Delete("Insfiles", true);




            //SobolSequenceInstructionFile sobolFiles = new SobolSequenceInstructionFile();
            //BrienenAnalysis brienenAnalysis = new BrienenAnalysis();
            //TwoPftStratified twoPftStratified = new TwoPftStratified();

            //SobolPoints2D sobolPoints = new SobolPoints2D();
            //sobolPoints.GenetrateAndWrite();

            //HydraulicsSpinupComp spinup = new HydraulicsSpinupComp();
            //HydraulicsPsi5088 psi5088 = new HydraulicsPsi5088();
            //HydraulicsSingleSitePsi5088Home psi5088_home = new HydraulicsSingleSitePsi5088Home();
            //HydraulicsSingleSitesPsi5088HomeTomPugh psi5088_home = new HydraulicsSingleSitesPsi5088HomeTomPugh();
            //HydraulicsHomeAllometric psi5088_home = new HydraulicsHomeAllometric();
            //HydraulicsStratifiedClusterSampling smlp = new HydraulicsStratifiedClusterSampling();
            // HydraulicsStratifiedFullCavSampling csamCavSampling = new HydraulicsStratifiedFullCavSampling();
            //ClusterAlphaaLambda clusterAlphaLambda = new ClusterAlphaaLambda();

            //HydraulicsAmazonBasinSetup setup = new
            //HydraulicsAmazonBasinSetup();
            //SelectedCAXSpeciesAmazon cAXSpeciesAmazon = new SelectedCAXSpeciesAmazon();

            //StratifiedSetup setup = new StratifiedSetup();
            //ABSetupHome setup = new ABSetupHome();

            //ABSetup setup = new ABSetup();


            //ABSetup_IncreasedSpinuptime setup = new ABSetup_IncreasedSpinuptime();

            A03_2023_Mortality_Sensitivities asd = new A03_2023_Mortality_Sensitivities();

            //A03_ks_constant_fix fix = new A03_ks_constant_fix();

            //Strat_Hydraulics_Fixes fixes = new Strat_Hydraulics_Fixes();

            //AB_Comp_Iso_Ansio setup = new AB_Comp_Iso_Ansio();

            //AfricanRainforestSetup setup = new AfricanRainforestSetup();

            //ABSetup173_var_deltaPSi setup2205= new ABSetup173_var_deltaPSi();

            //ABSetup174 setup174 = new ABSetup174();
            // StratifiedThreeFixed fiexed = new StratifiedThreeFixed();

            //ThreeWayHomeSetup  setup  = new ThreeWayHomeSetup();

            //StratiefiedTwo st = new StratiefiedTwo();
            //StratifiedThree st = new StratifiedThree();

            //RandomSampledThree r3 = new RandomSampledThree();

            //HomeStratifiedSetup f = new HomeStratifiedSetup();

            //InsfileExport insfileExport = new InsfileExport(String.Empty);


            //ISIMIP_Factory factory = new ISIMIP_Factory();

            //Strat_Hydraulics_Fixes fixes = new Strat_Hydraulics_Fixes();

            //Sobol_Hydraulics_Fixes fixes = new Sobol_Hydraulics_Fixes();

            //Console.ReadKey();

            Console.WriteLine("Finished!");
        }
    }
}
