using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InctructionFileCreator.Parameters;

namespace InctructionFileCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = Stopwatch.StartNew();

            string filename = @"F:\Repos\guess4\build_mobile\Release\masterH-Def-CORDEXorg.ins";

            IInsFile insfile = new InsFileHydraulics();

            InsParser parser = new InsParser(filename, insfile);
            parser.Read();
            

            IInsFile hydFile = (IInsFile)insfile.Clone();


            //Writer w = new Writer(insfile, @"F:\Repos\guess4\build_mobile\Release\masterH-Def-CORDEXorg-re.ins");
            //w.Write();
            //w.Dispose();

            for (int i = 0; i < 1000; i++)
            {
                Writer ws = new Writer(insfile, i + "Out.ins");

                
                GeneralParametersHydraulics par  = insfile.GeneralParameters as GeneralParametersHydraulics;
                par.NPatch = i;

  
                PftHyd p = insfile.Pfts["BNE"] as PftHyd;
                


                foreach (var pft in insfile.Pfts)
                {
                    PftHyd pfthyd = pft as PftHyd;
                    pfthyd.ks_max = 15;
                    pfthyd.kL_max = 2;
                    pfthyd.kr_max = 90;
                    pfthyd.Isohydricity = 0.0;
                    pfthyd.Delta_Psi_Max = 2.0;
                    pfthyd.psi50_xylem = -3.0;
                    pfthyd.cav_slope = 5.0;
                }
               
               
                
                ws.Write();
                ws.Dispose();

            }

            Console.WriteLine(sw.ElapsedMilliseconds);




            Console.ReadKey();
        }
    }
}
