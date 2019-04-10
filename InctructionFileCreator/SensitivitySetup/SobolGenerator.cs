using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SensitivitySetup
{
    public class SobolGenerator
    {

        public List<UniformParameter> Parameters => parameters;
        public int NPoints => nPoints;


        public SobolGenerator(List<UniformParameter>parameters, int nPoints)
        {
            this.parameters = parameters;
            this.nPoints = nPoints;
            this.nDims = parameters.Count;

            this.sequence_generator = new SobolSequence("..//..//new-joe-kuo-5.21201");
        }

        public void Generate()
        {
            sequence_generator.GetSequence(nPoints, nDims);

            double[,] unscaled_values = sequence_generator.Points;



            for (int d = 0; d < nDims; d++)
            {
                double min = parameters[d].Min;
                double max = parameters[d].Max;

                double[] rescaledValues = new double[nPoints];

                for (int k = 0; k < nPoints; k++)
                {
                    rescaledValues[k] = Rescale(unscaled_values[k, d], min, max);
                }

                parameters[d].Values = rescaledValues;
            }


        }


        private double[] Rescale(double[] xList, double to_min, double to_max)
        {
            double[] xListNew = new double[xList.Length];
            for (int i = 0; i < xList.Length; i++)
            {
                xListNew[i] = Rescale(xList[i], to_min, to_max);
            }
            return xListNew;
        }

        private List<double> Rescale(List<double> xList, double to_min, double to_max)
        {
            List<double> xListNew = new List<double>(xList.Count);
            for (int i = 0; i < xList.Count; i++)
            {
                xListNew[i] = Rescale(xList[i], to_min, to_max);
            }
            return xListNew;
        }

        private double Rescale(double x, double to_min, double to_max)
        {
            return to_min + (to_max - to_min) * x;
        }



        private List<UniformParameter> parameters;
        private SobolSequence sequence_generator;
        private int nPoints;
        private int nDims;

    }
}
