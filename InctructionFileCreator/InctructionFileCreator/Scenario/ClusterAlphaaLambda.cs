using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using InctructionFileCreator.Parameters;

namespace InctructionFileCreator
{
    class ClusterAlphaaLambda
    {
        public ClusterAlphaaLambda()
        {

            Stopwatch sw = Stopwatch.StartNew();

            //List<double> psi50s = new List<double>();
            //List<double> cavSlopes = new List<double>();

            //using (StreamReader reader = File.OpenText("F:\\Dropbox\\UNI\\Projekte\\03_Hydraulics_Implementation\\Analysis\\CavCurves\\CavitationTriples.csv"))
            //{
            //    StringBuilder sb = new StringBuilder();
            //    sb.Append(reader.ReadToEnd());

            //    string[] lines = sb.ToString().Split(new[] { "\r\n" }, StringSplitOptions.None);

            //    foreach (string line in lines)
            //    {
            //        string[] dataLine = line.Split(',');

            //        double psi50 = Convert.ToDouble(dataLine[0], CultureInfo.InvariantCulture);
            //        double psi88 = Convert.ToDouble(dataLine[1], CultureInfo.InvariantCulture);
            //        double slope = Convert.ToDouble(dataLine[2], CultureInfo.InvariantCulture);

            //        psi50s.Add(psi50);
            //        cavSlopes.Add(slope);
            //    }
            //}

            //string filename = @"F:\SourceTreeRepos\InstructionCreator\InctructionFileCreator\InctructionFileCreator\bin\Debug\masterH-Def-GLDAS.ins";
            //string filename = @"F:\ClimateData\master_hyd.ins";
            string filename = @"..\..\masterBase.ins";

            IInsFile insfile = new InsFileHydraulics();

            InsParser parser = new InsParser(filename, insfile);
            parser.Read();


            InsFileHydraulics hydFile = (IInsFile) insfile.Clone() as InsFileHydraulics;


            StreamWriter fileWriter = new StreamWriter("Insfiles.txt");
            StreamWriter values = new StreamWriter("Values.tsv");

            ClusterGLDASBaseLRZSetup baseSetup = new ClusterGLDASBaseLRZSetup(ref insfile);

            List<string> precDrivers = new List<string>();
            precDrivers.Add("/dss/dsshome1/lxc03/ga92wol2/driver_data/GLDAS2/GLDAS_1948_2010_prec_daily_half.nc");
            precDrivers.Add(
                "/dss/dsshome1/lxc03/ga92wol2/driver_data/GLDAS2/GLDAS_1948_2010_prec_daily_half_TNF_CAX_RED05_EndTime.nc");

            //precDrivers.Add("F:\\ClimateData\\Amazonia\\GLDAS_1948_2010_prec_daily_half_normal_TNF.nc");
            //precDrivers.Add("F:\\ClimateData\\Amazonia\\GLDAS_1948_2010_prec_daily_half_reduced_TNF.nc");



            int index = 0;


            //The medium group parameters of the xylem vulnerability space
            List<double> psi50s = new List<double>() {-3.02};
            List<double> cavSlopes = new List<double>() {-2.78};

            List<double> multipliers = new List<double>() {1.0};
            List<double> isohydricities = new List<double>() {0.0};



            List<double> lambdaMaxes = new List<double>() { 
                0.7, 0.715, 0.729, 0.743, 0.756, 0.769, 0.782, 0.794, 0.805, 0.816, 
                0.827, 0.838, 0.848, 0.858, 0.867, 0.877, 0.886, 0.895, 0.904, 0.912, 
                0.92, 0.928, 0.936, 0.944, 0.951, 0.959, 0.966, 0.973, 0.98, 0.987};

            List<double> alphaas = new List<double>()
            {

                0.3,
                0.325,
                0.35,
                0.375,
                0.4,
                0.425,
                0.45,
                0.475,
                0.5,
                0.525,
                0.55,
                0.575,
                0.6,
                0.625,
                0.65,
                0.675,
                0.7,
                0.725,
                0.75,
                0.775,
                0.8,
                0.825,
                0.85,
                0.875,
                0.9,
                0.925,
                0.95,
                0.975,
                1.0
            };


            string rootFolder = "Insfiles";

            Directory.CreateDirectory(rootFolder);


            for (int i = 0; i < precDrivers.Count; i++)
            {
                string filePrec = precDrivers[i];

                for (int la = 0; la < lambdaMaxes.Count; la++)
                {

                    for (int a = 0; a < alphaas.Count; a++)
                    {
                        


                    for (int iso = 0; iso < isohydricities.Count; iso++)
                    {



                        for (int f = 0; f < multipliers.Count; f++)
                        {



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
                                gParams.Alphaa_nlim = alphaas[a];
                                gParams.Suppress_daily_output = true;
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
                                pft_iso.Rootdist = new double[] {0.6, 0.4};
                                pft_iso.RespCoeff = 0.1;

                                pft_iso.GA = 0.005;
                                pft_iso.CrownArea_Max = 150.0;
                                pft_iso.Lambda_max = lambdaMaxes[la];

                                pft_iso.Isohydricity = isohydricities[iso];
                                double multiplier = multipliers[f];
                                pft_iso.ks_max = 80.0 * multiplier;
                                pft_iso.kL_max = 5.0 * multiplier;
                                pft_iso.kr_max = 15.0 * multiplier;

                                values.Write(index + "\t");
                                values.Write(i.ToString(CultureInfo.InvariantCulture) + "\t");
                                values.Write(psi50.ToString(CultureInfo.InvariantCulture) + "\t");
                                values.Write(cavS.ToString(CultureInfo.InvariantCulture) + "\t");
                                values.Write(pft_iso.Lambda_max.ToString(CultureInfo.InvariantCulture) + "\t");
                                values.Write(alphaas[a].ToString(CultureInfo.InvariantCulture) + "\t");
                                values.Write(pft_iso.Isohydricity.ToString(CultureInfo.InvariantCulture) + "\t");
                                values.Write(multipliers[f].ToString(CultureInfo.InvariantCulture));
                                values.Write("\n");

                                hydFile.Pfts[0] = pft_iso;

                                ws.Write();
                                ws.Dispose();

                                Console.Write(index +  " ");

                                index++;
                            }
                        }
                        }
                    }
                }
            }
            values.Flush();
            values.Close();

            fileWriter.Flush();
            fileWriter.Close();
        }


        
    }
}
