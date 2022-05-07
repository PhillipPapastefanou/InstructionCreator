using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using InctructionFileCreator.Parameters;

namespace InctructionFileCreator
{
    public class PftInsCreator
    {
        public IPft Pft { get; private set; }


        private InsGroup pftInsGroup;
        private Dictionary<string, int> pftParsedString;
        private PropertyInfo[] pftProperties;

        public PftInsCreator(InsGroup pftInsGroup, PftType pftType)
        {
            this.pftInsGroup = pftInsGroup;


            switch (pftType)
            {
                case PftType.Trunk:
                    //Create Pft object
                    Pft = new Pft(pftInsGroup.Name);
                    break;
                case PftType.Hydraulics:
                    Pft = new PftHyd(pftInsGroup.Name);
                    break;
                case PftType.Trunk41:
                    Pft = new Pft41(pftInsGroup.Name);
                    break;
                case PftType.Hydraulics41:
                    Pft = new PftHyd41(pftInsGroup.Name);
                    break;
                case PftType.Hydraulics41mp:
                    Pft = new PftHyd41mp(pftInsGroup.Name);
                    break;
            }


            pftProperties = Pft.GetType().GetProperties();
            pftParsedString = new Dictionary<string, int>();

            int i = 0;
            foreach (PropertyInfo info in pftProperties)
            {
                pftParsedString.Add(info.Name.ToLower(), i);
                i++;
            }


            foreach (KeyValuePair<string, string> parameter in this.pftInsGroup.Parameters)
            {
                string name = parameter.Key;
                string value = parameter.Value;

                if (pftParsedString.ContainsKey(name))
                {
                    int id = pftParsedString[name];
                    
                    ParseParameter(id, value);


                }

                else
                {
                    Console.WriteLine("Pft does not contain: " + name);
                }
            }

        }


        private void ParseParameter(int id, string value)
        {

            PropertyInfo parameter = pftProperties[id];

            Type type = parameter.PropertyType;

            if (type == typeof(bool))
            {
                ParseBoolean(parameter, value);
            }

            else if (type == typeof(double))
            {
                ParseDouble(parameter, value);
            }

            else if (type == typeof(int))
            {
                ParseInt(parameter, value);
            }

            else if (type == typeof(double[]))
            {
                ParseDoubleArray(parameter, value);
            }

            else if (type.IsEnum)
            {

                Type enumType = parameter.PropertyType;

                if (enumType == typeof(LandcoverType))
                {
                    ParseEnum<LandcoverType>(parameter, value);
                }
                else if (enumType == typeof(LifeFormType))
                {
                    ParseEnum<LifeFormType>(parameter, value);
                }
                else if (enumType == typeof(PathwayType))
                {
                    ParseEnum<PathwayType>(parameter, value);
                }
                else if (enumType == typeof(LeafPhysiognomyType))
                {
                    ParseEnum<LeafPhysiognomyType>(parameter, value);
                }
                else if (enumType == typeof(PhenologyType))
                {
                    ParseEnum<PhenologyType>(parameter, value);
                }
                else
                {
                    Console.WriteLine("Invalid enum parameter supplied: " + parameter.Name + " value: " + value);
                }
            }

            else
            {
                Console.WriteLine("Failed to parse parameter " + parameter.Name + " value: " + value);
                throw new Exception();
            }

            //object[] attrs = parameter.GetCustomAttributes(true);
            //foreach (object attr in attrs)
            //{
            //    Found found_att = attr as Found;
            //    if (found_att != null)
            //        found_att.HasFound = true;                

            //}
        }

        private void ParseBoolean(PropertyInfo parameter, string value)
        {
            int intValue;

            if (int.TryParse(value, out intValue))
            {
                bool boolValue = Convert.ToBoolean(intValue);

                parameter.SetValue(Pft, boolValue);
            }
            else
            {
                Console.WriteLine("Failed to parse boolean parameter " + parameter.Name + " value: " + value);
            }
        }
        private void ParseDouble(PropertyInfo parameter, string value)
        {
            double dvalue;

            if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out dvalue))
            {
                parameter.SetValue(Pft, dvalue);
            }
            else
            {
                Console.WriteLine("Failed to parse double parameter " + parameter.Name + " value: " + value);
            }
        }
        private void ParseInt(PropertyInfo parameter, string value)
        {
            int ivalue;

            if (int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out ivalue))
            {
                parameter.SetValue(Pft, ivalue);
            }
            else
            {
                Console.WriteLine("Failed to parse int parameter " + parameter.Name + " value: " + value);
            }
        }

        private void ParseEnum<TEnum>(PropertyInfo parameter, string value) where TEnum:struct
        {
            TEnum enumType;

            string valueConv = value.FirstUpperRestLower();

            if (Enum.TryParse(valueConv, out enumType))
            {
                parameter.SetValue(Pft, enumType);
            }
            else
            {
                Console.WriteLine("Failed to parse enum parameter " + parameter.Name + " value: " + value);
            }
        }

        private void ParseDoubleArray(PropertyInfo parameter, string value)
        {


            string[] valuesStr = value.Split(' ');

            List<double> valsDoubles = new List<double>();

            foreach (string valueStr in valuesStr)
            {
                double dvalue;

                //This happens for the last whitespace applied to to concatenate function
                //Ignore the whitespaces for this specific case
                if (valueStr!= String.Empty)
                {
                    if (double.TryParse(valueStr, NumberStyles.Any, CultureInfo.InvariantCulture, out dvalue))
                    {
                        valsDoubles.Add(dvalue);
                    }
                    else
                    {
                        Console.WriteLine("Failed to parse double parameter " + parameter.Name + " value: " + valueStr);
                    }
                }
                

            }

            parameter.SetValue(Pft, valsDoubles.ToArray());

        }



    }



}
