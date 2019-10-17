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
using SensitivitySetup;

namespace InctructionFileCreator
{
    class Program
    {
        static void Main(string[] args)
        {

            //SobolSequenceInstructionFile sobolFiles = new SobolSequenceInstructionFile();
            //BrienenAnalysis brienenAnalysis = new BrienenAnalysis();
            //TwoPftStratified twoPftStratified = new TwoPftStratified();

            //SobolPoints2D sobolPoints = new SobolPoints2D();
            //sobolPoints.GenetrateAndWrite();
            
            //HydraulicsSpinupComp spinup = new HydraulicsSpinupComp();
            //HydraulicsPsi5088 psi5088 = new HydraulicsPsi5088();
            HydraulicsSingleSitePsi5088Home psi5088_home = new HydraulicsSingleSitePsi5088Home();


            Console.ReadKey();
        }
    }
}
