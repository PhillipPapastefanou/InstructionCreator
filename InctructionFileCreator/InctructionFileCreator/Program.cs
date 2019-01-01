using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InctructionFileCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = Stopwatch.StartNew();

            string filename = @"F:\Repos\guess4\build_mobile\Release\masterH-Def-CORDEX - Kopie (2).ins";
            string filename2 = @"F:\Repos\guess4\build_mobile\Release\masterH-Def-CORDEX-re.ins";

            IInsFile insfile = new InsFileHydraulics();

            InsParser parser = new InsParser(filename, insfile);
            parser.Read();


            IInsFile otherFile = new InsFileHydraulics();

            InsParser parser2 = new InsParser(filename2, otherFile);

            parser2.Read();


            insfile.Compare(otherFile);



            IInsFile hydFile = (IInsFile)insfile.Clone();


            Writer w = new Writer(insfile, @"F:\Repos\guess4\build_mobile\Release\masterH-Def-CORDEX-re.ins");
            w.Write();
            w.Dispose();

            for (int i = 0; i < 5; i++)
            {
                Writer ws = new Writer(insfile, i + "Out.ins");

                
                GeneralParametersHydraulics par  = insfile.GeneralParameters as GeneralParametersHydraulics;
                
                ws.Write();
                ws.Dispose();
                Console.WriteLine(sw.ElapsedMilliseconds);
            }




            Console.ReadKey();
        }
    }
}
