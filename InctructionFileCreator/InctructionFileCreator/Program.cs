using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InctructionFileCreator.Parameters;

namespace InctructionFileCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = Stopwatch.StartNew();

            //string filename = @"F:\SourceTreeRepos\InstructionCreator\InctructionFileCreator\InctructionFileCreator\bin\Debug\masterH-Def-GLDAS.ins";
            //string filename = @"F:\ClimateData\master_hyd.ins";
            string filename = @"F:\Repos\guess4_hydraulics\build\Release\masterH-Def-GLDAS.ins";

            IInsFile insfile = new InsFileHydraulics();

            InsParser parser = new InsParser(filename, insfile);
            parser.Read();
            

  
            InsFileHydraulics hydFile = (IInsFile) insfile.Clone() as InsFileHydraulics;


            //Writer w = new Writer(insfile, @"F:\Repos\guess4\build_mobile\Release\masterH-Def-CORDEXorg-re.ins");
            //w.Write();
            //w.Dispose();

            StreamWriter fileWriter = new StreamWriter("Insfiles.txt");
            StreamWriter values = new StreamWriter("Values.txt");

            ClusterBaseSetup baseSetup = new ClusterBaseSetup(ref hydFile);
            

            List<string> precDrivers = new List<string>();
            precDrivers.Add("/home/hpc/pr48va/ga92wol2/driver_data/GLDAS2/GLDAS_1948_2010_prec_daily_half.nc");
            precDrivers.Add("/home/hpc/pr48va/ga92wol2/driver_data/GLDAS2/GLDAS_1948_2010_prec_daily_half_TNF_CAX_RED05.nc");

            //precDrivers.Add("F:\\ClimateData\\Amazonia\\GLDAS_1948_2010_prec_daily_half_normal_TNF.nc");
            //precDrivers.Add("F:\\ClimateData\\Amazonia\\GLDAS_1948_2010_prec_daily_half_reduced_TNF.nc");

            List<double> conductiveMultiplier = new List<double>();
            conductiveMultiplier.Add(0.5);
            conductiveMultiplier.Add(1.0);
            conductiveMultiplier.Add(2.0);

            List<double> deltaPsiMaxes = new List<double>();
            deltaPsiMaxes.Add(1.5);
            deltaPsiMaxes.Add(2.0);
            deltaPsiMaxes.Add(2.5);


            List<double> alphaAs = new List<double>();
            alphaAs.Add(0.6);
            alphaAs.Add(0.8);

            List<double> psi50s = new List<double>();
            psi50s.Add(-1.0);
            psi50s.Add(-1.5);
            psi50s.Add(-2.0);
            psi50s.Add(-2.5);
            psi50s.Add(-3.0);
            psi50s.Add(-3.5);

            List<double> cavSlopes = new List<double>();
            cavSlopes.Add(-3.0);
            cavSlopes.Add(-5.0);
            cavSlopes.Add(-15.0);

            List<double> isohydricities = new List<double>();
            isohydricities.Add(0.0);
            isohydricities.Add(0.25);
            isohydricities.Add(0.50);
            isohydricities.Add(1.0);

            List<int> rootScemes = new List<int>();
            rootScemes.Add(0);
            rootScemes.Add(1);


            int index = 0;


            string rootFolder = "Insfiles";

            Directory.CreateDirectory(rootFolder);

            for (int i = 0; i < precDrivers.Count; i++)
            {
                string filePrec = precDrivers[i];

                foreach (double alphaA in alphaAs)
                {
                    foreach (double psi50 in psi50s)
                    {
                        foreach (double cavS in cavSlopes)
                        {
                            foreach (double iso in isohydricities)
                            {
                                foreach (double deltaPsiMax in deltaPsiMaxes)
                                {
                                    foreach (double multiplier in conductiveMultiplier)
                                    {

                                        foreach (double rootSceme in rootScemes)
                                        {
                                            


                                    string name = index + "run.ins";
                                    //string path = @"/gpfs/scratch/pr48va/ga92wol2/ga92wol2/2019/Hydraulics_Sens_2019/";
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



                                    //PftHyd p = insfile.Pfts["TrBE"] as PftHyd;



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

                                        if (rootSceme == 0)
                                        {
                                            pfthyd.Rootdist = new double[]{ 0.6, 0.4};
                                        }

                                        else
                                        {
                                            pfthyd.Rootdist = new double[] { 0.4, 0.6 };
                                                }
                                    }

                                    values.Write(index + "\t");
                                    values.Write(i.ToString(CultureInfo.InvariantCulture) + "\t");
                                    values.Write(alphaA.ToString(CultureInfo.InvariantCulture) + "\t");
                                    values.Write(iso.ToString(CultureInfo.InvariantCulture) + "\t");
                                    values.Write(cavS.ToString(CultureInfo.InvariantCulture) + "\t");
                                    values.Write(psi50.ToString(CultureInfo.InvariantCulture) + "\t");
                                    values.Write(deltaPsiMax.ToString(CultureInfo.InvariantCulture) + "\t");
                                    values.Write(multiplier.ToString(CultureInfo.InvariantCulture) + "\t");
                                    values.Write(rootSceme.ToString(CultureInfo.InvariantCulture));
                                    values.Write("\n");

                                    ws.Write();
                                    ws.Dispose();

                                    index++;
                                        }

                                    }
                                }
                            }
                        }
                    }

                }
            }

            fileWriter.Close();
            values.Close();

            Console.WriteLine(sw.ElapsedMilliseconds);
            


            Console.ReadKey();
        }
    }
}
