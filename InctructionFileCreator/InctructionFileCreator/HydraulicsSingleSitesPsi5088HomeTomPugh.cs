using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InctructionFileCreator.Parameters;

namespace InctructionFileCreator
{

   

    class HydraulicsSingleSitesPsi5088HomeTomPugh
    {

        private List<double> psi50s;
        private List<double> cavSlopes;
        private List<double> mults;
        private List<double> slas;
        private List<double> kLatosScales;
        private List<double[]> rootDists;




        //List<double> lambdas = new List<double>(){-0.08, 0.15, 0.49};
        private List<double> lambdas;
         //List<double> deltaPsiWW = new List<double>(){0.62, 1.23, 2.15};
        private List<double> deltaPsiWW;
        //pft_iso.Delta_Psi_Max = 1.23;

        //pft_iso.Isohydricity = -0.08;
        //pft_iso.Delta_Psi_Max = 0.62;

        public HydraulicsSingleSitesPsi5088HomeTomPugh(){
            
        
        // ReadParameters(
        //   "F:\\Dropbox\\UNI\\Projekte\\03_Hydraulics_Implementation\\Analysis\\CavCurves\\CombosOfModelForPaper.tsv");
        ReadParameters(@"F:\Dropbox\UNI\Projekte\A03_Hydraulics_Implementation\MULTS-SLA-All-latosaScales.csv");


        string filename = @"F:\Dropbox\UNI\Projekte\A03_Hydraulics_Implementation\masterHomeWork_CAX.ins";
            List<double> alphaA = new List<double>(){0.7};
            List<double> multmult = new List<double>() {0.5, 0.7, 0.8};
           // List<double> slaScale = new List<double>() {0.75, 1.25};
            List<double> slaScale = new List<double>() {1.0};
            //List<double> respcoeffs = new List<double>() {0.1, 0.15};
            List<double> respcoeffs = new List<double>() {0.15};

            List<double> kLatosaMult = new List<double>() {5000,6000, 7000, 8000,9000};

            List<int> distintervals = new List<int>() {200, 400 };
            rootDists = new List<double[]>();
            rootDists.Add(new double[] {0.6, 0.4});
            rootDists.Add(new double[] {0.4, 0.6});

            int index = 0;


            StreamWriter fileWriter = new StreamWriter("Insfiles.txt");
            StreamWriter values = new StreamWriter("Values.tsv");


            for (int driverIndex = 0; driverIndex < 2; driverIndex++)
            {


                IInsFile insfile = new InsFileHydraulics();

                InsParser parser = new InsParser(filename, insfile);
                parser.Read();


                InsFileHydraulics hydFile = (IInsFile) insfile.Clone() as InsFileHydraulics;



                DriverFilesHydraulics driverFiles = hydFile.DriverFiles as DriverFilesHydraulics;


                List<string> precDrivers = new List<string>();

                if (driverIndex == 0)
                {
                    driverFiles.File_vpd = "F:\\ClimateData\\GLDAS_1948_2010_vpd_sunny_d_daily_half.nc";
                    precDrivers.Add("F:\\ClimateData\\GLDAS_1948_2010_prec_daily_half.nc");
                    //precDrivers.Add("F:\\ClimateData\\GLDAS_1948_2010_prec_daily_half_TNF_CAX_RED05_EndTime.nc");

                }

                else
                {
                    WatchHomeSetup homeWatchSetup = new WatchHomeSetup(ref hydFile);

                    driverFiles.File_vpd = "F:\\ClimateData\\WATCH_WFDEI_1950_2010_vpd.nc";
                    precDrivers.Add("F:\\ClimateData\\WATCH_WFDEI_1950_2010_prec.nc");
                    //precDrivers.Add("F:\\ClimateData\\WATCH_WFDEI_1950_2010_prec_TNF_CAX_RED05_EndTime.nc");
                }






                driverFiles.File_gridlist = "F:\\ClimateData\\Amazon\\TNF_CAX_K34_extend.txt";
                driverFiles.File_gridlist = "F:\\ClimateData\\Amazon\\TNF_K34_extend.txt";
                driverFiles.File_gridlist = "F:\\ClimateData\\Amazon\\2005zones.txt";



               


                string rootFolder = "Insfiles";
                Directory.CreateDirectory(rootFolder);

                //double[] isohydricities = new double[] {0.7,0.7, 0.0, -0.1};
                //double[] isohydricities = new double[] {0.5,0.3, 0.0, -0.1};
                //double[] isohydricities = new double[] {0.0,0.0, 0.0, 0.0};

                for (int i = 0; i < precDrivers.Count; i++)
                {
                    string filePrec = precDrivers[i];

                    for (int mm = 0; mm < multmult.Count; mm++)
                    {
                        for (int aa = 0; aa < alphaA.Count; aa++)
                        {

                            for (int d = 0; d < distintervals.Count; d++)
                            {

                                for (int rD = 0; rD < rootDists.Count; rD++)
                                {
                                    


                            for (int resp = 0; resp < respcoeffs.Count; resp++)
                            {

                                for (int klatosaIndex = 0; klatosaIndex < kLatosaMult.Count; klatosaIndex++)
                                {


                                for (int sla = 0; sla < slaScale.Count; sla++)
                                {
                                    int[] indexesSel = {16, 17};
                                    foreach (int j in indexesSel)
                                        //for (int j = 0; j < psi50s.Count; j++)
                                        //for (int j = 0; j < 2; j++)
                                    {

                                        string name = index + "run.ins";
                                        //string path = @"/gpfs/scratch/pr48va/ga92wol2/ga92wol2/2019/Hydraulics_Sens_2019/";
                                        fileWriter.Write("Insfiles/" + name + "\n");

                                        Writer ws = new Writer(hydFile, rootFolder + "//" + name);

                                        GeneralParametersHydraulics gParams =
                                            hydFile.GeneralParameters as GeneralParametersHydraulics;

                                        gParams.Hydraulic_system = HydraulicSystemType.VPD_BASED_GC;
                                        gParams.NPatch = 50;
                                        gParams.Nyear_spinup = 1000;
                                        gParams.DistInterval = distintervals[d];
                                        gParams.Alphaa_nlim = alphaA[aa];
                                        gParams.Suppress_daily_output = false;
                                        gParams.Suppress_annually_output = false;
                                        gParams.Suppress_monthly_output = false;
                                        gParams.Output_time_range = OutputTimeRangeType.Scenario;
                                        gParams.Disable_mort_greff = false;
                                        

                                        gParams.IfCalcSLA = false;

                                        DriverFilesHydraulics hyDriverFiles =
                                            hydFile.DriverFiles as DriverFilesHydraulics;
                                        hyDriverFiles.File_prec = filePrec;


                                        double psi50 = psi50s[j];
                                        double cavS = cavSlopes[j];


                                        PftHyd c3g = insfile.Pfts["C3G"] as PftHyd;
                                        c3g.Sla = 26.0;

                                        PftHyd c4g = insfile.Pfts["C4G"] as PftHyd;
                                        c4g.Sla = 26.0;


                                        hydFile.Pfts[1] = c3g;
                                        hydFile.Pfts[2] = c4g;


                                        PftHyd pft_iso = insfile.Pfts["TrBE"] as PftHyd;

                                        pft_iso.psi50_xylem = psi50;
                                        pft_iso.cav_slope = cavS;

                                        pft_iso.Sla = slas[j] * slaScale[sla];

                                        pft_iso.K_LaToSa = kLatosScales[j] * kLatosaMult[klatosaIndex];

                                        pft_iso.Rootdist = rootDists[rD];
                                        pft_iso.RespCoeff = respcoeffs[resp];

                                        pft_iso.GA = 0.005;
                                        pft_iso.CrownArea_Max = 150.0;
                                        pft_iso.Lambda_max = 0.90;

                                        pft_iso.GMin = 1.0;

                                        //pft_iso.Isohydricity = lambdas[j];
                                        //pft_iso.Delta_Psi_Max = deltaPsiWW[j];

                                        pft_iso.Isohydricity = 0.15;
                                        pft_iso.Delta_Psi_Max = 1.23;
                                        pft_iso.Longevity = 600;


                                        double multiplier = mults[j] * multmult[mm];
                                        pft_iso.ks_max = 80.0 * multiplier;
                                        pft_iso.kL_max = 5.0 * multiplier;
                                        pft_iso.kr_max = 15.0 * multiplier;

                                        //pft_iso.K_rp = 1.5;
                                        //pft_iso.K_allom1 = 374;
                                        //pft_iso.K_allom2 = 36;
                                        //pft_iso.K_allom3 = 0.58;

                                        if (index == 0)
                                        {
                                            values.Write("DriverIndex" + "\t");
                                            values.Write("InsfileIndex" + "\t");
                                            values.Write("PrecipitationDriver" + "\t");
                                            values.Write("Psi50" + "\t");
                                            values.Write("CavSlope" + "\t");
                                            values.Write("IsohydricityLambda" + "\t");
                                            values.Write("ConductivityScaler" + "\t");
                                            values.Write("AlphaA" + "\t");
                                            values.Write("RespCoeff" + "\t");
                                            values.Write("SLA" + "\t");
                                            values.Write("Distinterval" + "\t");
                                            values.Write("KlatosaScale" + "\t");
                                            values.Write("ConductivityScaler" + "\t");
                                            values.Write("RootDist");
                                            values.Write("\n");
                                        }

                                        values.Write(driverIndex + "\t");
                                        values.Write(index + "\t");
                                        values.Write(i.ToString(CultureInfo.InvariantCulture) + "\t");
                                        values.Write(psi50.ToString(CultureInfo.InvariantCulture) + "\t");
                                        values.Write(cavS.ToString(CultureInfo.InvariantCulture) + "\t");
                                        values.Write("0.15" + "\t");
                                        values.Write(multiplier.ToString(CultureInfo.InvariantCulture) + "\t");
                                        values.Write(alphaA[aa].ToString(CultureInfo.InvariantCulture) + "\t");
                                        values.Write(respcoeffs[resp].ToString(CultureInfo.InvariantCulture) + "\t");
                                        values.Write(slas[j].ToString(CultureInfo.InvariantCulture) + "\t");
                                        values.Write(distintervals[d].ToString(CultureInfo.InvariantCulture) + "\t");
                                        values.Write(kLatosaMult[klatosaIndex].ToString(CultureInfo.InvariantCulture)+ "\t");
                                        values.Write(multmult[mm].ToString(CultureInfo.InvariantCulture)+ "\t");
                                        values.Write(rootDists[rD][0].ToString(CultureInfo.InvariantCulture));

                                        values.Write("\n");

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
                        }
                    }

                }
            }


            fileWriter.Close();
            values.Close();


        }


        private void ReadParameters(string filename)
{
    psi50s = new List<double>();
    cavSlopes = new List<double>();
    mults = new List<double>();
    slas = new List<double>();
    deltaPsiWW = new List<double>();
    lambdas = new List<double>();
    kLatosScales = new List<double>();


    using (StreamReader reader = File.OpenText(filename))
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(reader.ReadToEnd());

        string[] lines = sb.ToString().Split(new[] { "\r\n" }, StringSplitOptions.None);

        foreach (string line in lines)
        {
            string[] dataLine = line.Split(',');

            double psi50 = Convert.ToDouble(dataLine[0], CultureInfo.InvariantCulture);
            double slope = Convert.ToDouble(dataLine[1], CultureInfo.InvariantCulture);
            double mult = Convert.ToDouble(dataLine[2], CultureInfo.InvariantCulture);
            double sla = Convert.ToDouble(dataLine[3], CultureInfo.InvariantCulture);
            double iso = Convert.ToDouble(dataLine[4], CultureInfo.InvariantCulture);
            double deltaPWW = Convert.ToDouble(dataLine[5], CultureInfo.InvariantCulture);
            double klatosaScale = Convert.ToDouble(dataLine[6], CultureInfo.InvariantCulture);

            psi50s.Add(psi50);
            cavSlopes.Add(slope);
            mults.Add(mult);
            slas.Add(sla);
            lambdas.Add(iso);
            deltaPsiWW.Add(deltaPWW);
            kLatosScales.Add(klatosaScale);
        }
    }

}
    }
}
