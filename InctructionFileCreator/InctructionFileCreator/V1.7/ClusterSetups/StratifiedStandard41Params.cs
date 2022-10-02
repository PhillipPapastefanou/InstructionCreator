using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InctructionFileCreator.InitialSetup;
using InctructionFileCreator.InsFiles;
using InctructionFileCreator.Parameters;
using InctructionFileCreator.Scenario;
namespace InctructionFileCreator.V1.ClusterSetups
{
    public class StratifiedStandard41Params
    {
        private ClusterDriverSetup setup;

        public StratifiedStandard41Params()
        {

            Stopwatch sw = Stopwatch.StartNew();
            setup = ClusterDriverSetup.GLDAS20;

            string filename = @"../../Europe_start.ins";
            InsFile41Hydraulics insfile = new InsFile41Hydraulics();
            InsParser parser = new InsParser(filename, insfile);
            parser.Read();
            IInsFile hydFile = (IInsFile)insfile.Clone() as InsFile41Hydraulics;


            StreamWriter fileWriter = new StreamWriter("Insfiles.txt");
            ParameterCombinationWriter writer = new ParameterCombinationWriter("Values.tsv");

            Cluster cluster = Cluster.Simba;

            switch (cluster)
            {
                case Cluster.LRZ:
                    {
                        if (setup == ClusterDriverSetup.GLDAS20)
                        { //ClusterGLDASBaseLRZSetup baseSetup = new ClusterGLDASBaseLRZSetup(ref hydFile);
                        }

                        else if (setup == ClusterDriverSetup.WATCH_WFDEI)
                        {
                            //ClusterWWBaseSetup baseSetup = new ClusterWWBaseSetup(ref hydFile);
                            //
                        }

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
                        {
                            SimbaGLDASBaseSetup baseSetup = new SimbaGLDASBaseSetup(ref hydFile);
                        }

                        else
                        { Console.WriteLine("No valid cluster setup specified."); }

                    }
                    break;
                default:
                    break;
            }



            //precDrivers.Add(
            //    "/dss/dsshome1/lxc03/ga92wol2/driver_data/GLDAS2/GLDAS_1948_2010_prec_daily_half_TNF_CAX_RED05_EndTime.nc");


            DriverFilesHyd41 driverFiles = hydFile.DriverFiles as DriverFilesHyd41;




            //driverFiles.File_gridlist = "/dss/dsshome1/lxc03/ga92wol2/driver_data/Gridlists/Amazon/TNF_CAX_K34_extend.txt";

            //precDrivers.Add("F:\\ClimateData\\Amazonia\\GLDAS_1948_2010_prec_daily_half_normal_TNF.nc");
            //precDrivers.Add("F:\\ClimateData\\Amazonia\\GLDAS_1948_2010_prec_daily_half_reduced_TNF.nc");



            int index = 0;


            List<double> maxKLeaf = new List<double>() { 7.5 };

            //List<double> multipliers = new List<double>() { 1.5 };
            List<double> alphaAs = new List<double>() { 0.60 };

            writer.Setup(new List<string>() { "precDriver", "psi50", "psi88", "maxKLeaf", "alphaA", "Isohyd", "DeltaPsi" });


            string rootFolder = "Insfiles";

            Directory.CreateDirectory(rootFolder);



                for (int alpha = 0; alpha < alphaAs.Count; alpha++)
                {

                    for (int f = 0; f < maxKLeaf.Count; f++)
                    {



                            string name = index + "run.ins";
                            //string path = @"/gpfs/scratch/pr48va/ga92wol2/ga92wol2/2019/Hydraulics_Sens_2019/";
                            fileWriter.Write("Insfiles/" + name + "\n");

                            Writer ws = new Writer(hydFile, rootFolder + "//" + name);

                            GeneralParameters41Hydraulics gParams =
                                hydFile.GeneralParameters as GeneralParameters41Hydraulics;

                            gParams.Hydraulic_system = HydraulicSystemType.STANDARD;
                            gParams.NPatch = 50;
                            gParams.IfDisturb = true;
                            gParams.Alphaa_nlim = alphaAs[alpha];
                            gParams.Suppress_daily_output = true;
                            gParams.Suppress_annually_output = false;
                            gParams.Suppress_monthly_output = false;
                            gParams.Output_time_range = OutputTimeRangeType.Scenario;
                            gParams.Disable_mort_greff = false;

                            gParams.IfCalcSLA = false;

                            DriverFilesHyd41 hyDriverFiles =
                                hydFile.DriverFiles as DriverFilesHyd41;





                            //writer.AddValue("precDriver", i);
                            //writer.AddValue("alphaA", gParams.Alphaa_nlim);
                            //writer.AddValue("maxKLeaf", maxKLeaf[f]);
                            //writer.AddValue("psi50", pft_iso.psi50_xylem);
                            //writer.AddValue("psi88", pft_iso.cav_slope);
                            //writer.AddValue("Isohyd", pft_iso.Isohydricity);
                            //writer.AddValue("DeltaPsi", pft_iso.Delta_Psi_Max);

                            ws.Write();
                            ws.Dispose();

                            index++;
                        


                    }
               

            }



            fileWriter.Close();
            writer.Write();

            Console.WriteLine(sw.ElapsedMilliseconds);
        }
    }
}
