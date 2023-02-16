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

using InctructionFileCreator;
namespace InctructionFileCreator.V1.ClusterSetups
{
    class A03_2023_Mortality_Sensitivities
    {

        private ClusterDriverSetup setup;

        //List<double> lambdas = new List<double>(){-0.08, 0.15, 0.49};
        private List<double> lambdas;
        //List<double> deltaPsiWW = new List<double>(){0.62, 1.23, 2.15};
        private List<double> deltaPsiWW;
        //pft_iso.Delta_Psi_Max = 1.23;
        //pft_iso.Isohydricity = -0.08;pri
        //pft_iso.Delta_Psi_Max = 0.62;

        public A03_2023_Mortality_Sensitivities()
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

            IInsFile insfile = new InsfileHydraulicsMort();

            InsParser parser = new InsParser(filename, insfile);
            parser.Read();




            StreamWriter fileWriter = new StreamWriter("Insfiles.txt");
            ParameterCombinationWriter writer = new ParameterCombinationWriter("Values.tsv");


            Cluster cluster = Cluster.MPI;


            switch (cluster)
            {
                case Cluster.LRZ:
                    {
                        if (setup == ClusterDriverSetup.GLDAS20)
                        { ClusterGLDASBaseLRZSetup baseSetup = new ClusterGLDASBaseLRZSetup(ref insfile); }

                        else if (setup == ClusterDriverSetup.WATCH_WFDEI)
                        { ClusterWWBaseSetup baseSetup = new ClusterWWBaseSetup(ref insfile); }

                        else
                        { Console.WriteLine("No valid cluster setup specified."); }

                    }
                    break;

                case Cluster.Aurora:
                    {
                        if (setup == ClusterDriverSetup.GLDAS20)
                        { ClusterGLDASBaseAuroraSetup baseSetup = new ClusterGLDASBaseAuroraSetup(ref insfile); }

                        else
                        { Console.WriteLine("No valid cluster setup specified."); }

                    }
                    break;

                case Cluster.Simba:
                    {
                        if (setup == ClusterDriverSetup.GLDAS20)
                        { SimbaGLDASBaseSetup baseSetup = new SimbaGLDASBaseSetup(ref insfile); }

                        else
                        { Console.WriteLine("No valid cluster setup specified."); }

                    }
                    break;

                case Cluster.MPI:
                    {
                        if (setup == ClusterDriverSetup.GLDAS20)
                        { MPI_GLDAS_Base_Setup baseSetup = new MPI_GLDAS_Base_Setup(ref insfile); }

                        else
                        { Console.WriteLine("No valid cluster setup specified."); }

                    }
                    break;

                default:
                    break;
            }



            InsfileHydraulicsMort hydFile = (IInsFile)insfile.Clone() as InsfileHydraulicsMort;


            //precDrivers.Add(
            //    "/dss/dsshome1/lxc03/ga92wol2/driver_data/GLDAS2/GLDAS_1948_2010_prec_daily_half_TNF_CAX_RED05_EndTime.nc");


            DriverFilesHydraulics driverFiles = hydFile.DriverFiles as DriverFilesHydraulics;

            driverFiles.File_gridlist = "/Net/Groups/BSI/scratch/ppapastefanou/GLDAS20/AB/TNF_CAX_K34_extend.txt";
   



            int index = 0;



            writer.Setup(new List<string>() { "precDriver", "psi50_xylem", "psi88_xylem", "dk", "fk", "b", "wilting_point", "m_rootdepth", "Isohyd", "DeltaPsi" });


            string rootFolder = "Insfiles";

            Directory.CreateDirectory(rootFolder);


            //List<double> multiplier_psi_leafs = new List<double>() { 0.5,  0.75, 1.0, 1.25, 1.5 };
            List<double> fks = new List<double>() { 0.72, 0.76, 0.8, 0.84, 0.88  };
            List<double> dks = new List<double>() { 7.2, 7.6, 8.0, 8.4, 8.8 };


            foreach (var fk in fks)
            { 
                    foreach (var dk in dks)
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


                                    DriverFilesHydraulics hyDriverFiles =
                                        hydFile.DriverFiles as DriverFilesHydraulics;

                                    //hyDriverFiles.File_gridlist = "/home/phillip/gridlists/TNF_CAX_K34_extend.tsv";


                                    double psi50_xylem = psi50s.Data[j];
                                    double cavS = cavSlopes.Data[j];


                        PftHydMort c3g = insfile.Pfts["C3G"] as PftHydMort;
                                    c3g.Sla = 26.0;

                        PftHydMort c4g = insfile.Pfts["C4G"] as PftHydMort;
                                    c4g.Sla = 26.0;


                                    hydFile.Pfts[1] = c3g;
                                    hydFile.Pfts[2] = c4g;


                        PftHydMort pft_iso = insfile.Pfts["TrBE"] as PftHydMort;

                                    pft_iso.psi50_xylem = psi50_xylem;
                        pft_iso.dk = dk;
                        pft_iso.fk = fk;

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
                                    writer.AddValue("fk", pft_iso.fk);
                                    writer.AddValue("dk", pft_iso.dk);
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
            fileWriter.Close();
            writer.Write();

            Console.WriteLine(sw.ElapsedMilliseconds);
        }
    }
}
