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
    class HydraulicsSpinupComp
    {
        public HydraulicsSpinupComp()
        {
            Stopwatch sw = Stopwatch.StartNew();


            //string filename = @"F:\SourceTreeRepos\InstructionCreator\InctructionFileCreator\InctructionFileCreator\bin\Debug\masterH-Def-GLDAS.ins";
            //string filename = @"F:\ClimateData\master_hyd.ins";
            string filename = @"..\..\masterHome.ins";

            IInsFile insfile = new InsFileHydraulics();

            InsParser parser = new InsParser(filename, insfile);
            parser.Read();


            InsFileHydraulics hydFile = (IInsFile)insfile.Clone() as InsFileHydraulics;


            StreamWriter fileWriter = new StreamWriter("Insfiles.txt");
            StreamWriter values = new StreamWriter("Values.tsv");

            //ClusterBaseSetup baseSetup = new ClusterBaseSetup(ref hydFile);





            List<string> precDrivers = new List<string>();
            //precDrivers.Add("/home/hpc/pr48va/ga92wol2/driver_data/GLDAS2/GLDAS_1948_2010_prec_daily_half.nc");
            //precDrivers.Add("/home/hpc/pr48va/ga92wol2/driver_data/GLDAS2/GLDAS_1948_2010_prec_daily_half_TNF_CAX_RED05.nc");

            precDrivers.Add("F:\\ClimateData\\GLDAS_1948_2010_prec_daily_half.nc");
            //precDrivers.Add("F:\\ClimateData\\Amazonia\\GLDAS_1948_2010_prec_daily_half_reduced_TNF.nc");


            List<double> nYearSpinups = new List<double>();

            //for (int i = 0; i < 10; i++)
            //{
                nYearSpinups.Add(250);
                nYearSpinups.Add(500);
                nYearSpinups.Add(750);
                nYearSpinups.Add(1000);
                nYearSpinups.Add(1250);
                nYearSpinups.Add(1500);
                nYearSpinups.Add(1750);
                nYearSpinups.Add(2000);
            //}


            int index = 0;

            string rootFolder = "Insfiles";

            Directory.CreateDirectory(rootFolder);

            for (int i = 0; i < precDrivers.Count; i++)
            {
                string filePrec = precDrivers[i];

                  foreach (int nYearSpinup in nYearSpinups)
                        {


                                        string name = index + "run.ins";
                                        //string path = @"/gpfs/scratch/pr48va/ga92wol2/ga92wol2/2019/Hydraulics_Sens_2019/";
                                        fileWriter.Write("Insfiles/" + name + "\n");

                                        Writer ws = new Writer(hydFile, rootFolder + "//" + name);


                                        GeneralParametersHydraulics gParams =
                                            hydFile.GeneralParameters as GeneralParametersHydraulics;

                                        gParams.Hydraulic_system = HydraulicSystemType.VPD_BASED_GC;
                                        gParams.NPatch = 50;
                                        gParams.Nyear_spinup = nYearSpinup;
                                        gParams.Suppress_daily_output = true;
                                        gParams.Suppress_annually_output = false;
                                        gParams.Suppress_monthly_output = false;
                                        gParams.Output_time_range = OutputTimeRangeType.Scenario;
                                        gParams.Disable_mort_greff = false;


                            DriverFilesHydraulics hyDriverFiles =
                                hydFile.DriverFiles as DriverFilesHydraulics;
                            hyDriverFiles.File_prec = filePrec;

                    values.Write(index + "\t");
                                        values.Write(i.ToString(CultureInfo.InvariantCulture) + "\t");
                                        values.Write(nYearSpinup.ToString(CultureInfo.InvariantCulture) + "\t");



                                        ws.Write();
                                        ws.Dispose();

                                        index++;

                                    }
                                }


            fileWriter.Close();
            values.Close();

            Console.WriteLine(sw.ElapsedMilliseconds);
        }

    }
}
