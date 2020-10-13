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

namespace InctructionFileCreator.V1._7.ClusterSetups
{
    class StratifiedSetup
    {



        private ClusterDriverSetup setup;



        //List<double> lambdas = new List<double>(){-0.08, 0.15, 0.49};
        private List<double> lambdas;
        //List<double> deltaPsiWW = new List<double>(){0.62, 1.23, 2.15};
        private List<double> deltaPsiWW;
        //pft_iso.Delta_Psi_Max = 1.23;

        //pft_iso.Isohydricity = -0.08;
        //pft_iso.Delta_Psi_Max = 0.62;

        public StratifiedSetup()
        {

            InitialSetup.MathematicaCSVReader csvReader = new MathematicaCSVReader(@"F:\Dropbox\UNI\Projekte\A03_Hydraulics_Implementation\Parameters_v1.7.csv");


            double[] psi50s = csvReader.GetData("psi50s");
            double[] psi88s = csvReader.GetData("psi88s");
            double[] cavSlopes = csvReader.GetData("cavslop");
            double[] mults = csvReader.GetData("mults");
            double[] kStemXylem = csvReader.GetData("kStemXylem");
            double[] kRoot = csvReader.GetData("kRoot");
            double[] SLA = csvReader.GetData("SLA");
            double[] kLaToSa = csvReader.GetData("klatosa");
            double[] isohydricities = csvReader.GetData("iso");
            double[] deltaPsiWW = csvReader.GetData("deltapsiww");
    
  
            Stopwatch sw = Stopwatch.StartNew();

            setup = ClusterDriverSetup.GLDAS20;


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
                ClusterGLDASBaseSetup baseSetup = new ClusterGLDASBaseSetup(ref hydFile);
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




            precDrivers.Add(
                "/dss/dsshome1/lxc03/ga92wol2/driver_data/GLDAS2/GLDAS_1948_2010_prec_daily_half_TNF_CAX_RED05_EndTime.nc");


            DriverFilesHydraulics driverFiles = hydFile.DriverFiles as DriverFilesHydraulics;

            driverFiles.File_gridlist = "/dss/dsshome1/lxc03/ga92wol2/driver_data/Gridlists/Amazon/TNF_CAX_K34_extend.txt";

            //precDrivers.Add("F:\\ClimateData\\Amazonia\\GLDAS_1948_2010_prec_daily_half_normal_TNF.nc");
            //precDrivers.Add("F:\\ClimateData\\Amazonia\\GLDAS_1948_2010_prec_daily_half_reduced_TNF.nc");



            int index = 0;


            List<double> maxKLeaf = new List<double>(){ 15.0, 10.0, 5.0};

            //List<double> multipliers = new List<double>() { 1.5 };
            List<double> alphaAs = new List<double>() { 0.65, 0.75 };


            string rootFolder = "Insfiles";

            Directory.CreateDirectory(rootFolder);


            for (int i = 0; i < precDrivers.Count; i++)
            {
                string filePrec = precDrivers[i];

                for (int alpha = 0; alpha < alphaAs.Count; alpha++)
                {
                    
                        for (int f = 0; f < maxKLeaf.Count; f++)
                        {
                        

                            for (int j = 0; j < psi50s.Length; j++)
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
                                gParams.Alphaa_nlim = alphaAs[alpha];
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

                                pft_iso.Sla = SLA[j];

                                pft_iso.K_LaToSa = kLaToSa[j];

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


                                //double multiplier = mults[j] * 0.75;

                                pft_iso.ks_max = kStemXylem[j];
                                pft_iso.kL_max = mults[j]*maxKLeaf[f];
                                pft_iso.kr_max = kRoot[j];

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



            fileWriter.Close();
            values.Close();

            Console.WriteLine(sw.ElapsedMilliseconds);
        }

    }

}
