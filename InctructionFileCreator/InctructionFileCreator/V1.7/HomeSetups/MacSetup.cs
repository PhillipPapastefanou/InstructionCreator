using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InctructionFileCreator.InitialSetup;
using InctructionFileCreator.Parameters;
using InctructionFileCreator.Scenario;
namespace InctructionFileCreator.V1.HomeSetups
{
    public class MacSetup
    {
        public MacSetup()
        {

            string filename = @"../../hydraulics_gldas.ins";

            IInsFile insfile = new InsFileHydraulics();

            InsParser parser = new InsParser(filename, insfile);
            parser.Read();





            InsFileHydraulics hydFile = (IInsFile)insfile.Clone() as InsFileHydraulics;


            Writer ws = new Writer(hydFile, "Test.ins");


            ws.Write();
            ws.Dispose();
        }
    }
}
