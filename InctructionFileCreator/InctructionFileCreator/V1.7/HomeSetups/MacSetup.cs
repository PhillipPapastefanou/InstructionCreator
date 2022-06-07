using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InctructionFileCreator.InitialSetup;
using InctructionFileCreator.InsFiles;
using InctructionFileCreator.Parameters;
using InctructionFileCreator.Scenario;
namespace InctructionFileCreator.V1.HomeSetups
{
    public class MacSetup
    {
        public MacSetup()
        {

            //string filename = @"../../hydraulics_gldas.ins";
            //string filename = "/Users/pp/Documents/Repos/guess4.1_hydraulics/cmake-build-release/TrBE/agg_hydraulics_gldas_TrBE_MP.ins";
            string filename = "/Users/pp/Documents/Repos/guess4.1_hydraulics/cmake-build-release/Europe_Test2_expand.ins";


            InsFile41Hydraulics insfile = new InsFile41Hydraulics();

            InsParser parser = new InsParser(filename, insfile);
            parser.Read();



            //var properties = insfile.Pfts[0].GetType().GetProperties();
            //foreach (var p in properties.Where(prop => prop.CanRead && prop.CanWrite))
            //{
            //    object[] attrs = p.GetCustomAttributes(true);

            //    Found found_att = attrs[0] as Found;

            //    if (found_att != null)
            //    {
            //        Console.WriteLine(p.Name + " " + found_att.HasFound);

            //    }
            //}


            InsFile41Hydraulics hydFile = (IInsFile)insfile.Clone() as InsFile41Hydraulics;


            //InsFileTrunk41 loeader = new InsFileTrunk41();

            //InsParser loeader_parser = new InsParser("/Users/pp/Documents/Repos/guess4.1_hydraulics/cmake-build-release/global_hyd_dummy_TeBS.ins", loeader);
            //loeader_parser.Read();
            // InsFileTrunk41 loader_file = (IInsFile)insfile.Clone() as InsFileTrunk41;




            DriverFilesHyd41 drivers = hydFile.DriverFiles as DriverFilesHyd41;

            drivers.Variable_Specifichum = "";

            GeneralParameters41Hydraulics parameters = hydFile.GeneralParameters as GeneralParameters41Hydraulics;

            parameters.State_year = 1;

            foreach (var pft in hydFile.Pfts)
            {
                PftHyd41 a = pft as PftHyd41;

                a.Boleheight_frac = 0.5;
            }




            Writer ws = new Writer(hydFile, "Test_Europe.ins");


            ws.Write();
            ws.Dispose();

            Console.ReadKey();
        }
    }
}
