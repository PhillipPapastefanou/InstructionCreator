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
    class HydraulicsPsi5088
    {
        public HydraulicsPsi5088()
        {
            Stopwatch sw = Stopwatch.StartNew();

            List<double> psi50s = new List<double>();
            List<double> cavSlopes = new List<double>();

            using (StreamReader reader = File.OpenText("F:\\Dropbox\\UNI\\Projekte\\03_Hydraulics_Implementation\\Analysis\\CavCurves\\CavitationTriples.csv"))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(reader.ReadToEnd());

                string[] lines = sb.ToString().Split(new [] {"\r\n"}, StringSplitOptions.None);

                foreach (string line in lines)
                {
                    string[] dataLine = line.Split(',');

                    double psi50 = Convert.ToDouble(dataLine[0], CultureInfo.InvariantCulture);
                    double psi88 = Convert.ToDouble(dataLine[1], CultureInfo.InvariantCulture);
                    double slope = Convert.ToDouble(dataLine[2], CultureInfo.InvariantCulture);

                    psi50s.Add(psi50);
                    cavSlopes.Add(slope);
                }
            }


            //string filename = @"F:\SourceTreeRepos\InstructionCreator\InctructionFileCreator\InctructionFileCreator\bin\Debug\masterH-Def-GLDAS.ins";
                //string filename = @"F:\ClimateData\master_hyd.ins";
                string filename = @"..\..\masterBase.ins";

            IInsFile insfile = new InsFileHydraulics();

            InsParser parser = new InsParser(filename, insfile);
            parser.Read();


            InsFileHydraulics hydFile = (IInsFile)insfile.Clone() as InsFileHydraulics;


            StreamWriter fileWriter = new StreamWriter("Insfiles.txt");
            StreamWriter values = new StreamWriter("Values.tsv");

            ClusterBaseSetup baseSetup = new ClusterBaseSetup(ref hydFile);

            List<string> precDrivers = new List<string>();
            precDrivers.Add("/dss/dsshome1/lxc03/ga92wol2/driver_data/GLDAS2/GLDAS_1948_2010_prec_daily_half.nc");
            //precDrivers.Add("/dss/dsshome1/lxc03/ga92wol2/driver_data/GLDAS2/GLDAS_1948_2010_prec_daily_half_TNF_CAX_RED05.nc");

            //precDrivers.Add("F:\\ClimateData\\Amazonia\\GLDAS_1948_2010_prec_daily_half_normal_TNF.nc");
            //precDrivers.Add("F:\\ClimateData\\Amazonia\\GLDAS_1948_2010_prec_daily_half_reduced_TNF.nc");



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
                                        gParams.NPatch = 100;
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
                                        pft_iso.Rootdist = new double[] { 0.6, 0.4 };
                                        pft_iso.RespCoeff = 0.1;


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

            Console.WriteLine(sw.ElapsedMilliseconds);
        }
    }
}
