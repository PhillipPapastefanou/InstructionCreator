using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InctructionFileCreator.Parameters;

namespace InctructionFileCreator
{
    class ClusterBaseSetup
    {
        public ClusterBaseSetup(ref InsFileHydraulics hydFile)
        {
            hydFile.DriverFiles.File_gridlist = "/home/hpc/pr48va/ga92wol2/driver_data/Gridlists/Amazon/TNF_CAX_K34.txt";
            hydFile.DriverFiles.File_temp = "/home/hpc/pr48va/ga92wol2/driver_data/GLDAS2/GLDAS_1948_2010_temp_daily_half.nc";
            hydFile.DriverFiles.File_insol = "/home/hpc/pr48va/ga92wol2/driver_data/GLDAS2/GLDAS_1948_2010_swdown_daily_half.nc";
            hydFile.DriverFiles.File_Co2 = "/home/hpc/pr48va/ga92wol2/driver_data/Misc/co2_1764_2100_extended_rcp85.dat";
            hydFile.DriverFiles.File_Cru = "/home/hpc/pr48va/ga92wol2/driver_data/Misc/Cruncep_1901_2015.bin";
            hydFile.DriverFiles.File_Cru_Misc = "/home/hpc/pr48va/ga92wol2/driver_data/Misc/Cruncep_1901_2015misc.bin";

            DriverFilesHydraulics hyDriverFiles = hydFile.DriverFiles as DriverFilesHydraulics;

            if (hyDriverFiles != null)
            {
                hyDriverFiles.File_vpd = "/home/hpc/pr48va/ga92wol2/driver_data/GLDAS2/GLDAS_1948_2010_vpd_d_daily_half.nc";
                hyDriverFiles.File_windspeed = "/home/hpc/pr48va/ga92wol2/driver_data/GLDAS2/GLDAS_1948_2010_windspeed_daily_half.nc";
            }

        }
    }
}
