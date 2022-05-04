using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InctructionFileCreator.Parameters
{
    class PftHyd: Pft
    {
        public PftHyd(string name):base(name)
        {
            
        }
        [Export]
        public double Isohydricity { get; set; }
        [Export]
        public double Delta_Psi_Max { get; set; }
        [Export]
        public double ks_max { get; set; }
        [Export]
        public double kr_max { get; set; }
        [Export]
        public double kL_max { get; set; }
        [Export]
        public double cav_slope { get; set; }
        [Export]
        public double psi50_xylem { get; set; }
        [Export]
        public double psi50_leaf { get; set; }
        [Export]
        public double psi50_root { get; set; }
        [Export]
        public double b_leaf_soil_xylem { get; set; }


        public override object Clone()
        {
            IPft pft = new PftHyd(Name);

            var properties = this.GetType().GetProperties();

            foreach (var p in properties.Where(prop => prop.CanRead && prop.CanWrite))
            {
                object copyValue = p.GetValue(this);
                p.SetValue(pft, copyValue);
            }

            return pft;
        }

        public override void Compare(IPft other)
        {
            Pft otherT = other as Pft;

            var properties = this.GetType().GetProperties();

            foreach (var prop in properties)
            {
                object value = prop.GetValue(this);
                object ovalue = prop.GetValue(other);
                if (!value.Equals(ovalue))
                {
                    Console.WriteLine(prop.Name + " " + prop.GetValue(this) + " " + prop.GetValue(otherT));
                }
                
            }
        }
    }
    }
