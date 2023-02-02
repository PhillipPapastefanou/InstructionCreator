using System;
using System.IO;
using InctructionFileCreator.InsFiles;
using InctructionFileCreator.Parameters;
using System.Collections.Generic;


namespace InctructionFileCreator
{
    public class Example3_Looping
    {
        public Example3_Looping()
        {
            // This example generates a list of infiles with different npatches, i.e. to test the influece
            // of increasing stochastisity 


            string filename = "/Users/pp/Documents/Repos/InstructionCreator/InctructionFileCreator/InctructionFileCreator/masterHyd41.ins";

            // Create a new Insfile. The class type specifies for which version of LPJ-GUESS. In this case it is the
            // 4.1 version using Hydraulics and SmartOutput
            InsFile41Hydraulics insfile = new InsFile41Hydraulics();

            // Creater the parsing object based on the filename and insfile
            InsParser parser = new InsParser(filename, insfile);
            parser.Read();


            InsFile41Hydraulics hydFile = (IInsFile)insfile.Clone() as InsFile41Hydraulics;



            string rootFolder = "Insfiles";
            Directory.CreateDirectory(rootFolder);


            // Create a write that writes all names with relative paths of the insfiles to
            // the root directory
            // This is quite usefull if you want the Smartoutput option of LPJ-GUESS
            // that the current hydraulics version has
            StreamWriter fileWriter = new StreamWriter("Insfiles.txt");

            // Create a list of patches we want to simulate
            List<int> npatch_list = new List<int>() { 5, 10, 20, 50, 100, 200, 500, 1000 };



         
            int index = 0;

            foreach (var npatch in npatch_list)
            {
                // (1) General parameters
                // Access general parameters
                GeneralParameters41Hydraulics gParams =
                         hydFile.GeneralParameters as GeneralParameters41Hydraulics;

                // Overwrite npatch value
                gParams.NPatch = npatch;

                // Filenames are simply just and index with run.ins attached
                string name = index + "run.ins";

                // For windows it might be neccessary to change the folder delimiter from /  to //
                fileWriter.Write("Insfiles/" + name + "\n");
                // For windows it might be neccessary to change the folder delimiter from /  to //
                Writer ws = new Writer(hydFile, rootFolder + "/" + name);

                ws.Write();
                ws.Dispose();

                index++;
            }

            fileWriter.Close();


        }
    }
}
