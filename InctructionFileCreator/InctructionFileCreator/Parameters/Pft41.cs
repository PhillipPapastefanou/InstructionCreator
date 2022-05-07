using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InctructionFileCreator.Parameters
{
    public class Pft41 : IPft
    {
        [Found]
        public double Root_beta { get; set; }
        [Found]
        public double Wtp_max { get; set; }
        [Found]
        public double Inund_duration { get; set; }
        [Found]
        public double Min_snow { get; set; }
        [Found]
        public double Max_snow { get; set; }
        [Found]
        public double Gdd0_max { get; set; }
        [Found]
        public double Gdd0_min { get; set; }
        [Found]
        public bool Has_aerenchyma { get; set; }


        public override object Clone()
        {
            IPft pft = new Pft41(Name);

            var properties = this.GetType().GetProperties();

            foreach (var p in properties.Where(prop => prop.CanRead && prop.CanWrite))
            {
                object copyValue = p.GetValue(this);
                object[] attrs = p.GetCustomAttributes(true);

                Found found_att = attrs[0] as Found;

                if (found_att != null)
                {
                    Console.WriteLine(found_att.HasFound);

                }


                p.SetValue(pft, copyValue);
            }

            return pft;
        }

        public override void Compare(IPft other)
        {
            Pft41 otherT = other as Pft41;

            var properties = this.GetType().GetProperties();

            foreach (var prop in properties)
            {
                Console.WriteLine(prop.Name + " " + prop.GetValue(this) + " " + prop.GetValue(otherT));
            }
        }

        public Pft41(string name)
        {
            Name = name;

        }
    }


    public class PftList41 : List<Pft41>
    {
        public Pft41 this[string name]
        {
            get
            {
                foreach (Pft41 pft in this)
                {
                    if (pft.Name == name)
                    {
                        return pft;
                    }
                }
                return null;
            }
        }
        public List<string> AllNames
        {
            get
            {
                List<string> names = new List<string>();
                foreach (Pft41 pft in this)
                {
                    names.Add(pft.Name);
                }
                return names;
            }
        }
    }

}


