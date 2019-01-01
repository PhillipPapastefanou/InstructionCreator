using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InctructionFileCreator
{
    class InsReader
    {
        private string filename;
        private List<string> rows;

        public InsReader(string filename)
        {
            this.filename = filename;
        }

        public List<string[]> GetData()
        {

            ReadInsFile();
            
            RemoveRedundandCharts();



            // Split data
            List<string[]> rawSplitData = new List<string[]>();
            foreach (var row in rows)
            {
                if (row.Length > 0)
                {
                    string[] data = row.Split(' ');
                    data = data.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                    rawSplitData.Add(data);
                }
            }


            return rawSplitData;
        }


        private void ReadInsFile()
        {
            rows = new List<string>();
            using (StreamReader sr = File.OpenText(filename))
            {
                string s = String.Empty;
                while ((s = sr.ReadLine()) != null)
                {
                    string[] stringsWOComments = s.Split('!');

                    if (stringsWOComments[0] != String.Empty)
                    {
                        rows.Add(stringsWOComments[0]);
                    }

                    else
                    {

                    }

                }
            }
        }


        private void RemoveRedundandCharts()
        {
            for (int i = 0; i < rows.Count; i++)
            {
                rows[i] = rows[i].Replace("\t", string.Empty);
                rows[i] = rows[i].Replace("\"", string.Empty);
            }
        }
    }
}
