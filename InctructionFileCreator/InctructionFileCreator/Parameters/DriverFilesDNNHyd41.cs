using System;
using System.Linq;

namespace InctructionFileCreator.Parameters
{
    public class DriverFilesDNNHyd41 : DriverFilesHyd41
    {
        public string File_disturb_dnn { get; set; }
        public string File_disturb_dnn_parameters { get; set; }
        public string File_disturb_dnn_species_id { get; set; }
        public string File_dnn_test_data { get; set; }
        public string Variable_pft_identifier { get; set; }

        public override object Clone()
        {
            IDriverFiles parameters = new DriverFilesDNNHyd41();

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
