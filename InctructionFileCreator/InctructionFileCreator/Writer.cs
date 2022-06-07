using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using InctructionFileCreator.Parameters;

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
            WriteSt();
            WritePfts();
           
        }

        private void WriteGeneralParameters()
        {
            writer.WriteLine();
            writer.WriteLine();

            IGeneralParameters generalParameters = insFile.GeneralParameters;
            

            PropertyInfo[] parameters = generalParameters.GetType().GetProperties();

            Array.Sort(parameters, (info1, info2) => info1.Name.CompareTo(info2.Name));

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


            Array.Sort(iParams, (info1, info2) => info1.Name.CompareTo(info2.Name));

            foreach (PropertyInfo iParam in iParams)
            {
                string name = iParam.Name.ToLower(CultureInfo.InvariantCulture);
                string value = ParseParameterToStr(iParam, insFile.DriverFiles);


                string line = string.Format("param \"{0}\"     (str {1})", name, value);
                writer.WriteLine(line);
            }
        }

        private void WritePfts()
        {

            PropertyInfo[] pftParams = insFile.Pfts.First().GetType().GetProperties();

            Array.Sort(pftParams, (info1, info2) => info1.Name.CompareTo(info2.Name));

            foreach (Pft41 pft in insFile.Pfts)
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

                        //object[] attrs = param.GetCustomAttributes(true);
                        //foreach (object attr in attrs)
                        //{
                        //    Found found_att = attr as Found;
                        //    if (found_att != null || found_att.HasFound == false)
                        //    {
                        //        Console.WriteLine("Parameter " + name + " has not been set in the insfiles!");
                        //    }   


                        //}

                        if (name == "cton_leaf_min" && value == "0")
                        {
                            Console.WriteLine("Not writing out cton_leaf_min!");
                        }

                        else if (name == "sla" && value == "0")
                        {
                            Console.WriteLine("Not writing out sla!");
                        }

                        else if (name == "alphar" && value == "0")
                        {
                            Console.WriteLine("Not writing out alphar!");
                        }

                        else if (name == "crownarea_max" && value == "0")
                        {
                            Console.WriteLine("Not writing out crownarea_max!");
                        }

                        else if (name == "cton_sap" && value == "0")
                        {
                            Console.WriteLine("Not writing out cton_sap!");
                        }


                        else if (name == "est_max" && value == "0")
                        {
                            Console.WriteLine("Not writing out est_max!");
                        }
                        else if (name == "k_allom1" && value == "0")
                        {
                            Console.WriteLine("Not writing out k_allom1!");
                        }
                        else if (name == "k_allom2" && value == "0")
                        {
                            Console.WriteLine("Not writing out k_allom2!");
                        }

                        else if (name == "k_allom3" && value == "0")
                        {
                            Console.WriteLine("Not writing out k_allom3!");
                        }
                        else if (name == "k_chilla" && value == "0")
                        {
                            Console.WriteLine("Not writing out k_chilla!");
                        }
                        else if (name == "k_chillb" && value == "0")
                        {
                            Console.WriteLine("Not writing out k_chillb!");
                        }
                        else if (name == "k_chillk" && value == "0")
                        {
                            Console.WriteLine("Not writing out k_chillk!");
                        }

                        else if (name == "k_latosa" && value == "0")
                        {
                            Console.WriteLine("Not writing out k_latosa!");
                        }
                        else if (name == "k_rp" && value == "0")
                        {
                            Console.WriteLine("Not writing out k_rp!");
                        }
                        else if (name == "kest_bg" && value == "0")
                        {
                            Console.WriteLine("Not writing out kest_bg!");
                        }
                        else if (name == "kest_pres" && value == "0")
                        {
                            Console.WriteLine("Not writing out kest_pres!");
                        }
                        else if (name == "kest_repr" && value == "0")
                        {
                            Console.WriteLine("Not writing out kest_repr!");
                        }
                        else if (name == "kest_bg" && value == "0")
                        {
                            Console.WriteLine("Not writing out kest_bg!");
                        }
                        else if (name == "wooddens" && value == "0")
                        {
                            Console.WriteLine("Not writing out wooddens!");
                        }
                        else
                        {
                            writer.WriteLine("\t" + name + " " + value);
                        }


                    }
                }

                writer.WriteLine(")");


            }


        }

        private void WriteSt()
        {

            PropertyInfo[] stParams = insFile.StandParameters.GetType().GetProperties();

            Array.Sort(stParams, (info1, info2) => info1.Name.CompareTo(info2.Name));

                writer.WriteLine();
                writer.WriteLine();

                string pftHeader = string.Format("st \"{0}\" (", insFile.StandParameters.Name);
                writer.WriteLine(pftHeader);

                foreach (PropertyInfo param in stParams)
                {
                    string name = param.Name.ToLower(CultureInfo.InvariantCulture);
                    string value = ParseParameterToStr(param, insFile.StandParameters);


                    //Do not write the name property as the name is already in the title of the pft
                    if (name != "name")
                    {

                        //object[] attrs = param.GetCustomAttributes(true);
                        //foreach (object attr in attrs)
                        //{
                        //    Found found_att = attr as Found;
                        //    if (found_att != null || found_att.HasFound == false)
                        //    {
                        //        Console.WriteLine("Parameter " + name + " has not been set in the insfiles!");
                        //    }   


                        //}

                        writer.WriteLine("\t" + name + " " + value);
                    }
                }

                writer.WriteLine(")");


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

                if (value != null)
                {

                    value = value.Insert(0, "\"");
                    value += "\"";
                }

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
