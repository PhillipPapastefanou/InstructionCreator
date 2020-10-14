using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InctructionFileCreator.InitialSetup
{
    class ParameterCombinationWriter
    {
        private string filename;
        private List<string> variableName;
        private List<List<double>> variableData;
        public ParameterCombinationWriter(string filename)
        {
            this.filename = filename;

        }

        public void Setup(List<string> variableName)
        {
            this.variableName = variableName;
            this.variableData = new List<List<double>>();


        }

        public void AddValue(string variableName, double value)
        {

        }
    }
}
