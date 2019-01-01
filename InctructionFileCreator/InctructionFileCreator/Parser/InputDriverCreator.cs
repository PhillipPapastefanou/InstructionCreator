using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace InctructionFileCreator
{
    class InputDriverCreator
    {
        private IDriverFiles driverInput;
        private Dictionary<string, int> inputFileParameterStrings;
        private PropertyInfo[] inputFileProperties;


        public InputDriverCreator(IDriverFiles driverInput)
        {
            this.driverInput = driverInput;

            inputFileParameterStrings = new Dictionary<string, int>();

            inputFileProperties = driverInput.GetType().GetProperties();


            int i = 0;
            foreach (PropertyInfo info in inputFileProperties)
            {
                inputFileParameterStrings.Add(info.Name.ToLower(CultureInfo.InvariantCulture), i);
                ++i;
            }
        }


        public void ParseLine(string[] row)
        {
            if (row.Length == 4)
            {
                string path = row[3];
                path = path.Replace(")", String.Empty);
                string parameterName = row[1];
                int id = -1;
                bool found = inputFileParameterStrings.TryGetValue(parameterName, out id);

                if (found)
                {
                    PropertyInfo info = inputFileProperties[id];

                    info.SetValue(driverInput, path);
                }

                else
                {
                    Console.WriteLine("Could not parse file-parameter: " + parameterName);
                }

            }

            else
            {
                Console.Write("Invalid file parameter: ");
                foreach (string s in row)
                {
                    Console.Write(s + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
