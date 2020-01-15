using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InctructionFileCreator.Parameters;

namespace InctructionFileCreator
{
    class HydraulicsSingleSitePsi5088Home
    {
        private List<double> psi50s;
        private List<double> cavSlopes;


        public HydraulicsSingleSitePsi5088Home()
        {
           // ReadParameters(
             //   "F:\\Dropbox\\UNI\\Projekte\\03_Hydraulics_Implementation\\Analysis\\CavCurves\\CombosOfModelForPaper.tsv");
            ReadParameters(
                "F:\\Dropbox\\UNI\\Projekte\\03_Hydraulics_Implementation\\Analysis\\SelectedCavCurvesDecember.tsv");

            string filename = @"F:\Dropbox\UNI\Projekte\03_Hydraulics_Implementation\masterHomeWork_CAX.ins";

            IInsFile insfile = new InsFileHydraulics();

            InsParser parser = new InsParser(filename, insfile);
            parser.Read();


            InsFileHydraulics hydFile = (IInsFile)insfile.Clone() as InsFileHydraulics;


            StreamWriter fileWriter = new StreamWriter("Insfiles.txt");
            StreamWriter values = new StreamWriter("Values.tsv");


            List<string> precDrivers = new List<string>();
            precDrivers.Add("F:\\ClimateData\\GLDAS_1948_2010_prec_daily_half.nc");
            precDrivers.Add("F:\\ClimateData\\GLDAS_1948_2010_prec_daily_half_TNF_CAX_RED05.nc");


            int index = 0;
            string rootFolder = "Insfiles";
            Directory.CreateDirectory(rootFolder);

            for (int i = 0; i < precDrivers.Count; i++)
            {
                string filePrec = precDrivers[i];

                for (int j = 0; j < psi50s.Count; j++)
                {


                    string name = index + "run.ins";
                    //string path = @"/gpfs/scratch/pr48va/ga92wol2/ga92wol2/2019/Hydraulics_Sens_2019/";
                    fileWriter.Write("Insfiles/" + name + "\n");

                    Writer ws = new Writer(hydFile, rootFolder + "//" + name);

                    GeneralParametersHydraulics gParams =
                        hydFile.GeneralParameters as GeneralParametersHydraulics;

                    gParams.Hydraulic_system = HydraulicSystemType.VPD_BASED_GC;
                    gParams.NPatch = 50;
                    gParams.Nyear_spinup = 1000;
                    gParams.DistInterval = 200;
                    gParams.Alphaa_nlim = 0.8;
                    gParams.Suppress_daily_output = false;
                    gParams.Suppress_annually_output = false;
                    gParams.Suppress_monthly_output = false;
                    gParams.Output_time_range = OutputTimeRangeType.Scenario;
                    gParams.Disable_mort_greff = false;

                    DriverFilesHydraulics hyDriverFiles =
                        hydFile.DriverFiles as DriverFilesHydraulics;
                    hyDriverFiles.File_prec = filePrec;


                    double psi50 = psi50s[j];
                    double cavS = cavSlopes[j];

                    PftHyd pft_iso = insfile.Pfts["TrBE"] as PftHyd;
                    pft_iso.psi50_xylem = psi50;
                    pft_iso.cav_slope = cavS;
                    pft_iso.Rootdist = new double[] { 0.6, 0.4 };
                    pft_iso.RespCoeff = 0.1;

                    pft_iso.CrownArea_Max = 150.0;
                    //pft_iso.K_rp = 1.5;
                    //pft_iso.K_allom1 = 374;
                    //pft_iso.K_allom2 = 36;
                    //pft_iso.K_allom3 = 0.58;

                    values.Write(index + "\t");
                    values.Write(i.ToString(CultureInfo.InvariantCulture) + "\t");
                    values.Write(psi50.ToString(CultureInfo.InvariantCulture) + "\t");
                    values.Write(cavS.ToString(CultureInfo.InvariantCulture));
                    values.Write("\n");

                    hydFile.Pfts[0] = pft_iso;

                    ws.Write();
                    ws.Dispose();

                    index++;

                }
            }

            fileWriter.Close();
            values.Close();


        }


        private void ReadParameters(string filename)
        {
            psi50s = new List<double>();
            cavSlopes = new List<double>();


            using (StreamReader reader = File.OpenText(filename))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(reader.ReadToEnd());

                string[] lines = sb.ToString().Split(new[] { "\r\n" }, StringSplitOptions.None);

                foreach (string line in lines)
                {
                    string[] dataLine = line.Split('\t');

                    double psi50 = Convert.ToDouble(dataLine[0], CultureInfo.InvariantCulture);
                    double slope = Convert.ToDouble(dataLine[1], CultureInfo.InvariantCulture);

                    psi50s.Add(psi50);
                    cavSlopes.Add(slope);
                }
            }

        }
    }
}
