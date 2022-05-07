using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InctructionFileCreator
{

    class Pft : IPft
    {
        public override object Clone()
        {
            IPft pft = new Pft(Name);

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
                Console.WriteLine(prop.Name + " " + prop.GetValue(this) + " " + prop.GetValue(otherT));
            }
        }

        public Pft(string name)
        {
            Name = name;

            //Todo fix that initialisations for grasses and initialize with some rnd number
            CrownArea_Max = 12.34;
            K_allom1 = 250;
            K_allom2 = 36;
            K_allom3 = 0.22;
            K_rp = 1.6;
            K_LaToSa = 1234;
            WoodDens = 200;
            CtoN_sap = 330;
            Kest_Repr = 200;
            Kest_bg = 0.1;
            Kest_pres = 1;
            Est_max = 0.05;
            Alphar = 3;
            Parff_min = 35000;
            Turnover_sap = 0.05;
            Sla = 1.0;
        }

    }
}
