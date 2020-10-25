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
using InctructionFileCreator.V1._7.ClusterSetups;
using SensitivitySetup;

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
            //HydraulicsAmazonBasinSetup setup = new HydraulicsAmazonBasinSetup();
            //SelectedCAXSpeciesAmazon cAXSpeciesAmazon = new SelectedCAXSpeciesAmazon();

            //StratifiedSetup setup = new StratifiedSetup();
            ABSetup setup = new ABSetup();

            //InsfileExport insfileExport = new InsfileExport(String.Empty);


            //Console.ReadKey();
        }
    }
}
