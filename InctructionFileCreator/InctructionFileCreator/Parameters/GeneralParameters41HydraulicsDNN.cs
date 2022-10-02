using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace InctructionFileCreator.Parameters
{
    public enum DNN_relative_growth_type
    {
        DBH_based,
        AGB_based
    }

    public enum Disturbance_Model_Type
    {
        Standard,
        DNN,
        Functional
    }

    public class GeneralParameters41HydraulicsDNN: GeneralParameters41Hydraulics
    {

        public Disturbance_Model_Type Disturbance_model { get; set; }


        public DNN_relative_growth_type DNN_Relative_Growth { get; set; }

        public override object Clone()
        {
            GeneralParameters41HydraulicsDNN generalParameters = new GeneralParameters41HydraulicsDNN();

            var properties = this.GetType().GetProperties();

            foreach (var p in properties.Where(prop => prop.CanRead && prop.CanWrite))
            {
                object copyValue = p.GetValue(this);
                p.SetValue(generalParameters, copyValue);
            }

            return generalParameters;
        }

        public override void Compare(IGeneralParameters other)
        {
            base.Compare(other);
        }
    }
}
