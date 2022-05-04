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
using InctructionFileCreator.V1._7.ClusterSetups;

namespace InctructionFileCreator.V1.ClusterSetupsß
{
    class Strat_Hydraulics_Fixes
    {
        private ClusterDriverSetup setup;

        //List<double> lambdas = new List<double>(){-0.08, 0.15, 0.49};
        private List<double> lambdas;
        //List<double> deltaPsiWW = new List<double>(){0.62, 1.23, 2.15};
        private List<double> deltaPsiWW;
        //pft_iso.Delta_Psi_Max = 1.23;
        //pft_iso.Isohydricity = -0.08;pri
        //pft_iso.Delta_Psi_Max = 0.62;

        public Strat_Hydraulics_Fixes()
        {
            //InitialSetup.MathematicaCSVReader csvReader = new MathematicaCSVReader(@"F:\Dropbox\UNI\Projekte\A03_Hydraulics_Implementation\Parameters_v1.7.3.csv");
            InitialSetup.MathematicaCSVReader csvReader = new MathematicaCSVReader(@"/Users/pp/Dropbox/UNI/Projekte/A03_Hydraulics_Implementation/Parameters_v1.7.3.csv");

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


            Cluster cluster = Cluster.Simba;


            switch (cluster)
            {
                case Cluster.LRZ:
                    {
                        if (setup == ClusterDriverSetup.GLDAS20)
                        { ClusterGLDASBaseLRZSetup baseSetup = new ClusterGLDASBaseLRZSetup(ref hydFile); }

                        else if (setup == ClusterDriverSetup.WATCH_WFDEI)
                        { ClusterWWBaseSetup baseSetup = new ClusterWWBaseSetup(ref hydFile); }

                        else
                        { Console.WriteLine("No valid cluster setup specified."); }

                    }
                    break;

                case Cluster.Aurora:
                    {
                        if (setup == ClusterDriverSetup.GLDAS20)
                        { ClusterGLDASBaseAuroraSetup baseSetup = new ClusterGLDASBaseAuroraSetup(ref hydFile); }

                        else
                        { Console.WriteLine("No valid cluster setup specified."); }

                    }
                    break;

                case Cluster.Simba:
                    {
                        if (setup == ClusterDriverSetup.GLDAS20)
                        { SimbaGLDASBaseSetup baseSetup = new SimbaGLDASBaseSetup(ref hydFile); }

                        else
                        { Console.WriteLine("No valid cluster setup specified."); }

                    }
                    break;
                default:
                    break;
            }



            //precDrivers.Add(
            //    "/dss/dsshome1/lxc03/ga92wol2/driver_data/GLDAS2/GLDAS_1948_2010_prec_daily_half_TNF_CAX_RED05_EndTime.nc");


            DriverFilesHydraulics driverFiles = hydFile.DriverFiles as DriverFilesHydraulics;

            //driverFiles.File_gridlist = "/dss/dsshome1/lxc03/ga92wol2/driver_data/Gridlists/Amazon/TNF_CAX_K34_extend.txt";
            //precDrivers.Add("F:\\ClimateData\\Amazonia\\GLDAS_1948_2010_prec_daily_half_normal_TNF.nc");
            //precDrivers.Add("F:\\ClimateData\\Amazonia\\GLDAS_1948_2010_prec_daily_half_reduced_TNF.nc");



            int index = 0;



            writer.Setup(new List<string>() { "precDriver", "psi50_xylem", "psi88_xylem", "m_leaf", "m_root", "b", "wilting_point", "m_rootdepth", "Isohyd", "DeltaPsi"});


            string rootFolder = "Insfiles";

            Directory.CreateDirectory(rootFolder);


            List<double> multiplier_psi_leafs = new List<double>() { 1.0, 1.25, 1.5 };
            List<double> multiplier_psi_roots = new List<double>() { 1.0, 0.75, 0.5 };
            List<double> multiplier_root_depths= new List<double>() { 1.0, 1.5, 2.0};
            List<double> wilting_points = new List<double>() { -3.5, -2.5, -3.0, -4.0 };
            List<double> bs = new List<double>() { 0.5, 0.3, 0.7 };


            foreach (var b in bs)
            {
                foreach (var m_psi_leaf in multiplier_psi_leafs)
                {
                    foreach (var m_psi_root in multiplier_psi_roots)
                    {
                        foreach (var m_root_depth in multiplier_root_depths)
                        {
                            foreach (var wp in wilting_points)
                            {



                                for (int j = 0; j < psi50s.Data.Length; j++)
                                {

                                    string name = index + "run.ins";
                                    //string path = @"/gpfs/scratch/pr48va/ga92wol2/ga92wol2/2019/Hydraulics_Sens_2019/";
                                    fileWriter.Write("Insfiles/" + name + "\n");

                                    Writer ws = new Writer(hydFile, rootFolder + "/" + name);

                                    GeneralParametersHydraulics gParams =
                                        hydFile.GeneralParameters as GeneralParametersHydraulics;

                                    gParams.Hydraulic_system = HydraulicSystemType.VPD_BASED_GC;
                                    gParams.NPatch = 50;
                                    gParams.Nyear_spinup = 1500;

                                    gParams.IfDisturb = true;
                                    gParams.DistInterval = 1000;
                                    gParams.Alphaa_nlim = 0.65;
                                    gParams.Suppress_daily_output = true;
                                    gParams.Suppress_annually_output = false;
                                    gParams.Suppress_monthly_output = false;
                                    gParams.Output_time_range = OutputTimeRangeType.Scenario;
                                    gParams.Disable_mort_greff = false;
                                    gParams.IfCalcSLA = false;

                                    gParams.Soildepth_upper = 500.0 * m_root_depth;
                                    gParams.Soildepth_lower = 1000.0 * m_root_depth;

                                    gParams.Soil_wilting_point = wp;


                                    DriverFilesHydraulics hyDriverFiles =
                                        hydFile.DriverFiles as DriverFilesHydraulics;

                                    hyDriverFiles.File_gridlist = "/home/phillip/gridlists/TNF_CAX_K34_extend.tsv";


                                    double psi50_xylem = psi50s.Data[j];
                                    double cavS = cavSlopes.Data[j];


                                    PftHyd c3g = insfile.Pfts["C3G"] as PftHyd;
                                    c3g.Sla = 26.0;

                                    PftHyd c4g = insfile.Pfts["C4G"] as PftHyd;
                                    c4g.Sla = 26.0;


                                    hydFile.Pfts[1] = c3g;
                                    hydFile.Pfts[2] = c4g;


                                    PftHyd pft_iso = insfile.Pfts["TrBE"] as PftHyd;

                                    pft_iso.psi50_xylem = psi50_xylem;
                                    pft_iso.psi50_leaf = psi50_xylem * m_psi_leaf;
                                    pft_iso.psi50_root = psi50_xylem * m_psi_root;
                                    pft_iso.b_leaf_soil_xylem = b;
                                    pft_iso.cav_slope = cavS;

                                    pft_iso.Sla = SLA.Data[j];
                                    pft_iso.Isohydricity = isohydricities.Data[j];
                                    pft_iso.Delta_Psi_Max = 1.6;
                                    //pft_iso.Delta_Psi_Max = deltaPsiWW.Data[j];

                                    //pft_iso.K_LaToSa = kLaToSa.Data[j];
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

                                    pft_iso.ks_max = kStemXylem.Data[j];
                                    pft_iso.kL_max = 7.5;
                                    pft_iso.kr_max = kRoot.Data[j];

                                    //pft_iso.K_rp = 1.5;
                                    //pft_iso.K_allom1 = 374;
                                    //pft_iso.K_allom2 = 36;
                                    //pft_iso.K_allom3 = 0.22;
                                    pft_iso.K_allom3 = 0.58;
                                    pft_iso.WoodDens = 200;
                                    pft_iso.LToR_Max = 1.0;


                                    writer.AddValue("psi50_xylem", pft_iso.psi50_xylem);
                                    writer.AddValue("m_root", m_psi_root);
                                    writer.AddValue("m_leaf", m_psi_leaf);
                                    writer.AddValue("b", pft_iso.b_leaf_soil_xylem);
                                    writer.AddValue("m_rootdepth", m_root_depth);
                                    writer.AddValue("wilting_point", gParams.Soil_wilting_point);
                                    writer.AddValue("psi88_xylem", pft_iso.cav_slope);
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
                }
            }
            fileWriter.Close();
            writer.Write();

            Console.WriteLine(sw.ElapsedMilliseconds);
        }
    }
}
