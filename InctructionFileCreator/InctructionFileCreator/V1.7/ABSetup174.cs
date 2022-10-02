using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InctructionFileCreator;
using InctructionFileCreator.InitialSetup;
using InctructionFileCreator.Parameters;
using InctructionFileCreator.Scenario;
using InctructionFileCreator.V1._7.ClusterSetups;

namespace InctructionFileCreator.V1._7
{

    class ABSetup174
    {
        public ABSetup174()
        {

            InctructionFileCreator.InitialSetup.MathematicaCSVReader csvReader = new MathematicaCSVReader(@"F:\Dropbox\UNI\Projekte\A03_Hydraulics_Implementation\Parameters_v1.7.3.csv",
                 ' ') ;


            Column psi50s = csvReader.GetData("psi50s");
            Column psi88s = csvReader.GetData("psi88s");
            Column cavSlopes = csvReader.GetData("cavslop");
            Column kStemXylem = csvReader.GetData("kStemXylem");
            Column kRoot = csvReader.GetData("kRoot");
            Column SLA = csvReader.GetData("SLA");
            Column kLaToSa = csvReader.GetData("klatosa");
            Column isohydricities = csvReader.GetData("iso");
            Column deltaPsiWW = csvReader.GetData("deltapsiww");


            Stopwatch sw = Stopwatch.StartNew();

            string gridlist = "/dss/dsshome1/lxc03/ga92wol2/driver_data/Gridlists/Amazon/Amazon_basin_05.txt";



            //string filename = @"F:\SourceTreeRepos\InstructionCreator\InctructionFileCreator\InctructionFileCreator\bin\Debug\masterH-Def-GLDAS.ins";
            //string filename = @"F:\ClimateData\master_hyd.ins";
            string filename = @"..\..\masterBase.ins";

            IInsFile insfile = new InsFileHydraulics();

            InsParser parser = new InsParser(filename, insfile);
            parser.Read();


            InsFileHydraulics hydFile = (IInsFile)insfile.Clone() as InsFileHydraulics;


            StreamWriter fileWriter = new StreamWriter("Insfiles.txt");
            ParameterCombinationWriter writer = new ParameterCombinationWriter("Values.tsv");





            int index = 0;
            
            List<double> maxKLeaf = new List<double>() { 7.5 };

            //List<double> multipliers = new List<double>() { 1.5 };
            List<double> alphaAs = new List<double>() { 0.65 };

            writer.Setup(new List<string>() { "scenario", "psi50", "psi88", "maxKLeaf", "alphaA", "Isohyd", "DeltaPsi" });




            string rootFolder = "Insfiles";

            Directory.CreateDirectory(rootFolder);

            List<ScenarioType> scenarioTypes = new List<ScenarioType>();
            //scenarioTypes.Add(ScenarioType.RCP26);
            //scenarioTypes.Add(ScenarioType.RCP85);


            for (int i = 0; i < scenarioTypes.Count; i++)
            {
                //ClusterSetupCMIP6 setup = new ClusterSetupCMIP6(ref hydFile, scenarioTypes[i], gridlist);
                //precDrivers.Add(
                //    "/dss/dsshome1/lxc03/ga92wol2/driver_data/GLDAS2/GLDAS_1948_2010_prec_daily_half_TNF_CAX_RED05_EndTime.nc");
                DriverFilesHydraulics driverFiles = hydFile.DriverFiles as DriverFilesHydraulics;


                for (int alpha = 0; alpha < alphaAs.Count; alpha++)
                {

                    for (int f = 0; f < maxKLeaf.Count; f++)
                    {


                        List<int> sels = new List<int>();
                        sels.Add(6);
                        sels.Add(31);

                        for (int j = 0; j < sels.Count; j++)
                        //for (int j = 0; j < psi50s.Data.Length; j++)
                        {

                            string name = index + "run.ins";
                            //string path = @"/gpfs/scratch/pr48va/ga92wol2/ga92wol2/2019/Hydraulics_Sens_2019/";
                            fileWriter.Write("Insfiles/" + name + "\n");

                            Writer ws = new Writer(hydFile, rootFolder + "//" + name);

                            GeneralParametersHydraulics gParams =
                                hydFile.GeneralParameters as GeneralParametersHydraulics;

                            gParams.Hydraulic_system = HydraulicSystemType.VPD_BASED_GC;
                            gParams.NPatch = 50;
                            gParams.Nyear_spinup = 1500;

                            gParams.IfDisturb = true;
                            gParams.DistInterval = 600;
                            gParams.Alphaa_nlim = alphaAs[alpha];
                            gParams.Suppress_daily_output = true;
                            gParams.Suppress_annually_output = false;
                            gParams.Suppress_monthly_output = false;
                            gParams.Output_time_range = OutputTimeRangeType.Scenario;
                            gParams.Disable_mort_greff = false;

                            gParams.IfCalcSLA = false;

                            DriverFilesHydraulics hyDriverFiles =
                                hydFile.DriverFiles as DriverFilesHydraulics;


                            double psi50 = psi50s.Data[sels[j]];
                            double cavS = cavSlopes.Data[sels[j]];


                            PftHyd c3g = insfile.Pfts["C3G"] as PftHyd;
                            c3g.Sla = 26.0;

                            PftHyd c4g = insfile.Pfts["C4G"] as PftHyd;
                            c4g.Sla = 26.0;


                            hydFile.Pfts[1] = c3g;
                            hydFile.Pfts[2] = c4g;


                            PftHyd pft_iso = insfile.Pfts["TrBE"] as PftHyd;

                            pft_iso.psi50_xylem = psi50;
                            pft_iso.cav_slope = cavS;

                            pft_iso.Sla = SLA.Data[sels[j]];
                            pft_iso.Isohydricity = isohydricities.Data[sels[j]];
                            pft_iso.Delta_Psi_Max = 1.6;

                            //pft_iso.K_LaToSa = kLaToSa.Data[j];
                            pft_iso.K_LaToSa = 12000.0;

                            pft_iso.Rootdist = new double[] { 0.4, 0.6 };

                            pft_iso.RespCoeff = 0.25;
                            pft_iso.Longevity = 500;
                            pft_iso.Est_max = 0.05;

                            pft_iso.GA = 0.005;
                            pft_iso.CrownArea_Max = 150.0;
                            pft_iso.Lambda_max = 0.90;

                            pft_iso.GMin = 0.75;






                            //double multiplier = mults[j] * 0.75;

                            pft_iso.ks_max = kStemXylem.Data[j];
                            pft_iso.kL_max = maxKLeaf[f];
                            pft_iso.kr_max = kRoot.Data[j];

                            //pft_iso.K_rp = 1.5;
                            //pft_iso.K_allom1 = 374;
                            //pft_iso.K_allom2 = 36;
                            //pft_iso.K_allom3 = 0.22;
                            pft_iso.K_allom3 = 0.58;
                            pft_iso.WoodDens = 200;
                            pft_iso.LToR_Max = 1.0;


                            writer.AddValue("scenario", i);
                            writer.AddValue("alphaA", gParams.Alphaa_nlim);
                            writer.AddValue("maxKLeaf", maxKLeaf[f]);
                            writer.AddValue("psi50", pft_iso.psi50_xylem);
                            writer.AddValue("psi88", pft_iso.cav_slope);
                            writer.AddValue("Isohyd", pft_iso.Isohydricity);
                            writer.AddValue("DeltaPsi", pft_iso.Delta_Psi_Max);


                            hydFile.Pfts[0] = pft_iso;

                            ws.Write();
                            ws.Dispose();

                            index++;
                        }


                    }
                }

            }



            fileWriter.Close();
            writer.Write();

            Console.WriteLine(sw.ElapsedMilliseconds);
        }

    }
    }
