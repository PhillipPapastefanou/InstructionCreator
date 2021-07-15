using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace InctructionFileCreator.V1._7.ClusterSetups
{
    class MonteCarloLinearSample
    {
        private int size;
        private Random random;
        private double xmin;
        private double xmax;
        private double scale;
        public MonteCarloLinearSample(double xmin, double xmax, double step, int seed)
        {
            size =(int) Math.Round((double) (xmax - xmin) / (double)(step)) + 1;
            this.xmin = xmin;
            scale = (xmax - xmin) / (size-1);
            random = new Random(seed);
        }

        public double Next()
        {
            return random.Next(size) * scale + xmin;
        }

        public List<double>  Next(int n)
        {
            List<double> values = new List<double>();
            
            for (int i = 0; i < n; i++)
            {
                values.Add(random.Next(size) * scale + xmin);
            }

            return values;
        }
    }
}
