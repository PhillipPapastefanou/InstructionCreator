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
    class SobolSequenceInstructionFile
    {
        public SobolSequenceInstructionFile()
        {
            List<UniformParameter> uParameters = new List<UniformParameter>();

            uParameters.Add(new UniformParameter("AlphaA", 0.6, 0.9));
            uParameters.Add(new UniformParameter("Isohydricity", 0.0, 1.0));
            uParameters.Add(new UniformParameter("Psi50", -3.5, -1.0));
            uParameters.Add(new UniformParameter("CavSlope", -15.0, -2.0));
            uParameters.Add(new UniformParameter("DeltaPsiMax", 1.0, 2.5));
            uParameters.Add(new UniformParameter("RootDist", 0.0, 1.0));
            uParameters.Add(new UniformParameter("CondMult", 0.5, 2.0));

            int n = 10000;
            SobolGenerator sobolGenerator = new SobolGenerator(uParameters, n);

            sobolGenerator.Generate();

            ParameterWriter writer = new ParameterWriter(sobolGenerator, '\t');

            Stopwatch sw = Stopwatch.StartNew();


            //string filename = @"F:\SourceTreeRepos\InstructionCreator\InctructionFileCreator\InctructionFileCreator\bin\Debug\masterH-Def-GLDAS.ins";
            string filename = @"F:\ClimateData\master_hyd.ins";
            //string filename = @"F:\Repos\guess4_hydraulics\build\Release\masterH-Def-GLDAS.ins";

            IInsFile insfile = new InsFileHydraulics();

            InsParser parser = new InsParser(filename, insfile);
            parser.Read();



            InsFileHydraulics hydFile = (IInsFile)insfile.Clone() as InsFileHydraulics;


            StreamWriter fileWriter = new StreamWriter("Insfiles.txt");
            ClusterGLDASBaseLRZSetup baseSetup = new ClusterGLDASBaseLRZSetup(ref hydFile);

            List<string> precDrivers = new List<string>();
            precDrivers.Add("/home/hpc/pr48va/ga92wol2/driver_data/GLDAS2/GLDAS_1948_2010_prec_daily_half.nc");
            precDrivers.Add("/home/hpc/pr48va/ga92wol2/driver_data/GLDAS2/GLDAS_1948_2010_prec_daily_half_TNF_CAX_RED05.nc");

            string rootFolder = "Insfiles";
            Directory.CreateDirectory(rootFolder);

            int index = 0;

            for (int d = 0; d < precDrivers.Count; d++)
            {
                for (int i = 0; i < n; i++)
                {
                    string filePrec = precDrivers[d];

                    double alphaA = uParameters[0].Values[i];
                    double iso = uParameters[1].Values[i];
                    double psi50 = uParameters[2].Values[i];
                    double cavS = uParameters[3].Values[i];
                    double deltaPsiMax = uParameters[4].Values[i];
                    double rootWeight = uParameters[5].Values[i];
                    double multiplier = uParameters[6].Values[i];




                    
                    string name = index + "run.ins";
                    fileWriter.Write(name + "\n");

                    Writer ws = new Writer(hydFile, rootFolder + "//" + name);


                    GeneralParametersHydraulics gParams =
                        hydFile.GeneralParameters as GeneralParametersHydraulics;

                    gParams.Hydraulic_system = HydraulicSystemType.VPD_BASED_GC;
                    gParams.NPatch = 50;
                    gParams.Suppress_daily_output = true;
                    gParams.Suppress_annually_output = false;
                    gParams.Suppress_monthly_output = false;
                    gParams.Output_time_range = OutputTimeRangeType.Scenario;

                    gParams.Alphaa_nlim = alphaA;
                    DriverFilesHydraulics hyDriverFiles = hydFile.DriverFiles as DriverFilesHydraulics;
                    hyDriverFiles.File_prec = filePrec;

                    foreach (var pft in hydFile.Pfts)
                    {
                        PftHyd pfthyd = pft as PftHyd;
                        pfthyd.ks_max = 80.0 * multiplier;
                        pfthyd.kL_max = 5.0 * multiplier;
                        pfthyd.kr_max = 15.0 * multiplier;
                        pfthyd.Isohydricity = iso;
                        pfthyd.Delta_Psi_Max = deltaPsiMax;
                        pfthyd.psi50_xylem = psi50;
                        pfthyd.cav_slope = cavS;

                        pfthyd.Rootdist = new double[] { 0.2 + 0.7*rootWeight, 1.0 - ( 0.2 + 0.7 * rootWeight) };

                    }

                    ws.Write();
                    ws.Dispose();

                    index++;

                }


            }

            fileWriter.Close();
            Console.WriteLine(sw.ElapsedMilliseconds);
        }

    }
}
