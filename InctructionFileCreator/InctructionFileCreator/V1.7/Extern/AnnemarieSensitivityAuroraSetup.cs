using System;
using System.Collections.Generic;
using System.IO;
using InctructionFileCreator.InitialSetup;
using InctructionFileCreator.InsFiles;
using InctructionFileCreator.Parameters;

namespace InctructionFileCreator.V1.Extern
{
    public class AnnemarieSensitivityAuroraSetup
    {
        public AnnemarieSensitivityAuroraSetup()
        {

            string filename = "/Users/pp/Documents/Repos/InstructionCreator/InctructionFileCreator/InctructionFileCreator/main_start_aurora.ins";
            string combination_filename = "/Users/pp/Documents/Repos/InstructionCreator/InctructionFileCreator/InctructionFileCreator/SA_Parameters.txt";


            InitialSetup.MathematicaCSVReader csvReader = new MathematicaCSVReader(combination_filename);


            var klatosas = csvReader.GetData("TeBS_k_latosa");
            var ksmaxs = csvReader.GetData("TeBS_ks_max");
            var wooddenss = csvReader.GetData("TeBS_wooddens");
            var psi50xylems= csvReader.GetData("TeBS_psi50_xylem");
            var slas = csvReader.GetData("TeBS_sla");
            var isohyds = csvReader.GetData("TeBS_isohydricity");
            var deltapsiss = csvReader.GetData("TeBS_delta_psi_max");
            var gmins = csvReader.GetData("broadleaved_gmin");


            InsFile41Hydraulics insfile = new InsFile41Hydraulics();
            InsParser parser = new InsParser(filename, insfile);
            parser.Read();
            InsFile41Hydraulics hydFile = (IInsFile)insfile.Clone() as InsFile41Hydraulics;


            StreamWriter fileWriter = new StreamWriter("Insfiles.txt");
            ParameterCombinationWriter writer = new ParameterCombinationWriter("Values.tsv");


            writer.Setup(new List<string>() { "scenario", "psi50", "psi88", "maxKLeaf", "alphaA", "Isohyd", "DeltaPsi" });
            string rootFolder = "Insfiles";

            Directory.CreateDirectory(rootFolder);


            int nsamples = klatosas.Data.Length;

            int index = 0;

            for (int i = 0; i < nsamples; i++)
            {

                string name = index + "run.ins";
                //string path = @"/gpfs/scratch/pr48va/ga92wol2/ga92wol2/2019/Hydraulics_Sens_2019/";
                fileWriter.Write("Insfiles/" + name + "\n");

                Writer ws = new Writer(hydFile, rootFolder + "//" + name);

                PftHyd41 pft = insfile.Pfts["TeBS"] as PftHyd41;

                pft.K_LaToSa = klatosas.Data[i];
                pft.ks_max = ksmaxs.Data[i];
                pft.WoodDens = wooddenss.Data[i];
                pft.psi50_xylem = psi50xylems.Data[i];
                pft.Sla = slas.Data[i];
                pft.Isohydricity = isohyds.Data[i];
                pft.Delta_Psi_Max = deltapsiss.Data[i];
                pft.GMin = gmins.Data[i];

                hydFile.Pfts[0] = pft;

                ws.Write();
                ws.Dispose();

                index++;

            }

            fileWriter.Flush();

        }
    }
}
