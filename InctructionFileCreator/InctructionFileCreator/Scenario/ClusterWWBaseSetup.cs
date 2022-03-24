using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InctructionFileCreator.Parameters;

namespace InctructionFileCreator.Scenario
{
    class ClusterWWBaseSetup
    {
        public ClusterWWBaseSetup(ref InsFileHydraulics hydFile)
        {
            //hydFile.DriverFiles.File_gridlist = "/dss/dsshome1/lxc03/ga92wol2/driver_data/Gridlists/Amazon/TNF_CAX_K34_extend.txt";
            hydFile.DriverFiles.File_gridlist = "/dss/dsshome1/lxc03/ga92wol2/driver_data/Gridlists/Amazon/Amazon_basin_05.txt";
            hydFile.DriverFiles.File_temp = "/dss/dsshome1/lxc03/ga92wol2/driver_data/WATCH_WFDEI/WATCH_WFDEI_1950_2010_temp.nc";
            hydFile.DriverFiles.File_insol = "/dss/dsshome1/lxc03/ga92wol2/driver_data/WATCH_WFDEI/WATCH_WFDEI_1950_2010_rad.nc";
            hydFile.DriverFiles.File_Co2 = "/dss/dsshome1/lxc03/ga92wol2/driver_data/Misc/co2_1764_2100_extended_rcp85.dat";
            

            DriverFilesHydraulics hyDriverFiles = hydFile.DriverFiles as DriverFilesHydraulics;

            if (hyDriverFiles != null)
            {
                hyDriverFiles.File_vpd = "/dss/dsshome1/lxc03/ga92wol2/driver_data/WATCH_WFDEI/WATCH_WFDEI_1950_2010_vpd.nc";
                hyDriverFiles.File_windspeed = "/dss/dsshome1/lxc03/ga92wol2/driver_data/WATCH_WFDEI/WATCH_WFDEI_1950_2010_windspeed.nc";
            }

        }
    }
}
