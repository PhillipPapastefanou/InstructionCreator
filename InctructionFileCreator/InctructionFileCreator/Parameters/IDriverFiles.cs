using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InctructionFileCreator
{
    public abstract class IDriverFiles
    {
        public string File_gridlist { get; set; }
        public string File_Co2 { get; set; }
        public string File_insol { get; set; }
        public string Variable_insol { get; set; }
        public string File_prec { get; set; }
        public string Variable_prec { get; set; }
        public string File_temp { get; set; }
        public string Variable_temp { get; set; }

        public string File_Wetdays { get; set; }
        public string File_Min_Temp { get; set; }
        public string Variable_min_temp { get; set; }
        public string File_Max_Temp { get; set; }
        public string Variable_Max_temp { get; set; }


        public abstract object Clone();

    }
}
