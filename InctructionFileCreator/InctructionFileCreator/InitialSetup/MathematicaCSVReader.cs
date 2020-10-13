using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InctructionFileCreator.InitialSetup
{
    class MathematicaCSVReader
    {
        public string[] Header { get; set; }

        private double[,] data;


        
        public MathematicaCSVReader(string filename)
        {

            using (StreamReader reader = File.OpenText(filename))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(reader.ReadToEnd());

                string[] lines = sb.ToString().Split(new[] {"\r\n"}, StringSplitOptions.None);

                Header = lines[0].Split(',');

                this.data = new double[lines.Length - 1, Header.Length];

                for (int i = 0; i < lines.Length-1; i++)
                {
                    string[] dataLine = lines[i + 1].Split(',');
                    
                    for (int j = 0; j < dataLine.Length; j++)
                    {
                        data[i, j] = Convert.ToDouble(dataLine[j], CultureInfo.InvariantCulture);
                    }
                }
            }
        }


        public double[] GetData(string varName)
        {
            int fIndex = -1;

            for (int index = 0; index < Header.Length; index++)
            {
                if (Header[index] == varName)
                {
                    fIndex = index;
                }
            }

            if (fIndex == -1)
            {
                Console.WriteLine("Variable not found");
                
            }

            double[] slice = new double[data.GetLength(0)];

            for (int dI = 0; dI < data.GetLength(0); dI++)
            {
                slice[dI] = data[dI, fIndex];
            }
            
            return slice;
        }

    }
}
