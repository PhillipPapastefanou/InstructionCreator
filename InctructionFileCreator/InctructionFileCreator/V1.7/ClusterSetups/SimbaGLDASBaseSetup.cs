using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InctructionFileCreator.Parameters;
using InctructionFileCreator.InsFiles;
namespace InctructionFileCreator.V1.ClusterSetups
{
    class SimbaGLDASBaseSetup
    {
        public SimbaGLDASBaseSetup(ref IInsFile hydFile)
        {
            string root_path = "/scratch/phillip/data/GLDAS20/";


            DriverFilesHyd41 driverFiles = hydFile.DriverFiles as DriverFilesHyd41;

            //hydFile.DriverFiles.File_gridlist = "/dss/dsshome1/lxc03/ga92wol2/driver_data/Gridlists/Amazon/TNF_CAX_K34_extend.txt";
            driverFiles.File_gridlist = root_path + "TNF_CAX_K34_extend.tsv";
            driverFiles.File_temp = root_path + "GLDAS_1948_2010_temp_daily_quarter.nc";
            driverFiles.File_insol = root_path + "GLDAS_1948_2010_swdown_daily_quarter.nc";
            driverFiles.File_prec = root_path + "GLDAS_1948_2010_prec_daily_quarter.nc";
            driverFiles.File_Co2 = root_path + "co2_1764_2100_extended_rcp85.dat";

            driverFiles.Variable_temp = "temp";
            driverFiles.Variable_prec = "prec";
            driverFiles.Variable_insol = "insol";


            driverFiles.File_Soildata = root_path + "soils_lpj.dat";
            driverFiles.File_Mip_Noy = root_path + "ndep_NOy_2011_1x1deg.nc";
            driverFiles.File_Specifichum = "";
            driverFiles.File_Mip_nhx = root_path + "ndep_NHx_2011_1x1deg.nc";

            driverFiles.Variable_Pres = "";
            driverFiles.Variable_Max_temp = "";
            driverFiles.Variable_Specifichum = "";

            DriverFilesHydraulics hyDriverFiles = hydFile.DriverFiles as DriverFilesHydraulics;

            if (driverFiles != null)
            {
                driverFiles.File_Vpd = root_path + "GLDAS_1948_2010_vpd_Mean4day_d_daily_half.nc";
                driverFiles.File_Wind = root_path + "GLDAS_1948_2010_windspeed_daily_quarter.nc";
                driverFiles.File_Relhum = "";
                driverFiles.Variable_vpd = "vpd";
                driverFiles.Variable_Relhum = "hursAdjust";
                driverFiles.Variable_Wind = "windspeed";
            }
        }
    }
}

