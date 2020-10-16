using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InctructionFileCreator.InitialSetup
{
    class ParameterCombinationWriter
    {
        private string filename;
        private List<string> variableNames;
        private List<List<double>> variableData;
        public ParameterCombinationWriter(string filename)
        {
            this.filename = filename;

        }

        public void Setup(List<string> variableNames)
        {
            this.variableNames = variableNames;
            this.variableData = new List<List<double>>();

            foreach (string variableName in variableNames)
            {
                this.variableData.Add(new List<double>());
            }


        }

        public void AddValue(string variableName, double value)
        {
            int index = -1;

            for (int i = 0; i < variableNames.Count; i++)
            {
                if (variableName == variableNames[i])
                {
                    index = i;
                }

            }


            variableData[index].Add(value);
        }

        public void Write()
        {
            StreamWriter streamWriter = new StreamWriter(filename);

            string del = "\t";

            streamWriter.Write("ID" + del);
            for (int i = 0; i < variableNames.Count - 1; i++)
            {
                streamWriter.Write(variableNames[i] + del);
            }
            streamWriter.Write(variableNames[variableNames.Count - 1]);
            streamWriter.WriteLine();


            List<List<double>> dataTransform = Transpose(variableData);
            int rowindex = 0;
            foreach (List<double> dataRow in dataTransform)
            {
                streamWriter.Write(rowindex.ToString() + del);
                for (int i = 0; i < dataRow.Count - 1; i++)
                {
                    streamWriter.Write(dataRow[i].ToString(CultureInfo.InvariantCulture) + del, CultureInfo.InvariantCulture);
                }
                streamWriter.Write(dataRow[variableNames.Count - 1].ToString(CultureInfo.InvariantCulture));
                streamWriter.WriteLine();
                rowindex++;
            }

            streamWriter.Close();
        }


        public static List<List<T>> Transpose<T>(List<List<T>> lists)
        {
            var longest = lists.Any() ? lists.Max(l => l.Count) : 0;
            List<List<T>> outer = new List<List<T>>(longest);
            for (int i = 0; i < longest; i++)
                outer.Add(new List<T>(lists.Count));
            for (int j = 0; j < lists.Count; j++)
            for (int i = 0; i < longest; i++)
                outer[i].Add(lists[j].Count > i ? lists[j][i] : default(T));
            return outer;
        }
    }
}
