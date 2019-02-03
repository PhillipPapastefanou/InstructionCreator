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
            string filename = @"F:\ClimateData\master_hyd.ins";

            IInsFile insfile = new InsFileHydraulics();

            InsParser parser = new InsParser(filename, insfile);
            parser.Read();
            

            IInsFile hydFile = (IInsFile)insfile.Clone();
            

            //Writer w = new Writer(insfile, @"F:\Repos\guess4\build_mobile\Release\masterH-Def-CORDEXorg-re.ins");
            //w.Write();
            //w.Dispose();

            StreamWriter fileWriter = new StreamWriter("Insfiles.txt");
            StreamWriter values = new StreamWriter("Values.txt");

            //hydFile.DriverFiles.File_gridlist = "/home/hpc/pr48va/ga92wol2/driver_data/Gridlists/Amazon/TNF_CAX_K34.txt";
            //hydFile.DriverFiles.File_temp = "/home/hpc/pr48va/ga92wol2/driver_data/GLDAS2/GLDAS_1948_2010_temp_daily_half.nc";
            //hydFile.DriverFiles.File_insol = "/home/hpc/pr48va/ga92wol2/driver_data/GLDAS2/GLDAS_1948_2010_swdown_daily_half.nc";



            List<string> precDrivers = new List<string>();
            //precDrivers.Add("/home/hpc/pr48va/ga92wol2/driver_data/GLDAS2/GLDAS_1948_2010_prec_daily_half_normal_TNF.nc");
            //precDrivers.Add("/home/hpc/pr48va/ga92wol2/driver_data/GLDAS2/GLDAS_1948_2010_prec_daily_half_reduced_TNF.nc");

            precDrivers.Add("F:\\ClimateData\\GLDAS_1948_2010_prec_daily_half_normal_TNF.nc");
            precDrivers.Add("F:\\ClimateData\\GLDAS_1948_2010_prec_daily_half_reduced_TNF.nc");


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
            cavSlopes.Add(-8.0);

            List<double> isohydricities = new List<double>();
            isohydricities.Add(0.0);
            isohydricities.Add(0.3);
            isohydricities.Add(0.8);


            int index = 0;


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


                                string name = index + "run.ins";
                                //string path = @"/gpfs/scratch/pr48va/ga92wol2/ga92wol2/2019/Hydraulics_Sens_2019/";
                                fileWriter.WriteLine(name);

                                Writer ws = new Writer(hydFile, name);


                                GeneralParametersHydraulics gParams =
                                    hydFile.GeneralParameters as GeneralParametersHydraulics;

                                gParams.Hydraulic_system = HydraulicSystemType.VPD_BASED_GC;
                                gParams.NPatch = 50;

                                gParams.Alphaa_nlim = alphaA;
                                DriverFilesHydraulics hyDriverFiles = hydFile.DriverFiles as DriverFilesHydraulics;
                                hyDriverFiles.File_prec = filePrec;
                                //hyDriverFiles.File_vpd= "/home/hpc/pr48va/ga92wol2/driver_data/GLDAS2/GLDAS_1948_2010_vpd_d_daily_half.nc";
                                //hyDriverFiles.File_windspeed= "/home/hpc/pr48va/ga92wol2/driver_data/GLDAS2/GLDAS_1948_2010_windspeed_daily_half.nc";





                                //PftHyd p = insfile.Pfts["TrBE"] as PftHyd;



                                foreach (var pft in hydFile.Pfts)
                                {
                                    PftHyd pfthyd = pft as PftHyd;
                                    pfthyd.ks_max = 80.0;
                                    pfthyd.kL_max = 15.0;
                                    pfthyd.kr_max = 5.0;
                                    pfthyd.Isohydricity = iso;
                                    pfthyd.Delta_Psi_Max = 1.75;
                                    pfthyd.psi50_xylem = psi50;
                                    pfthyd.cav_slope = cavS;
                                }

                                values.Write(index + "\t");
                                values.Write(i.ToString(CultureInfo.InvariantCulture) + "\t");
                                values.Write(alphaA.ToString(CultureInfo.InvariantCulture) + "\t");
                                values.Write(iso.ToString(CultureInfo.InvariantCulture) + "\t");
                                values.Write(cavS.ToString(CultureInfo.InvariantCulture) + "\t");
                                values.Write(psi50.ToString(CultureInfo.InvariantCulture));
                                values.WriteLine();

                                ws.Write();
                                ws.Dispose();

                                index++;
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
