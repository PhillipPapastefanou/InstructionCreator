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
        public string File_throughfall_exclusion { get; set; }

        public override object Clone()
        {
            IDriverFiles parameters = new DriverFilesHydraulics();

            var properties = this.GetType().GetProperties();

            foreach (var p in properties.Where(prop => prop.CanRead && prop.CanWrite))
            {
                object copyValue = p.GetValue(this);
                p.SetValue(parameters, copyValue);
            }

            return parameters;
        }
    }
}
