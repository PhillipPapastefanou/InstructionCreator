using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace InctructionFileCreator.Parameters
{
    public enum HydraulicSystemType
    {
        STANDARD,
        VPD_BASED_GC
    }

    public enum OutputTimeRange
    {
        //The standard time of the climate data simulation
        Scenario,
        //Only the spinup is used for output
        Spinup,
        //Spinup + scenario are out out (basically every time step available is simulated)
        Full,
        //Select the begin and end year yourself
        Custom
    }

    public class GeneralParameters41Hydraulics : GeneralParametersTrunk41
    {
        public HydraulicSystemType Hydraulic_system { get; set; }
        public double Alphaa_nlim { get; set; }
        public bool Disable_mort_greff { get; set; }

        public bool Suppress_daily_output { get; set; }
        public bool Suppress_monthly_output { get; set; }
        public bool Suppress_annually_output { get; set; }
        public OutputTimeRangeType Output_time_range { get; set; }
        public int Year_begin { get; set; }
        public int Year_end { get; set; }
        public OutputDataFormatType Output_data_format { get; set; }


        public override object Clone()
        {
            GeneralParameters41Hydraulics generalParameters = new GeneralParameters41Hydraulics();

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
            GeneralParameters41Hydraulics otherT = other as GeneralParameters41Hydraulics;

            var properties = this.GetType().GetProperties();

            foreach (var prop in properties)
            {
                Console.WriteLine(prop.Name + " " + prop.GetValue(this) + " " + prop.GetValue(otherT));
            }
        }

    }
}
