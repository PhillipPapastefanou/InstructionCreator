using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SensitivitySetup;

namespace InctructionFileCreator
{
    class SobolPoints2D
    {
        private SobolGenerator generator;
        public SobolPoints2D()
        {
            List<UniformParameter> parameterList = new List<UniformParameter>();
            parameterList.Add(new UniformParameter("X", -158.1, 158.1));
            parameterList.Add(new UniformParameter("Y", -158.1, 158.1));
            this.generator = new SobolGenerator(parameterList, 1000);
        }

        public void GenetrateAndWrite()
        {
            generator.Generate();

            ParameterWriter writer = new ParameterWriter(generator, ',');

        }

    }
}
