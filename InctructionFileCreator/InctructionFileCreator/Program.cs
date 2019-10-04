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

            SobolPoints2D sobolPoints = new SobolPoints2D();
            sobolPoints.GenetrateAndWrite();
            

            Console.ReadKey();
        }
    }
}
