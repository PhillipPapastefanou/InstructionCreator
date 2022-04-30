using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InctructionFileCreator.Parameters;
using InctructionFileCreator.Scenario;

namespace InctructionFileCreator
{
    class HydraulicsAmazonBasinSetup
    {

        private List<double> psi50s;
        private List<double> psi88s;
        private List<double> cavSlopes;
        private List<double> mults;
        private List<double> slas;
        private List<double> kLatosScales;

        private ClusterDriverSetup setup;



        //List<double> lambdas = new List<double>(){-0.08, 0.15, 0.49};
        private List<double> lambdas;
        //List<double> deltaPsiWW = new List<double>(){0.62, 1.23, 2.15};
        private List<double> deltaPsiWW;
        //pft_iso.Delta_Psi_Max = 1.23;

        //pft_iso.Isohydricity = -0.08;
        //pft_iso.Delta_Psi_Max = 0.62;

        public HydraulicsAmazonBasinSetup()
        {
            Stopwatch sw = Stopwatch.StartNew();

            setup = ClusterDriverSetup.GLDAS20;

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


            ReadParameters(@"F:\Dropbox\UNI\Projekte\A03_Hydraulics_Implementation\MULTS-SLA-All.csv");


            //string filename = @"F:\SourceTreeRepos\InstructionCreator\InctructionFileCreator\InctructionFileCreator\bin\Debug\masterH-Def-GLDAS.ins";
            //string filename = @"F:\ClimateData\master_hyd.ins";
            string filename = @"..\..\masterBase.ins";

            IInsFile insfile = new InsFileHydraulics();

            InsParser parser = new InsParser(filename, insfile);
            parser.Read();


            InsFileHydraulics hydFile = (IInsFile)insfile.Clone() as InsFileHydraulics;


            StreamWriter fileWriter = new StreamWriter("Insfiles.txt");
            StreamWriter values = new StreamWriter("Values.tsv");

            if (setup == ClusterDriverSetup.GLDAS20)
            {
                ClusterGLDASBaseLRZSetup baseSetup = new ClusterGLDASBaseLRZSetup(ref hydFile);
            }

            else if (setup == ClusterDriverSetup.WATCH_WFDEI)
            {
                ClusterWWBaseSetup baseSetup = new ClusterWWBaseSetup(ref hydFile);
            }

            else
            {
                Console.WriteLine("No valid cluster setup specified.");
            }


            

            List<string> precDrivers = new List<string>();

            if (setup == ClusterDriverSetup.GLDAS20)
            {
                precDrivers.Add("/dss/dsshome1/lxc03/ga92wol2/driver_data/GLDAS2/GLDAS_1948_2010_prec_daily_half.nc");
            }

            else if (setup == ClusterDriverSetup.WATCH_WFDEI)
            {
                precDrivers.Add("/dss/dsshome1/lxc03/ga92wol2/driver_data/WATCH_WFDEI/WATCH_WFDEI_1950_2010_prec.nc");
            }

            else
            {
                Console.WriteLine("No valid cluster setup specified.");
            }




            //precDrivers.Add(
            //    "/dss/dsshome1/lxc03/ga92wol2/driver_data/GLDAS2/GLDAS_1948_2010_prec_daily_half_TNF_CAX_RED05_EndTime.nc");

            //precDrivers.Add("F:\\ClimateData\\Amazonia\\GLDAS_1948_2010_prec_daily_half_normal_TNF.nc");
            //precDrivers.Add("F:\\ClimateData\\Amazonia\\GLDAS_1948_2010_prec_daily_half_reduced_TNF.nc");



            int index = 0;


            List<double> multipliers = new List<double>() {1.5};
            List<double> isohydricities = new List<double>() { 0.0};
            List<double> lambdaMaxes = new List<double>() { 0.95 };


            string rootFolder = "Insfiles";

            Directory.CreateDirectory(rootFolder);


            for (int i = 0; i < precDrivers.Count; i++)
            {
                string filePrec = precDrivers[i];

                for (int la = 0; la < lambdaMaxes.Count; la++)
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
                                gParams.DistInterval = 400;
                                gParams.Alphaa_nlim = 0.7;
                                gParams.Suppress_daily_output = true;
                                gParams.Suppress_annually_output = false;
                                gParams.Suppress_monthly_output = false;
                                gParams.Output_time_range = OutputTimeRangeType.Scenario;
                                gParams.Disable_mort_greff = false;

                                gParams.IfCalcSLA = false;

                                DriverFilesHydraulics hyDriverFiles =
                                    hydFile.DriverFiles as DriverFilesHydraulics;
                                hyDriverFiles.File_prec = filePrec;


                                double psi50 = psi50s[j];
                                double cavS = cavSlopes[j];


                                PftHyd c3g = insfile.Pfts["C3G"] as PftHyd;
                                c3g.Sla = 26.0;

                                PftHyd c4g = insfile.Pfts["C4G"] as PftHyd;
                                c4g.Sla = 26.0;


                                hydFile.Pfts[1] = c3g;
                                hydFile.Pfts[2] = c4g;


                                PftHyd pft_iso = insfile.Pfts["TrBE"] as PftHyd;

                                pft_iso.psi50_xylem = psi50;
                                pft_iso.cav_slope = cavS;

                                pft_iso.Sla = slas[j];

                                pft_iso.K_LaToSa = kLatosScales[j] * 12000.0;

                                pft_iso.Rootdist = new double[] { 0.4, 0.6 };
                                pft_iso.RespCoeff = 0.15;

                                pft_iso.GA = 0.005;
                                pft_iso.CrownArea_Max = 150.0;
                                pft_iso.Lambda_max = 0.90;
                                pft_iso.Longevity = 500;

                                pft_iso.GMin = 1.0;

                                //pft_iso.Isohydricity = lambdas[j];
                                //pft_iso.Delta_Psi_Max = deltaPsiWW[j];

                                pft_iso.Isohydricity = 0.15;
                                pft_iso.Delta_Psi_Max = 1.23;


                                double multiplier = mults[j] * 0.75;
                                pft_iso.ks_max = 80.0 * multiplier;
                                pft_iso.kL_max = 5.0 * multiplier;
                                pft_iso.kr_max = 15.0 * multiplier;

                                //pft_iso.K_rp = 1.5;
                                //pft_iso.K_allom1 = 374;
                                //pft_iso.K_allom2 = 36;
                                //pft_iso.K_allom3 = 0.58;

                                if (index == 0)
                                {
                                    values.Write("DriverIndex" + "\t");
                                    values.Write("InsfileIndex" + "\t");
                                    values.Write("PrecipitationDriver" + "\t");
                                    values.Write("Psi50" + "\t");
                                    values.Write("CavSlope" + "\t");
                                    values.Write("Sapwood conductivity" + "\t");
                                    values.Write("SLA" + "\t");
                                    values.Write("KlatosaScale");
                                    values.Write("Psi88");
                                    values.Write("\n");
                                }

                                values.Write(0 + "\t");
                                values.Write(index + "\t");
                                values.Write(i.ToString(CultureInfo.InvariantCulture) + "\t");
                                values.Write(psi50.ToString(CultureInfo.InvariantCulture) + "\t");
                                values.Write(cavS.ToString(CultureInfo.InvariantCulture) + "\t");
                                values.Write(pft_iso.ks_max.ToString(CultureInfo.InvariantCulture) + "\t");
                                values.Write(pft_iso.Sla.ToString(CultureInfo.InvariantCulture) + "\t");
                                values.Write(pft_iso.K_LaToSa.ToString(CultureInfo.InvariantCulture) + "\t");
                                values.Write(psi88s[j].ToString(CultureInfo.InvariantCulture));

                                values.Write("\n");

                                hydFile.Pfts[0] = pft_iso;

                                ws.Write();
                                ws.Dispose();

                                index++;
                            }

                        }
                    }
                }

            }



            fileWriter.Close();
            values.Close();

            Console.WriteLine(sw.ElapsedMilliseconds);
        }

        private void ReadParameters(string filename)
        {
            psi50s = new List<double>();
            psi88s = new List<double>();
            cavSlopes = new List<double>();
            mults = new List<double>();
            slas = new List<double>();
            deltaPsiWW = new List<double>();
            lambdas = new List<double>();
            kLatosScales = new List<double>();


            using (StreamReader reader = File.OpenText(filename))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(reader.ReadToEnd());

                string[] lines = sb.ToString().Split(new[] { "\r\n" }, StringSplitOptions.None);

                foreach (string line in lines)
                {
                    string[] dataLine = line.Split(',');

                    double psi50 = Convert.ToDouble(dataLine[0], CultureInfo.InvariantCulture);
                    double slope = Convert.ToDouble(dataLine[1], CultureInfo.InvariantCulture);
                    double psi88 = Convert.ToDouble(dataLine[5], CultureInfo.InvariantCulture);
                    double mult = Convert.ToDouble(dataLine[2], CultureInfo.InvariantCulture);
                    double sla = Convert.ToDouble(dataLine[3], CultureInfo.InvariantCulture);
                    double klatosaScale = Convert.ToDouble(dataLine[4], CultureInfo.InvariantCulture);

                    psi50s.Add(psi50);
                    psi88s.Add(psi88);
                    cavSlopes.Add(slope);
                    mults.Add(mult);
                    slas.Add(sla);
                    kLatosScales.Add(klatosaScale);
                }
            }

        }
    }

}
