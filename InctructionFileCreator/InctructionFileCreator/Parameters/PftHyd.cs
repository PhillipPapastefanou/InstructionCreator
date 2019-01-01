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

        public double Isohydricity { get; set; }
        public double Delta_Psi_Opt { get; set; }
        public double ks_max { get; set; }
        public double kr_max { get; set; }
        public double kL_max { get; set; }
        public double cav_slope { get; set; }
        public double psi50_xylem { get; set; }
    }
}
