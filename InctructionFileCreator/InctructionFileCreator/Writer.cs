using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace InctructionFileCreator
{
    class Writer
    {
        private IInsFile insFile;
        private string filename;

        private StreamWriter writer;

        public Writer(IInsFile insFile, string filename)
        {
            this.insFile = insFile;
            this.filename = filename;

            writer = new StreamWriter(filename);

            DateTime dt = DateTime.Now;
            string program = "INSParser v1.0.1";
            string s = string.Format("! LPJ-GUESS instruction file created on {0} with {1}.",
                dt.ToString("d", CultureInfo.InvariantCulture), program);
            writer.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            writer.WriteLine("!");
            writer.WriteLine(s);
            writer.WriteLine("!");
            writer.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");

        }


        public void Write()
        {
            WriteGeneralParameters();
            WriteDriverFiles();
            WritePfts();
        }

        private void WriteGeneralParameters()
        {
            writer.WriteLine();
            writer.WriteLine();

            IGeneralParameters generalParameters = insFile.GeneralParameters;


            PropertyInfo[] parameters = generalParameters.GetType().GetProperties();

            foreach (PropertyInfo parameter in parameters)
            {
                string name = parameter.Name.ToLower(CultureInfo.InvariantCulture);
                string value = ParseParameterToStr(parameter, generalParameters);

                writer.WriteLine(name + " " + value);
            }

        }

        private void WriteDriverFiles()
        {
            writer.WriteLine();
            writer.WriteLine();

            PropertyInfo[] iParams = insFile.DriverFiles.GetType().GetProperties();

            foreach (PropertyInfo iParam in iParams)
            {
                string name = iParam.Name.ToLower(CultureInfo.InvariantCulture);
                string value = ParseParameterToStr(iParam, insFile.DriverFiles);


                string line = string.Format("param \"{0}\"     str({1})", name, value);
                writer.WriteLine(line);
            }
        }

        private void WritePfts()
        {

            PropertyInfo[] pftParams = insFile.Pfts.First().GetType().GetProperties();

            foreach (Pft pft in insFile.Pfts)
            {
                writer.WriteLine();
                writer.WriteLine();

                string pftHeader = string.Format("pft \"{0}\" (", pft.Name);
                writer.WriteLine(pftHeader);

                foreach (PropertyInfo param in pftParams)
                {
                    string name = param.Name.ToLower(CultureInfo.InvariantCulture);
                    string value = ParseParameterToStr(param, pft);


                    //Do not write the name property as the name is already in the title of the pft
                    if (name != "name")
                    {
                        writer.WriteLine("\t" + name + " " + value);
                    }
                }

                writer.WriteLine(")");


            }


        }

        string ParseParameterToStr(PropertyInfo param, object parameters)
        {
            string value = String.Empty;

            if (param.PropertyType == typeof(double))
                value = Convert.ToString(param.GetValue(parameters), CultureInfo.InvariantCulture);

            else if (param.PropertyType == typeof(int))
                value = Convert.ToString(param.GetValue(parameters));

            else if (param.PropertyType == typeof(bool))
            {
                bool bValue = (bool) param.GetValue(parameters);

                value = bValue ? "1" : "0";
            }

            else if (param.PropertyType == typeof(string))
            {
                value = (string)param.GetValue(parameters);

                value = value.Insert(0, "\"");
                value += "\"";

            }

            else if (param.PropertyType.IsEnum)
            {
                value = Convert.ToString(param.GetValue(parameters), CultureInfo.InvariantCulture).ToLower();
                value = value.Insert(0, "\"");
                value += "\"";
            }

            else if (param.PropertyType == typeof(double[]))
            {
                double[] valueDArray = (double[])param.GetValue(parameters);

                foreach (double d in valueDArray)
                {
                    value += d.ToString(CultureInfo.InvariantCulture) + " ";
                }
            }

            else
            {
                Console.WriteLine("Invalid parameter type: " +  param.PropertyType);
            }

            return value;
        }


        public void Dispose()
        {
            writer.Dispose();
        }




    }
}
