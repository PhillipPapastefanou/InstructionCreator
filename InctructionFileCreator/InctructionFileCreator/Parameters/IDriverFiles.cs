using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InctructionFileCreator
{
    public interface IDriverFiles
    {
        string File_gridlist { get; set; }
        string File_Co2 { get; set; }
        string File_Cru { get; set; }
        string File_Cru_Misc { get; set; }
        string File_insol { get; set; }
        string Variable_insol { get; set; }
        string File_prec { get; set; }
        string Variable_prec { get; set; }
        string File_temp { get; set; }
        string Variable_temp { get; set; }

        string File_Wetdays { get; set; }
        string File_Min_Temp { get; set; }
        string File_Max_Temp { get; set; }
        string File_NDep { get; set; }


        object Clone();
    }
}
