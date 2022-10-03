using System;
using InctructionFileCreator.InsFiles;
using InctructionFileCreator.Parameters;

namespace InctructionFileCreator
{
    public class Example2_GeneralChanges
    {
        public Example2_GeneralChanges()
        {

            string filename = "/Users/pp/Documents/Repos/guess4.1_hydraulics/cmake-build-release/Europe_Test2_expand.ins";

            // Create a new Insfile. The class type specifies for which version of LPJ-GUESS. In this case it is the
            // 4.1 version using Hydraulics and SmartOutput
            InsFile41Hydraulics insfile = new InsFile41Hydraulics();

            // Creater the parsing object based on the filename and insfile
            InsParser parser = new InsParser(filename, insfile);
            parser.Read();


            InsFile41Hydraulics hydFile = (IInsFile)insfile.Clone() as InsFile41Hydraulics;


            // Parameters are split into three distinct groups:
            // General parameters (1), Driver files (2) and the Pftlist(3)


            // (1) General parameters
            // Access general parameters
           GeneralParameters41Hydraulics gParams =
                    hydFile.GeneralParameters as GeneralParameters41Hydraulics;

            // Change the number of replicate patches of the new instruction file
            gParams.NPatch = 123;

            // Change the hydraulic system of the insfile
            gParams.Hydraulic_system = HydraulicSystemType.VPD_BASED_GC;



            //--------------------------------------------



            // (2) Driving Files
            // Access driving files
            DriverFilesHyd41 driverFiles = hydFile.DriverFiles as DriverFilesHyd41;

            // Change the path of the VPD file
            // IMPORTANT NOTE: Make sure to always put absoulte paths here. Otherwise
            // Smartoutput and other output procedures might not work
            driverFiles.File_Vpd = " path to different VPD file";

            // Change the gridlist
            driverFiles.File_gridlist = "gridlist.txt";


            

            //----------------------------------------------
            // (3) Pfts
            // Acccessing the Pft objects


            // Access a single PFt by name in the new insfile
            PftHyd41 pft1 = hydFile.Pfts["TeBS"] as PftHyd41;


            // Access a single PFt by index in the new insfile
            PftHyd41 pft2 = hydFile.Pfts[0] as PftHyd41;


            // Change pft2's crownarea_max
            pft2.CrownArea_Max = 150;

            // Change pft2's leafphysiognomy (Altought it is unlikely that you will gonnd do exactly that)
            pft2.LeafPhysiognomy = LeafPhysiognomyType.Needleleaf;

        
            // You can also loop through pfts
            foreach (PftHyd41 pft in hydFile.Pfts)
            {
                // Do something with the Pfts over here
            }






            // Write out the filename
            Writer ws = new Writer(hydFile, "Parsed_Insfile.ins");
            ws.Write();
            ws.Dispose();
        }
    }
}
