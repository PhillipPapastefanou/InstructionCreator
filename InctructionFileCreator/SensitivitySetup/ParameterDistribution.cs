using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensitivitySetup
{
    public abstract class ParameterDistribution
    {
        public string Name => name;

        public ParameterDistribution(string name)
        {
            this.name = name;
        }

        protected string name;
    }


    public class UniformParameter: ParameterDistribution
    {
        public UniformParameter(string name, double min, double max) : base(name)
        {
            this.Min = min;
            this.Max = max;
        }

        public void DivideEqually(int n)
        {
            Values = new double[n];

            for (int i = 0; i < n; i++)
            {
                Values[i] = Min + (Max - Min) / (n - 1) * i;
            }
        }


        public double Min { get; private set; }
        public double Max { get; private set; }
        public double[] Values { get; set; }
    }



}
