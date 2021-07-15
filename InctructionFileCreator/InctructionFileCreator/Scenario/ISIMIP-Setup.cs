using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InctructionFileCreator.Parameters;

namespace InctructionFileCreator.V1._7.ClusterSetups
{
    enum ScenarioType
    {
        RCP_126,
        RCP_585
    }

    enum EarthSystemModelType
    {
        ESM4,
        IPSL,
        MPI,
        MRI,
        UKESM
    }

    class ISIMIP_Setup_v174
    {
        
        public ISIMIP_Setup_v174(ref InsFileHydraulics hydFile, EarthSystemModelType esm_type, ScenarioType scenarioType)
        {
            string basePath = "/dss/dsshome1/lxc03/ga92wol2/driver_data/ISIMIP-CMIP6/";

            string esm_type_str = esm_type.ToString(CultureInfo.InvariantCulture);
            string scenario_type_str = "";

            if (scenarioType == ScenarioType.RCP_126)
            {
                hydFile.DriverFiles.File_Co2 = "/dss/dsshome1/lxc03/ga92wol2/driver_data/Misc/co2_rcp26.csv";
                scenario_type_str = "126";
            }


            if (scenarioType == ScenarioType.RCP_585)
            {
                hydFile.DriverFiles.File_Co2 = "/dss/dsshome1/lxc03/ga92wol2/driver_data/Misc/co2_1764_2100_extended_rcp85.dat";
                scenario_type_str = "585";
            }

            hydFile.DriverFiles.File_gridlist = "/dss/dsshome1/lxc03/ga92wol2/driver_data/Gridlists/Amazon/Amazon_basin_05.txt";
            hydFile.DriverFiles.File_temp = basePath + esm_type_str  + "/tasAdjust_ssp"+ scenario_type_str + "_1850_2100.nc";
            hydFile.DriverFiles.File_prec = basePath + esm_type_str + "/prAdjust_ssp" + scenario_type_str + "_1850_2100.nc";
            hydFile.DriverFiles.File_insol = basePath + esm_type_str + "/rsdsAdjust_ssp" + scenario_type_str + "_1850_2100.nc";

            hydFile.DriverFiles.Variable_temp = "tasAdjust";
            hydFile.DriverFiles.Variable_prec = "prAdjust";
            hydFile.DriverFiles.Variable_insol = "rsdsAdjust";





            hydFile.DriverFiles.File_Cru = "/dss/dsshome1/lxc03/ga92wol2/driver_data/Misc/Cruncep_1901_2015.bin";
            hydFile.DriverFiles.File_Cru_Misc = "/dss/dsshome1/lxc03/ga92wol2/driver_data/Misc/Cruncep_1901_2015misc.bin";


            DriverFilesHydraulics hyDriverFiles = hydFile.DriverFiles as DriverFilesHydraulics;

            if (hyDriverFiles != null)
            {
                hyDriverFiles.File_vpd = "";
                hyDriverFiles.File_windspeed = basePath + esm_type_str + "/sfcWindAdjust_ssp" + scenario_type_str + "_1850_2100.nc";
                hyDriverFiles.File_hurs = basePath + esm_type_str + "/hursAdjust_ssp" + scenario_type_str + "_1850_2100.nc";


                hyDriverFiles.Variable_windspeed = "sfcWindAdjust";
                hyDriverFiles.Variable_vpd = "vpd";
                hyDriverFiles.Variable_hurs = "hursAdjust";
            }

        }

    }
}
