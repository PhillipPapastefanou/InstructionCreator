using System;
using System.Linq;

namespace InctructionFileCreator.Parameters
{
    class PftHyd41mp : PftHyd41
    {
        //public double B_leaf_soil_xylem { get; set; }
        public double Psi50_leaf { get; set; }
        public double Psi50_root { get; set; }

        public PftHyd41mp(string name) : base(name)
        {
        }


        public override object Clone()
        {
            IPft pft = new PftHyd41mp(Name);

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
            PftHyd41mp otherT = other as PftHyd41mp;

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
