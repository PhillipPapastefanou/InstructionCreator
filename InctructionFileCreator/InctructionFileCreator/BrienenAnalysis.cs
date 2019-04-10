using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InctructionFileCreator.Parameters;
using SensitivitySetup;

namespace InctructionFileCreator
{
    class BrienenAnalysis
    {
        public BrienenAnalysis()
        {
            int nValuesPerParameter = 10;


            UniformParameter alphaA = new UniformParameter("AlphaA", 0.4, 1.0);
            UniformParameter psi50 = new UniformParameter("Psi50", -4.0, -1.0);
            UniformParameter cavSlope = new UniformParameter("CavSlope", -20.0, -2.0);
            UniformParameter isohyd = new UniformParameter("Isohydricity", -0.3, 1.0);
            UniformParameter deltaPsiMax = new UniformParameter("DeltaPsiMax", 0.3, 3.0);
            UniformParameter condScaler = new UniformParameter("ConductivityScaler", 0.5, 2.0);
            UniformParameter rootDepthScaler = new UniformParameter("RootDepthScaler", 0.0, 1.0);



            alphaA.DivideEqually(nValuesPerParameter);
            psi50.DivideEqually(nValuesPerParameter);
            cavSlope.DivideEqually(nValuesPerParameter);
            isohyd.DivideEqually(nValuesPerParameter);
            deltaPsiMax.DivideEqually(nValuesPerParameter);
            condScaler.DivideEqually(nValuesPerParameter);
            rootDepthScaler.DivideEqually(nValuesPerParameter);




            string filename = @"F:\ClimateData\master_hyd.ins";

            IInsFile insfile = new InsFileHydraulics();

            InsParser parser = new InsParser(filename, insfile);
            parser.Read();


            InsFileHydraulics hydFile = (IInsFile)insfile.Clone() as InsFileHydraulics;


            StreamWriter fileWriter = new StreamWriter("Insfiles.txt");
            ClusterBaseSetup baseSetup = new ClusterBaseSetup(ref hydFile);

            hydFile.DriverFiles.File_gridlist = "/home/hpc/pr48va/ga92wol2/driver_data/Gridlists/Amazon/Brienen_coords.txt";
            hydFile.DriverFiles.File_prec = "/home/hpc/pr48va/ga92wol2/driver_data/GLDAS2/GLDAS_1948_2010_prec_daily_half.nc";

            string rootFolder = "Insfiles";
            Directory.CreateDirectory(rootFolder);

            Stopwatch sw = Stopwatch.StartNew();

            int index = 0;

            for (int i = 0; i < nValuesPerParameter; i++)
            {
                string name = index + "run.ins";
                fileWriter.Write("Insfiles/" + name + "\n");

                SetupBase(hydFile);

                GeneralParametersHydraulics gParams =
                    hydFile.GeneralParameters as GeneralParametersHydraulics;

                gParams.Alphaa_nlim = alphaA.Values[i];

                Writer ws = new Writer(hydFile, rootFolder + "//" + name);

                ws.Write();
                ws.Dispose();
                index++;
            }

            for (int i = 0; i < nValuesPerParameter; i++)
            {
                string name = index + "run.ins";
                fileWriter.Write("Insfiles/" + name + "\n");

                SetupBase(hydFile);

                foreach (var pft in hydFile.Pfts)
                {
                    PftHyd pfthyd = pft as PftHyd;
                    pfthyd.psi50_xylem = psi50.Values[i];
                }


                Writer ws = new Writer(hydFile, rootFolder + "//" + name);

                ws.Write();
                ws.Dispose();
                index++;
            }

            for (int i = 0; i < nValuesPerParameter; i++)
            {
                string name = index + "run.ins";
                fileWriter.Write("Insfiles/" + name + "\n");

                SetupBase(hydFile);

                foreach (var pft in hydFile.Pfts)
                {
                    PftHyd pfthyd = pft as PftHyd;
                    pfthyd.cav_slope = cavSlope.Values[i];
                }


                Writer ws = new Writer(hydFile, rootFolder + "//" + name);

                ws.Write();
                ws.Dispose();
                index++;
            }

            for (int i = 0; i < nValuesPerParameter; i++)
            {
                string name = index + "run.ins";
                fileWriter.Write("Insfiles/" + name + "\n");

                SetupBase(hydFile);

                foreach (var pft in hydFile.Pfts)
                {
                    PftHyd pfthyd = pft as PftHyd;
                    pfthyd.Isohydricity = isohyd.Values[i];
                }


                Writer ws = new Writer(hydFile, rootFolder + "//" + name);

                ws.Write();
                ws.Dispose();
                index++;
            }

            for (int i = 0; i < nValuesPerParameter; i++)
            {
                string name = index + "run.ins";
                fileWriter.Write("Insfiles/" + name + "\n");

                SetupBase(hydFile);

                foreach (var pft in hydFile.Pfts)
                {
                    PftHyd pfthyd = pft as PftHyd;
                    pfthyd.Delta_Psi_Max = deltaPsiMax.Values[i];
                }


                Writer ws = new Writer(hydFile, rootFolder + "//" + name);

                ws.Write();
                ws.Dispose();
                index++;
            }

            for (int i = 0; i < nValuesPerParameter; i++)
            {
                string name = index + "run.ins";
                fileWriter.Write("Insfiles/" + name + "\n");

                SetupBase(hydFile);

                foreach (var pft in hydFile.Pfts)
                {
                    PftHyd pfthyd = pft as PftHyd;
                    pfthyd.ks_max = 80.0 * condScaler.Values[i];
                    pfthyd.kL_max = 5.0 * condScaler.Values[i];
                    pfthyd.kr_max = 15.0 * condScaler.Values[i];
                }


                Writer ws = new Writer(hydFile, rootFolder + "//" + name);

                ws.Write();
                ws.Dispose();
                index++;
            }

            for (int i = 0; i < nValuesPerParameter; i++)
            {
                string name = index + "run.ins";
                fileWriter.Write("Insfiles/" + name + "\n");

                SetupBase(hydFile);

                foreach (var pft in hydFile.Pfts)
                {
                    PftHyd pfthyd = pft as PftHyd;
                    pfthyd.Rootdist = new double[] { 0.2 + 0.7 * rootDepthScaler.Values[i], 1.0 - (0.2 + 0.7 * rootDepthScaler.Values[i]) };
                }

                Writer ws = new Writer(hydFile, rootFolder + "//" + name);

                ws.Write();
                ws.Dispose();
                index++;
            }



            fileWriter.Close();
            Console.WriteLine(sw.ElapsedMilliseconds);

        }
        
        private void SetupBase(InsFileHydraulics hydFile)
        {

            GeneralParametersHydraulics gParams =
                hydFile.GeneralParameters as GeneralParametersHydraulics;

            gParams.Hydraulic_system = HydraulicSystemType.VPD_BASED_GC;
            gParams.NPatch = 50;
            gParams.Suppress_daily_output = true;
            gParams.Suppress_annually_output = false;
            gParams.Suppress_monthly_output = false;
            gParams.Output_time_range = OutputTimeRangeType.Scenario;


            gParams.Alphaa_nlim = 0.6;
            DriverFilesHydraulics hyDriverFiles = hydFile.DriverFiles as DriverFilesHydraulics;

            foreach (var pft in hydFile.Pfts)
            {
                PftHyd pfthyd = pft as PftHyd;
                pfthyd.ks_max = 80.0;
                pfthyd.kL_max = 5.0;
                pfthyd.kr_max = 15.0;
                pfthyd.Isohydricity = 0.0;
                pfthyd.Delta_Psi_Max = 2.0;
                pfthyd.psi50_xylem = -2.5;
                pfthyd.cav_slope = -5.0;

                pfthyd.Rootdist = new double[] { 0.6, 0.4};
            }

        }

    }



}
