﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InctructionFileCreator.Parameters;

namespace InctructionFileCreator
{
    class ClusterGLDASBaseAuroraSetup
    {
        public ClusterGLDASBaseAuroraSetup(ref IInsFile hydFile)
        {
            string root_path = "/home/papa/snic2022-6-59/phillip/input/GLDAS20/";

            hydFile.DriverFiles.File_gridlist = root_path + "Amazon_basin_05.txt";
            hydFile.DriverFiles.File_temp = root_path + "GLDAS_1948_2010_temp_daily_half.nc";
            hydFile.DriverFiles.File_insol = root_path + "GLDAS_1948_2010_swdown_daily_half.nc";
            hydFile.DriverFiles.File_prec = root_path + "GLDAS_1948_2010_prec_daily_half.nc";


            hydFile.DriverFiles.File_Co2 = root_path + "co2_1764_2100_extended_rcp85.dat";
            hydFile.DriverFiles.File_Cru = root_path + "Cruncep_1901_2015.bin";
            hydFile.DriverFiles.File_Cru_Misc = root_path + "Cruncep_1901_2015misc.bin";

            DriverFilesHydraulics hyDriverFiles = hydFile.DriverFiles as DriverFilesHydraulics;

            if (hyDriverFiles != null)
            {
                hyDriverFiles.File_vpd = root_path + "GLDAS_1948_2010_vpd_Mean4day_d_daily_half.nc";
                hyDriverFiles.File_windspeed = root_path + "GLDAS_1948_2010_windspeed_daily_half.nc";
                hyDriverFiles.File_hurs = "";
                hyDriverFiles.Variable_vpd = "vpd";
                hyDriverFiles.Variable_hurs = "hursAdjust";
            }

        }
    }
}
