using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using InctructionFileCreator.Parameters;

namespace InctructionFileCreator
{

    class Entry
    {
        public string Name { get; set; }
    }

    class InsfileExport
    {

        public InsfileExport(string filename)
        {

            filename = @"F:\ClimateData\masterH-Def-GLDAS2020.ins";

            IInsFile insfile = new InsFileHydraulics();

            InsParser parser = new InsParser(filename, insfile);
            parser.Read();

            InsFileHydraulics hydFile = (IInsFile)insfile.Clone() as InsFileHydraulics;


            PropertyInfo[] properties = typeof(PftHyd).GetProperties();

            List<List<string>> values = new List<List<string>>();


            foreach (PropertyInfo info in properties)
            {
                if (Attribute.IsDefined(info, typeof(Export)))
                {
                    List<string> row = new List<string>();

                    row.Add(info.Name);
                    

                    foreach (var pft in hydFile.Pfts)
                    {

                        PftHyd pfthyd = pft as PftHyd;

                        object value = info.GetValue(pfthyd);

                        if (value.GetType() == typeof(double))
                        {
                            double valueD = (double)info.GetValue(pfthyd);
                            row.Add(valueD.ToString(CultureInfo.InvariantCulture));
                        }

                        else
                        {
                            row.Add(value.ToString());
                        }


                    }

                    values.Add(row);
                }
            }

            values = values
                .SelectMany(inner => inner.Select((item, index) => new { item, index }))
                .GroupBy(i => i.index, i => i.item)
                .Select(g => g.ToList())
                .ToList();


            StreamWriter fileWriter = new StreamWriter("InsData.csv",false);

            foreach (List<string> row in values)
            {

                for (int i = 0; i < row.Count - 1; i++)
                {
                    fileWriter.Write(row[i]);
                    fileWriter.Write(',');
                }

                fileWriter.WriteLine(row[row.Count-1]);

            }

            fileWriter.Close();
            


        }
    }
}
