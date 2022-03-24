using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InctructionFileCreator.Parameters;

namespace InctructionFileCreator
{
    class ClusterGLDASBaseSetupAfrica
    {
        public ClusterGLDASBaseSetupAfrica(ref InsFileHydraulics hydFile)
        {
            string root_path = "/gpfs/scratch/pr48va/ga92wol2/ga92wol2/data/Africa/";

            //hydFile.DriverFiles.File_gridlist = "/dss/dsshome1/lxc03/ga92wol2/driver_data/Gridlists/Amazon/TNF_CAX_K34_extend.txt";
            hydFile.DriverFiles.File_gridlist = "/dss/dsshome1/lxc03/ga92wol2/driver_data/Gridlists/Africa/CoordsAfricaTropicalRain_025.tsv";
            hydFile.DriverFiles.File_temp = root_path + "GLDAS_1948_2010_temp_daily_quarter.nc";
            hydFile.DriverFiles.File_insol = root_path + "GLDAS_1948_2010_swdown_daily_quarter.nc";
            hydFile.DriverFiles.File_prec = root_path + "GLDAS_1948_2010_prec_daily_quarter.nc";
            hydFile.DriverFiles.File_Co2 = "/dss/dsshome1/lxc03/ga92wol2/driver_data/Misc/co2_1764_2100_extended_rcp85.dat";

            DriverFilesHydraulics hyDriverFiles = hydFile.DriverFiles as DriverFilesHydraulics;

            if (hyDriverFiles != null)
            {
                hyDriverFiles.File_vpd = root_path + "GLDAS_1948_2010_VPD_allmean_quarter.nc";
                hyDriverFiles.File_windspeed = root_path + "GLDAS_1948_2010_windspeed_daily_quarter.nc";
                hyDriverFiles.File_hurs = "";
                hyDriverFiles.Variable_vpd = "vpd";
                hyDriverFiles.Variable_hurs = "hursAdjust";
            }

        }
    }
}
