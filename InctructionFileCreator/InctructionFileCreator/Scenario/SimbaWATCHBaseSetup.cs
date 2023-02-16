using System;
using InctructionFileCreator.Parameters;
namespace InctructionFileCreator.Scenario
{
    class SimbaWATCHBaseSetup
    {
        public SimbaWATCHBaseSetup(ref IInsFile hydFile)
        {

            string root_path = "/scratch/phillip/data/WATCH_WFDEI/";

            //hydFile.DriverFiles.File_gridlist = "/dss/dsshome1/lxc03/ga92wol2/driver_data/Gridlists/Amazon/TNF_CAX_K34_extend.txt";
            hydFile.DriverFiles.File_gridlist = root_path + "Amazon_basin_05.txt";
            hydFile.DriverFiles.File_temp = root_path + "WATCH_WFDEI_1950_2010_temp.nc";
            hydFile.DriverFiles.File_insol = root_path + "WATCH_WFDEI_1950_2010_rad.nc";
            hydFile.DriverFiles.File_prec = root_path + "WATCH_WFDEI_1950_2010_prec.nc";
            hydFile.DriverFiles.File_Co2 = root_path + "co2_1764_2100_extended_rcp85.dat";
            hydFile.DriverFiles.File_Cru = root_path + "Cruncep_1901_2015.bin";
            hydFile.DriverFiles.File_Cru_Misc = root_path + "Cruncep_1901_2015misc.bin";

            DriverFilesHydraulics hyDriverFiles = hydFile.DriverFiles as DriverFilesHydraulics;

            if (hyDriverFiles != null)
            {
                hyDriverFiles.File_vpd = root_path + "WATCH_WFDEI_1950_2010_vpd.nc";
                hyDriverFiles.File_windspeed = root_path + "WATCH_WFDEI_1950_2010_windspeed.nc";
                hyDriverFiles.File_hurs = "";
                hyDriverFiles.Variable_vpd = "vpd";
                hyDriverFiles.Variable_hurs = "hursAdjust";
            }
        }
    }
    
}
