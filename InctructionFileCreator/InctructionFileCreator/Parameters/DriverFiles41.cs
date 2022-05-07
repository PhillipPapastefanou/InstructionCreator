using System;
using System.Linq;
namespace InctructionFileCreator.Parameters
{
    public class DriverFiles41: IDriverFiles
    {
        public string File_Mip_Noy { get; set; }
        public string File_Soildata { get; set; }
        public string File_Specifichum { get; set; }
        //public string Variable_Specifichum { get; set; }
        public string File_Pres { get; set; }
        //public string Variable_Pres { get; set; }
        public string File_Relhum { get; set; }
        //public string Variable_Relhum { get; set; }
        public string File_Wind { get; set; }
        //public string Variable_Wind { get; set; }
        public string File_Mip_nhx { get; set; }


        public override object Clone()
        {
            IDriverFiles parameters = new DriverFiles41();

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
