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
        string File_insol { get; set; }
        string Variable_insol { get; set; }
        string File_prec { get; set; }
        string Variable_prec { get; set; }
        string File_temp { get; set; }
        string Variable_temp { get; set; }

        string File_Wetdays { get; set; }
        string File_Min_Temp { get; set; }
        string Variable_min_temp { get; set; }
        string File_Max_Temp { get; set; }
        string Variable_Max_temp { get; set; }

        string File_Mip_Noy { get; set; }
        string File_Mip_nhx { get; set; }
        string File_Soildata { get; set; }

        string File_Specifichum { get; set; }
        //string Variable_Specifichum { get; set; }
        string File_Pres { get; set; }
        //string Variable_Pres{ get; set; }
        string File_Relhum { get; set; }
        //string Variable_Relhum { get; set; }
        string File_Wind { get; set; }
       // string Variable_Wind { get; set; }


        object Clone();
    }
}
