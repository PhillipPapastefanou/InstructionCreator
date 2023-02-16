using System;
using System.Linq;
namespace InctructionFileCreator.Parameters
{
    class PftHydMort : PftHyd
    {
        public PftHydMort(string name): base(name)
        {
        }

        public double fk { get; set; }
        public double dk { get; set; }


        public override object Clone()
        {
            IPft pft = new PftHydMort(Name);

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
