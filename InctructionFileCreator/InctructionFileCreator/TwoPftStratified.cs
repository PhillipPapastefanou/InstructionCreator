using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InctructionFileCreator.Parameters;

namespace InctructionFileCreator
{
    class TwoPftStratified
    {
        public TwoPftStratified()
        {
            Stopwatch sw = Stopwatch.StartNew();


            //string filename = @"F:\SourceTreeRepos\InstructionCreator\InctructionFileCreator\InctructionFileCreator\bin\Debug\masterH-Def-GLDAS.ins";
            //string filename = @"F:\ClimateData\master_hyd.ins";
            string filename = @"..\..\masterN.ins";

            IInsFile insfile = new InsFileHydraulics();

            InsParser parser = new InsParser(filename, insfile);
            parser.Read();
            

            InsFileHydraulics hydFile = (IInsFile) insfile.Clone() as InsFileHydraulics;


            StreamWriter fileWriter = new StreamWriter("Insfiles.txt");
            StreamWriter values = new StreamWriter("Values.tsv");

            ClusterBaseSetup baseSetup = new ClusterBaseSetup(ref hydFile);





            List<string> precDrivers = new List<string>();
            precDrivers.Add("/home/hpc/pr48va/ga92wol2/driver_data/GLDAS2/GLDAS_1948_2010_prec_daily_half.nc");
            precDrivers.Add("/home/hpc/pr48va/ga92wol2/driver_data/GLDAS2/GLDAS_1948_2010_prec_daily_half_TNF_CAX_RED05.nc");

            //precDrivers.Add("F:\\ClimateData\\Amazonia\\GLDAS_1948_2010_prec_daily_half_normal_TNF.nc");
            //precDrivers.Add("F:\\ClimateData\\Amazonia\\GLDAS_1948_2010_prec_daily_half_reduced_TNF.nc");

            List<Pair<double>> conductiveMultiplier = new List<Pair<double>>();
            conductiveMultiplier.Add(new Pair<double>(1.0, 0.75));
            conductiveMultiplier.Add(new Pair<double>(1.0, 0.5));
            conductiveMultiplier.Add(new Pair<double>(1.0, 0.3));
            conductiveMultiplier.Add(new Pair<double>(0.7, 0.5));
            conductiveMultiplier.Add(new Pair<double>(0.7, 0.3));


            List<double> deltaPsiMaxes = new List<double>();
            deltaPsiMaxes.Add(1.2);
            deltaPsiMaxes.Add(1.7);

            List<double> alphaAs = new List<double>();
            alphaAs.Add(0.8);

            List<Pair<double>> psi50s = new List<Pair<double>>();
            psi50s.Add(new Pair<double>(-2.5, -3.2));
            psi50s.Add(new Pair<double>(-2.5, -3.0));
            psi50s.Add(new Pair<double>(-2.5, -2.8));
            psi50s.Add(new Pair<double>(-2.3, -3.0));
            psi50s.Add(new Pair<double>(-2.3, -2.8));
            psi50s.Add(new Pair<double>(-2.3, -2.5));
            psi50s.Add(new Pair<double>(-2.0, -2.9));
            psi50s.Add(new Pair<double>(-2.0, -2.7));
            psi50s.Add(new Pair<double>(-2.0, -2.5));
            psi50s.Add(new Pair<double>(-1.7, -2.5));
            psi50s.Add(new Pair<double>(-1.7, -2.0));

            List<double> cavSlopes = new List<double>();
            cavSlopes.Add(-3.0);
            cavSlopes.Add(-5.0);
            cavSlopes.Add(-15.0);


            List<Pair<double>> isohydricities = new List<Pair<double>>();
            isohydricities.Add(new Pair<double>(0.8, -0.3));
            isohydricities.Add(new Pair<double>(0.6, -0.3));
            isohydricities.Add(new Pair<double>(0.8, 0.0));
            isohydricities.Add(new Pair<double>(0.6, 0.0));
            isohydricities.Add(new Pair<double>(0.8, 0.3));
            isohydricities.Add(new Pair<double>(0.6, 0.3));


            int index = 0;




            string rootFolder = "Insfiles";

            Directory.CreateDirectory(rootFolder);

            for (int i = 0; i < precDrivers.Count; i++)
            {
                string filePrec = precDrivers[i];
  
                    foreach (Pair<double> psi50 in psi50s)
                    {
                        foreach (double cavS in cavSlopes)
                        {
                            foreach (Pair<double> iso in isohydricities)
                            {
                                foreach (double deltaPsiMax in deltaPsiMaxes)
                                {
                                    foreach (Pair<double> multiplier in conductiveMultiplier)
                                    {
                                       
                                            string name = index + "run.ins";
                                            //string path = @"/gpfs/scratch/pr48va/ga92wol2/ga92wol2/2019/Hydraulics_Sens_2019/";
                                            fileWriter.Write("Insfiles/" + name + "\n");

                                            Writer ws = new Writer(hydFile, rootFolder + "//" + name);


                                            GeneralParametersHydraulics gParams =
                                                hydFile.GeneralParameters as GeneralParametersHydraulics;

                                            gParams.Hydraulic_system = HydraulicSystemType.VPD_BASED_GC;
                                            gParams.NPatch = 50;
                                            gParams.Suppress_daily_output = true;
                                            gParams.Suppress_annually_output = false;
                                            gParams.Suppress_monthly_output = false;
                                            gParams.Output_time_range = OutputTimeRangeType.Scenario;

                                            gParams.Alphaa_nlim = 0.8;
                                            DriverFilesHydraulics hyDriverFiles = hydFile.DriverFiles as DriverFilesHydraulics;
                                            hyDriverFiles.File_prec = filePrec;


                                        PftHyd pft_iso = insfile.Pfts["TrBE_iso"] as PftHyd;
                                        pft_iso.ks_max = 80.0 * multiplier.A;
                                        pft_iso.kL_max = 5.0 * multiplier.A;
                                        pft_iso.kr_max = 15.0 * multiplier.A;
                                        pft_iso.Isohydricity = iso.A;
                                        pft_iso.Delta_Psi_Max = deltaPsiMax;
                                        pft_iso.psi50_xylem = psi50.A;
                                        pft_iso.cav_slope = cavS;
                                        pft_iso.Rootdist = new double[] { 0.6, 0.4 };


                                        PftHyd pft_ansio = insfile.Pfts["TrBE_aniso"] as PftHyd;
                                        pft_ansio.ks_max = 80.0 * multiplier.B;
                                        pft_ansio.kL_max = 5.0 * multiplier.B;
                                        pft_ansio.kr_max = 15.0 * multiplier.B;
                                        pft_ansio.Isohydricity = iso.B;
                                        pft_ansio.Delta_Psi_Max = deltaPsiMax;
                                        pft_ansio.psi50_xylem = psi50.B;
                                        pft_ansio.cav_slope = cavS;
                                        pft_ansio.Rootdist = new double[] { 0.4, 0.6 };



                                        values.Write(index + "\t");
                                        values.Write(i.ToString(CultureInfo.InvariantCulture) + "\t");
                                        values.Write("0.8" + "\t");
                                        values.Write(iso.ToString() + "\t");
                                        values.Write(cavS.ToString(CultureInfo.InvariantCulture) + "\t");
                                        values.Write(psi50.ToString() + "\t");
                                        values.Write(deltaPsiMax.ToString(CultureInfo.InvariantCulture) + "\t");
                                        values.Write(multiplier.ToString() + "\t");
                                        values.Write("\n");

                                        hydFile.Pfts[0] = pft_iso;
                                        hydFile.Pfts[1] = pft_ansio;


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
            values.Close();

            Console.WriteLine(sw.ElapsedMilliseconds);
        }

    }
}
