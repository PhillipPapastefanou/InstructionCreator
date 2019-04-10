﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensitivitySetup
{
    public class ParameterWriter
    {
        private int n;

        private StreamWriter writer;


        public ParameterWriter(SobolGenerator sobolGenerator)
        {
            List<UniformParameter> parameters = sobolGenerator.Parameters;
            n = sobolGenerator.NPoints;
            
            writer = new StreamWriter("Values.tsv");
            List<ParameterDistribution> iparams = new List<ParameterDistribution>();

            for (int i = 0; i < parameters.Count; i++)
                iparams.Add(parameters[i] as ParameterDistribution);
            
            
            WriteHeader(iparams);

            WriteRows(parameters);
        }



        private void WriteHeader(List<ParameterDistribution> parameters)
        {
            for (int i = 0; i < parameters.Count; i++)
            {
                ParameterDistribution param = parameters[i];
                writer.Write(param.Name);

                if (i != parameters.Count - 1)
                    writer.Write('\t');
            }
            writer.WriteLine();
        }


        private void WriteRows(List<UniformParameter> parameters)
        {
            for (int i = 0; i < n; i++)
            {
                for (int d = 0; d < parameters.Count; d++)
                {
                    UniformParameter param = parameters[d];

                    writer.Write(param.Values[i].ToString(CultureInfo.InvariantCulture));

                    if (d != parameters.Count - 1)
                        writer.Write('\t');
                }
                writer.WriteLine();
            }

            writer.Close();

        }




        }

    }

