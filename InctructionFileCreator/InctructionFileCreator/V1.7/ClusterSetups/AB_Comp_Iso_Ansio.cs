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

namespace InctructionFileCreator.V1.ClusterSetups
{
    public class AB_Comp_Iso_Ansio
    {

        private ClusterDriverSetup setup;

        public AB_Comp_Iso_Ansio()
        {

            //InitialSetup.MathematicaCSVReader csvReader = new MathematicaCSVReader(@"F:\Dropbox\UNI\Projekte\A03_Hydraulics_Implementation\Parameters_v1.7.3.csv");
            InitialSetup.MathematicaCSVReader csvReader = new MathematicaCSVReader(@"/Users/pp/Dropbox/UNI/Projekte/A04_HydraulicsCompetition/Psi50sAnsioIso.csv");

            Column psi50aniso = csvReader.GetData("psi50aniso");
            Column psi50iso = csvReader.GetData("psi50iso");

            Stopwatch sw = Stopwatch.StartNew();

            setup = ClusterDriverSetup.GLDAS20;


            //string filename = @"F:\SourceTreeRepos\InstructionCreator\InctructionFileCreator\InctructionFileCreator\bin\Debug\masterH-Def-GLDAS.ins";
            //string filename = @"F:\ClimateData\master_hyd.ins";
            string filename = @"../../masterBase.ins";

            IInsFile insfile = new InsFileHydraulics();

            InsParser parser = new InsParser(filename, insfile);
            parser.Read();


            InsFileHydraulics hydFile = (IInsFile)insfile.Clone() as InsFileHydraulics;


            StreamWriter fileWriter = new StreamWriter("Insfiles.txt");
            ParameterCombinationWriter writer = new ParameterCombinationWriter("Values.tsv");



            if (setup == ClusterDriverSetup.GLDAS20)
            {
                //ClusterGLDASBaseSetup baseSetup = new ClusterGLDASBaseSetup(ref hydFile);
                SimbaGLDASBaseSetup baseSetup = new SimbaGLDASBaseSetup(ref hydFile);
            }

            else if (setup == ClusterDriverSetup.WATCH_WFDEI)
            {
                ClusterWWBaseSetup baseSetup = new ClusterWWBaseSetup(ref hydFile);
            }

            else
            {
                Console.WriteLine("No valid cluster setup specified.");
            }





            //precDrivers.Add(
            //    "/dss/dsshome1/lxc03/ga92wol2/driver_data/GLDAS2/GLDAS_1948_2010_prec_daily_half_TNF_CAX_RED05_EndTime.nc");


            DriverFilesHydraulics driverFiles = hydFile.DriverFiles as DriverFilesHydraulics;

            //driverFiles.File_gridlist = "/dss/dsshome1/lxc03/ga92wol2/driver_data/Gridlists/Amazon/TNF_CAX_K34_extend.txt";

            //precDrivers.Add("F:\\ClimateData\\Amazonia\\GLDAS_1948_2010_prec_daily_half_normal_TNF.nc");
            //precDrivers.Add("F:\\ClimateData\\Amazonia\\GLDAS_1948_2010_prec_daily_half_reduced_TNF.nc");

            int index = 0;

            List<double> maxKLeaf = new List<double>() { 7.5 };
            List<double> alphaAs = new List<double>() { 0.65 };


            string rootFolder = "Insfiles";

            Directory.CreateDirectory(rootFolder);



            for (int j = 0; j < psi50iso.Data.Length; j++)
            {

                string name = index + "run.ins";
                //string path = @"/gpfs/scratch/pr48va/ga92wol2/ga92wol2/2019/Hydraulics_Sens_2019/";
                fileWriter.Write("Insfiles/" + name + "\n");

                Writer ws = new Writer(hydFile, rootFolder + "//" + name);

                GeneralParametersHydraulics gParams =
                    hydFile.GeneralParameters as GeneralParametersHydraulics;

                gParams.Hydraulic_system = HydraulicSystemType.VPD_BASED_GC;
                gParams.NPatch = 20;
                gParams.Nyear_spinup = 1500;

                gParams.IfDisturb = true;
                gParams.DistInterval = 1000;
                gParams.Alphaa_nlim = alphaAs[0];
                gParams.Suppress_daily_output = true;
                gParams.Suppress_annually_output = false;
                gParams.Suppress_monthly_output = false;
                gParams.Output_time_range = OutputTimeRangeType.Scenario;
                gParams.Disable_mort_greff = false;

                gParams.IfCalcSLA = false;

                DriverFilesHydraulics hyDriverFiles =
                    hydFile.DriverFiles as DriverFilesHydraulics;



                //double cavS_iso = cavSlopes.Data[j];


                PftHyd c3g = insfile.Pfts["C3G"] as PftHyd;
                c3g.Sla = 26.0;

                PftHyd c4g = insfile.Pfts["C4G"] as PftHyd;
                c4g.Sla = 26.0;


                hydFile.Pfts[2] = c3g;
                //hydFile.Pfts[3] = c4g;


                double psi50_iso = psi50iso.Data[j];


                PftHyd pft = (insfile.Pfts["TrBE"] as PftHyd);

                PftHyd pft_iso = (PftHyd) pft.Clone();

                pft_iso.Name = "TrBE_Iso";
                pft_iso.psi50_xylem = psi50_iso;
                pft_iso.cav_slope = SLOPE(psi50_iso, psi50_iso - 1.0);
                pft_iso.Sla = SLA(psi50_iso);
                pft_iso.Isohydricity = ISO(psi50_iso);
                pft_iso.ks_max = KS(psi50_iso);
                pft_iso.Delta_Psi_Max = 1.6;


                pft_iso.K_LaToSa = 10000.0;

                pft_iso.Rootdist = new double[] { 0.4, 0.6 };

                pft_iso.RespCoeff = 0.3;
                pft_iso.Longevity = 600;
                pft_iso.Est_max = 0.05;

                pft_iso.GA = 0.005;
                pft_iso.CrownArea_Max = 600.0;
                pft_iso.Lambda_max = 0.90;

                pft_iso.GMin = 0.75;


                //double multiplier = mults[j] * 0.75;


                pft_iso.kL_max = maxKLeaf[0];
                pft_iso.kr_max = 0.0004;

                //pft_iso.K_rp = 1.5;
                //pft_iso.K_allom1 = 374;
                //pft_iso.K_allom2 = 36;
                //pft_iso.K_allom3 = 0.22;
                pft_iso.K_allom3 = 0.58;
                pft_iso.WoodDens = 200;
                pft_iso.LToR_Max = 1.0;
                hydFile.Pfts[0] = pft_iso;




                PftHyd pft_aniso = (PftHyd)pft.Clone();


                double psi50_aniso = psi50aniso.Data[j];
                pft_aniso.Name = "TrBE_Aniso";
                pft_aniso.psi50_xylem = psi50_aniso;
                pft_aniso.cav_slope = SLOPE(psi50_aniso, psi50_aniso - 1.0) ;
                pft_aniso.Sla = SLA(psi50_aniso);
                pft_aniso.Isohydricity = ISO(psi50_aniso);
                pft_aniso.ks_max = KS(psi50_aniso);
                pft_aniso.Delta_Psi_Max = 1.6;


                pft_aniso.K_LaToSa = 10000.0;

                pft_aniso.Rootdist = new double[] { 0.4, 0.6 };

                pft_aniso.RespCoeff = 0.3;
                pft_aniso.Longevity = 600;
                pft_aniso.Est_max = 0.05;

                pft_aniso.GA = 0.005;
                pft_aniso.CrownArea_Max = 600.0;
                pft_aniso.Lambda_max = 0.90;

                pft_aniso.GMin = 0.75;


                //double multiplier = mults[j] * 0.75;


                pft_aniso.kL_max = maxKLeaf[0];
                pft_aniso.kr_max = 0.0004;

                //pft_iso.K_rp = 1.5;
                //pft_iso.K_allom1 = 374;
                //pft_iso.K_allom2 = 36;
                //pft_iso.K_allom3 = 0.22;
                pft_aniso.K_allom3 = 0.58;
                pft_aniso.WoodDens = 200;
                pft_aniso.LToR_Max = 1.0;
                hydFile.Pfts[1] = pft_aniso;



                ws.Write();
                ws.Dispose();

                index++;
            }





            fileWriter.Close();
            //writer.Write();

            Console.WriteLine(sw.ElapsedMilliseconds);
        }


        private double KS(double psi50)
        {
            return 5.54282 / Math.Pow(-psi50, 1.809077000000000);
        }

        private double SLA(double psi50)
        { 
            return 43.0335 / Math.Pow(-psi50, 0.8049757000000000);
        }

        private double SLOPE(double psi50, double psi88)
        {
            return 1.9924301646902063 / Math.Log(psi50 / psi88);
        }

        private double ISO(double psi50)
        {
            return 0.65 + 0.15 * psi50;
        }


    }


    
}
