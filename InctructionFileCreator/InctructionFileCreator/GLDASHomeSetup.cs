using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InctructionFileCreator.Parameters;

namespace InctructionFileCreator
{
    class GLDASHomeSetup
    {
        public GLDASHomeSetup(ref InsFileHydraulics hydFile)
        {
            //hydFile.DriverFiles.File_gridlist = "/dss/dsshome1/lxc03/ga92wol2/driver_data/Gridlists/Amazon/TNF_CAX_K34_extend.txt";
            hydFile.DriverFiles.File_gridlist = "F:\\ClimateData\\Amazon\\Amazon_basin_coords.txt";
            hydFile.DriverFiles.File_temp = "F:\\ClimateData\\GLDAS_1948_2010_temp_daily_half.nc";
            hydFile.DriverFiles.File_insol = "F:\\ClimateData\\GLDAS_1948_2010_swdown_daily_half.nc";
            hydFile.DriverFiles.File_prec = "F:\\ClimateData\\GLDAS_1948_2010_prec_daily_half.nc";
            //hydFile.DriverFiles.File_Co2 = "/dss/dsshome1/lxc03/ga92wol2/driver_data/Misc/co2_1764_2100_extended_rcp85.dat";
            //hydFile.DriverFiles.File_Cru = "/dss/dsshome1/lxc03/ga92wol2/driver_data/Misc/Cruncep_1901_2015.bin";
            //hydFile.DriverFiles.File_Cru_Misc = "/dss/dsshome1/lxc03/ga92wol2/driver_data/Misc/Cruncep_1901_2015misc.bin";

            DriverFilesHydraulics hyDriverFiles = hydFile.DriverFiles as DriverFilesHydraulics;

            if (hyDriverFiles != null)
            {
                hyDriverFiles.File_vpd = "F:\\ClimateData\\GLDAS_1948_2010_vpd_Mean4day_d_daily_half.nc";
                hyDriverFiles.File_windspeed = "F:\\ClimateData\\GLDAS_1948_2010_windspeed_daily_half.nc";
            }
        }
    }
}
