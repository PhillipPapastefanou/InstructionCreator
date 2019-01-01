using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InctructionFileCreator
{
    class DriverFilesTrunk : IDriverFiles
    {
        public string File_gridlist { get; set; }
        public string File_Co2 { get; set; }
        public string File_Cru { get; set; }
        public string File_Cru_Misc { get; set; }
        public string File_insol { get; set; }
        public string Variable_insol { get; set; }
        public string File_prec { get; set; }
        public string Variable_prec { get; set; }
        public string File_temp { get; set; }
        public string Variable_temp { get; set; }

        public string File_Wetdays { get; set; }
        public string File_Min_Temp { get; set; }
        public string File_Max_Temp { get; set; }
        public string File_NDep { get; set; }
        public virtual object Clone()
        {
            IDriverFiles parameters = new DriverFilesTrunk();

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
