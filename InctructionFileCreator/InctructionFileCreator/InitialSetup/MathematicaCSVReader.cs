using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InctructionFileCreator.InitialSetup
{
    class Column
    {
        public Column(string headerName, double[] data)
        {
            this.Header = headerName;
            this.Data = data;
        }

        public string Header { get; private set; }
        public double[] Data { get; private set; }
    }



    class MathematicaCSVReader
    {
        public string[] Header { get; set; }

        private double[,] data;

        private List<Column> columns;


        
        public MathematicaCSVReader(string filename, int cutoff = 2)
        {

            columns = new List<Column>();

            using (StreamReader reader = File.OpenText(filename))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(reader.ReadToEnd());

                string[] lines = sb.ToString().Split(new[] { "\n" }, StringSplitOptions.None);

                Header = lines[0].Split(',');




                bool ifMacOs = true;

                int realLines = lines.Length - 1;

                // On Mac we end up having an extra line fo reasons I am not sure yet
                if (ifMacOs)
                {
                    realLines--;

                    for (int i = 0; i < Header.Length; i++)
                    {
                        string s = Header[i];
                        string result = string.Empty;
                        if (cutoff !=0 )
                             result = s.Substring(1, s.Length - cutoff);
                        else
                        {
                            result = s;
                        }


                        Header[i] = result;

                    }

                }

                this.data = new double[realLines, Header.Length];

                for (int i = 0; i < realLines; i++)
                {
                    string[] dataLine = lines[i + 1].Split(',');

                    for (int j = 0; j < dataLine.Length; j++)
                    {
                        data[i, j] = Convert.ToDouble(dataLine[j], CultureInfo.InvariantCulture);
                    }
                }

                for (int i = 0; i < Header.Length; i++)
                {
                    double[] slice = new double[data.GetLength(0)];
                    for (int dI = 0; dI < data.GetLength(0); dI++)
                    {
                        slice[dI] = data[dI, i];
                    }

                    columns.Add(new Column(Header[i], slice));
                }


            }
        }


        public Column GetData(string varName)
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


            return columns[fIndex];
        }

    }
}
