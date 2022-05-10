using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InctructionFileCreator.Parameters;
using InctructionFileCreator.InsFiles;

namespace InctructionFileCreator
{
    class ClusterGLDASBaseAuroraSetup
    {
        public ClusterGLDASBaseAuroraSetup(ref InsFile41Hydraulics hydFile)
        {
            string root_path = "/home/papa/snic2022-6-59/phillip/input/GLDAS20/";

            DriverFilesHyd41 driverFiles = hydFile.DriverFiles as DriverFilesHyd41;

            driverFiles.File_gridlist = root_path + "Amazon_basin_05.txt";
            driverFiles.File_temp = root_path + "GLDAS_1948_2010_temp_daily_half.nc";
            driverFiles.File_insol = root_path + "GLDAS_1948_2010_swdown_daily_half.nc";
            driverFiles.File_prec = root_path + "GLDAS_1948_2010_prec_daily_half.nc";


            driverFiles.File_Co2 = root_path + "co2_1764_2100_extended_rcp85.dat";
            driverFiles.File_Soildata = root_path + "soils_lpj.dat";
            driverFiles.File_Mip_Noy = root_path + "ndep_NOy_2011_1x1deg.nc";
            driverFiles.File_Specifichum = "";
            driverFiles.File_Mip_nhx = root_path + "ndep_NHx_2011_1x1deg.nc";


            driverFiles.Variable_Pres = "";
            driverFiles.Variable_Max_temp = "";
            driverFiles.Variable_Specifichum = "";


            //hydFile.DriverFiles.File_Cru = root_path + "Cruncep_1901_2015.bin";
            //hydFile.DriverFiles.File_Cru_Misc = root_path + "Cruncep_1901_2015misc.bin";


            if (driverFiles != null)
            {
                driverFiles.File_Vpd = root_path + "GLDAS_1948_2010_vpd_Mean4day_d_daily_half.nc";
                driverFiles.File_Wind = root_path + "GLDAS_1948_2010_windspeed_daily_half.nc";
                driverFiles.File_Relhum = "";
                driverFiles.Variable_vpd = "vpd";
                driverFiles.Variable_Relhum = "hursAdjust";

            }

        }
    }
}
