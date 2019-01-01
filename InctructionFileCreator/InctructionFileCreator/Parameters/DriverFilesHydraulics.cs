using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InctructionFileCreator.Parameters
{
    class DriverFilesHydraulics : DriverFilesTrunk
    {

        public string File_vpd { get; set; }
        public string Variable_vpd { get; set; }
        public string File_windspeed { get; set; }
        public string Variable_windspeed { get; set; }
    }
}
