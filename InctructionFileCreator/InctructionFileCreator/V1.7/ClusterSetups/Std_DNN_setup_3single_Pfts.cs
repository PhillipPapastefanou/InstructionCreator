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
    public class BiomClassesSinglePFTsetup
    {
        public BiomClassesSinglePFTsetup()
        {
            //string filename = @"../../hydraulics_gldas.ins";
            //string filename = "/Users/pp/Documents/Repos/guess4.1_hydraulics/cmake-build-release/TrBE/agg_hydraulics_gldas_TrBE_MP.ins";
            string filename = "/Users/pp/Documents/Repos/guess4.1_hydraulics/cmake-build-release/Freising_biom_classes/German_Ned_June2022_fix_3Pft.ins";



            InsFile41Hydraulics insfile = new InsFile41Hydraulics();

            InsParser parser = new InsParser(filename, insfile);
            parser.Read();









            //InsFileTrunk41 loeader = new InsFileTrunk41();

            //InsParser loeader_parser = new InsParser("/Users/pp/Documents/Repos/guess4.1_hydraulics/cmake-build-release/global_hyd_dummy_TeBS.ins", loeader);
            //loeader_parser.Read();
            // InsFileTrunk41 loader_file = (IInsFile)insfile.Clone() as InsFileTrunk41;




            StreamWriter fileWriter = new StreamWriter("Insfiles.txt");
            ParameterCombinationWriter writer = new ParameterCombinationWriter("Values.tsv");

            Cluster cluster = Cluster.Simba;
            ClusterDriverSetup setup = ClusterDriverSetup.GLDAS20;





            int index = 0;


            List<double> boleheights = new List<double>() { 0, 0.5, 0.75 };
            List<double> greff_min_mult = new List<double>() { 0.8, 1, 1.2 };
            List<double> leaf_long_mult = new List<double>() { 0.75, 1, 1.5 };
           
            writer.Setup(new List<string>() {"pft_id",  "boleheight", "greff_min_m", "leaflong_m"});

            string rootFolder = "Insfiles";

            Directory.CreateDirectory(rootFolder);


            for (int pft_id = 0; pft_id < 3; pft_id++)
            {



            foreach (var boleheight in boleheights)
            {
                   foreach (var greff_min_m in greff_min_mult)
                    {
                        foreach (var leaf_long_m in leaf_long_mult)
                        {
                            IInsFile hydFile = (IInsFile)insfile.Clone() as InsFile41Hydraulics;
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

                                            //hydFile.DriverFiles.File_gridlist = "/scratch/phillip/data/GLDAS20/gridlist_example3.txt";
                                            hydFile.DriverFiles.File_gridlist = "/home/phillip/gridlists/Coords_Dead_Alive_selection34.tsv";

                                        DriverFilesHyd41 driverFilesHyd41 = hydFile.DriverFiles as DriverFilesHyd41;
                                            driverFilesHyd41.File_Vpd = "";

                                        }

                                        else
                                        { Console.WriteLine("No valid cluster setup specified."); }

                                    }
                                    break;
                                default:
                                    break;
                            }




                            DriverFilesHyd41 drivers = hydFile.DriverFiles as DriverFilesHyd41;

                            drivers.Variable_Specifichum = "";

                            GeneralParameters41Hydraulics parameters = hydFile.GeneralParameters as GeneralParameters41Hydraulics;

                            parameters.State_year = 1;
                            parameters.NPatch = 50;
                            parameters.Nyear_spinup = 1000;

                          


                            string name = index + "run.ins";
                            fileWriter.Write("Insfiles/" + name + "\n");
                            Writer ws = new Writer(hydFile, rootFolder + "//" + name);

                            int run_id = 0;
                            string pft_name = String.Empty;
                            foreach (var pft in hydFile.Pfts)
                            {
                                PftHyd41 a = pft as PftHyd41;

                                if (pft_id == run_id)
                                {
                                    a.Include = true;
                                }

                                else
                                {
                                    a.Include = false;
                                }

                        

                                a.Boleheight_frac = boleheight;
                                a.Greff_min *= greff_min_m;
                                a.LeafLong *= leaf_long_m;

                                run_id++;

                            }



                            writer.AddValue("pft_id", pft_id);
                            writer.AddValue("boleheight", boleheight);
                            writer.AddValue("greff_min_m", greff_min_m);
                            writer.AddValue("leaflong_m", leaf_long_m);

                            ws.Write();
                            ws.Dispose();


                            index++;
                        }
                    

                }

            }



            }



            fileWriter.Close();
            writer.Write();


            Console.ReadKey();
        }
    }
    
}
