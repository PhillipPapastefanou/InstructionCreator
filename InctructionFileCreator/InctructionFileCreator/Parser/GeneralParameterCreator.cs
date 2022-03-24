using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace InctructionFileCreator
{
    public class GeneralParameterCreator
    {

        private IGeneralParameters generalParameters;
        private Dictionary<string, int> generalParametersSmallStrings;
        private PropertyInfo[] generalParametersProperties;

        public GeneralParameterCreator(IGeneralParameters generalParameters)
        {
            this.generalParameters = generalParameters;
            generalParametersSmallStrings = new Dictionary<string, int>();
            generalParametersProperties = generalParameters.GetType().GetProperties();

            int i = 0;
            foreach (PropertyInfo info in generalParametersProperties)
            {
                generalParametersSmallStrings.Add(info.Name.ToLower(CultureInfo.InvariantCulture), i);
                ++i;
            }
        }

        public void ParseLine(string[] row)
        {
            string parameter_name = row[0];
            string value_str = String.Empty;
            
            if (row.Length == 2)
            {
                value_str = row[1];
            }

            //we have a parameter that needs to initalized as an array or a list of strings
            else
            {
                for (int i = 1; i < row.Length; i++)
                    value_str += row[i] + " ";
                value_str.Remove(value_str.Length - 1);
            }
           

            int id = -1;

            bool found = generalParametersSmallStrings.TryGetValue(parameter_name, out id);

            if (found)
            {
                PropertyInfo info = generalParametersProperties[id];
                Type varType = info.PropertyType;
                bool isEnum = info.PropertyType.IsEnum;


                if (varType == typeof(string))
                {
                    info.SetValue(generalParameters, value_str);
                }

                else if (isEnum)
                {

                    if (info.PropertyType == typeof(VegetationMode))
                    {
                        VegetationMode mode;

                        string enumString = value_str.Substring(0, 1).ToUpper() + value_str.Substring(1).ToLower();

                        bool parsed = Enum.TryParse(enumString, out mode);

                        if (parsed)
                        {
                            info.SetValue(generalParameters, mode);
                        }
                    }

                    else if (info.PropertyType == typeof(WaterUptake))
                    {
                        WaterUptake mode;

                        string enumString = value_str.Substring(0, 1).ToUpper() + value_str.Substring(1).ToLower();

                        bool parsed = Enum.TryParse(enumString, out mode);

                        if (parsed)
                        {
                            info.SetValue(generalParameters, mode);
                        }
                    }

                    else if (info.PropertyType == typeof(HydraulicSystemType))
                    {
                        HydraulicSystemType mode;

                        string enumString = value_str.Substring(0, 1).ToUpper() + value_str.Substring(1).ToLower();

                        bool parsed = Enum.TryParse(enumString, out mode);

                        if (parsed)
                        {
                            info.SetValue(generalParameters, mode);
                        }
                    }

                    else if (info.PropertyType == typeof(OutputTimeRangeType))
                    {
                        OutputTimeRangeType mode;

                        string enumString = value_str.Substring(0, 1).ToUpper() + value_str.Substring(1).ToLower();

                        bool parsed = Enum.TryParse(enumString, out mode);

                        if (parsed)
                        {
                            info.SetValue(generalParameters, mode);
                        }
                    }


                    else if (info.PropertyType == typeof(RootDistribution))
                    {
                        RootDistribution mode;

                        string enumString = value_str.Substring(0, 1).ToUpper() + value_str.Substring(1).ToLower();

                        bool parsed = Enum.TryParse(enumString, out mode);

                        if (parsed)
                        {
                            info.SetValue(generalParameters, mode);
                        }
                    }

                    else if (info.PropertyType == typeof(WeatherGeneratorType))
                    {
                        WeatherGeneratorType mode;

                        string enumString = value_str.Substring(0, 1).ToUpper() + value_str.Substring(1).ToLower();

                        bool parsed = Enum.TryParse(enumString, out mode);

                        if (parsed)
                        {
                            info.SetValue(generalParameters, mode);
                        }
                    }


                    else if (info.PropertyType == typeof(FireModelType))
                    {
                        FireModelType mode;

                        string enumString = value_str.Substring(0, 1).ToUpper() + value_str.Substring(1).ToLower();

                        bool parsed = Enum.TryParse(enumString, out mode);

                        if (parsed)
                        {
                            info.SetValue(generalParameters, mode);
                        }
                    }


                    else
                    {
                        Console.WriteLine("Invalid Enumtype supplied: " + info.PropertyType);
                    }

                }

                else if (varType == typeof(double))
                {
                    double valueDouble = Convert.ToDouble(value_str, CultureInfo.InvariantCulture);
                    info.SetValue(generalParameters, valueDouble);
                }

                else if (varType == typeof(bool))
                {
                    int value_int = Convert.ToInt32(value_str);
                    bool valueBoolean = Convert.ToBoolean(value_int);
                    info.SetValue(generalParameters, valueBoolean);
                }

                else if (varType == typeof(int))
                {
                    int value_int = Convert.ToInt32(value_str);
                    info.SetValue(generalParameters, value_int);
                }

                else
                {
                    Console.WriteLine("Invalid Variable type supplied: " + varType);
                }
            }

        }

    }

}
