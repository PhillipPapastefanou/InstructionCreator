using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using InctructionFileCreator.Parser;

namespace InctructionFileCreator
{
    class InsParser
    {

        private InsReader reader;

        private IInsFile insFile;

        private GeneralParameterCreator generalParameterCreator;
        private InputDriverCreator inputDriverCreator;
        private StandParameterCreator standParameterCreator;

        public InsParser(string filename, IInsFile insFile)
        {
            this.filename = filename;
            this.reader = new InsReader(filename);
            this.insFile = insFile;

            generalParameterCreator = new GeneralParameterCreator(insFile.GeneralParameters);
            inputDriverCreator = new InputDriverCreator(insFile.DriverFiles);
            standParameterCreator = new StandParameterCreator(insFile.StandParameters);


        }


        public void Read()
        {
            List<string[]> rawInput = reader.GetData();

            bool foundGroup = false;
            bool foundPft = false;

            bool foundSt = false;


            InsGroup gGroup = null;
            InsGroupCollection groups = new InsGroupCollection();
            InsGroupCollection pftGroups = new InsGroupCollection();

            InsGroup sGroup = null;
            InsGroupCollection sgroups = new InsGroupCollection();
            InsGroupCollection stgroups = new InsGroupCollection();


            foreach (string[] row in rawInput)
            {
                if (foundGroup)
                {
                    if (row.Length > 0)
                    {

                    if (row[0].Contains(')'))
                    {
                        foundGroup = false;
                        groups.Groups.Add(gGroup);
                    }

                    //This should be a sub group
                    else if (row.Length == 1)
                    {
                        gGroup.SubGroups.Add(groups[row[0]]);
                    }


                    else
                    {
                        string paramName = row[0];

                        string paramValue = String.Empty;


                        for (int i = 1; i < row.Length; i++)
                        {
                            paramValue += row[i] + " ";
                        }


   
                        gGroup.Parameters.Add(paramName, paramValue);

                        }


                    }
                }

                else if (foundPft)
                {
                    if (row.Length > 0)
                    {

                        if (row[0].Contains(')'))
                        {
                            foundPft = false;
                            pftGroups.Groups.Add(gGroup);
                        }

                        //This should be a sub group
                        else if (row.Length == 1)
                        {
                            gGroup.SubGroups.Add(groups[row[0]]);
                        }


                        else
                        {
                            string paramName = row[0];

                            string paramValue = String.Empty;
                            

                            for (int i = 1; i < row.Length; i++)
                            {
                                paramValue += row[i] + " ";
                            }

                            gGroup.Parameters.Add(paramName, paramValue);
                        }


                    }

                }

                else if (foundSt)
                {
                    if (row.Length > 0)
                    {

                        if (row[0].Contains(')'))
                        {
                            foundSt = false;
                            stgroups.Groups.Add(sGroup);
                        }

                        //This should be a sub group
                        else if (row.Length == 1)
                        {
                            sGroup.SubGroups.Add(groups[row[0]]);
                        }


                        else
                        {
                            string paramName = row[0];

                            string paramValue = String.Empty;


                            for (int i = 1; i < row.Length; i++)
                            {
                                paramValue += row[i] + " ";
                            }

                            sGroup.Parameters.Add(paramName, paramValue);
                        }


                    }

                }

                else if(row.Length > 1)
                {
                    // Check for group or Pft first:
                    if (row[0] == "group" )
                    {
                        foundGroup = true;
                        gGroup = new InsGroup();
                        gGroup.Name = row[1];
                    }

                    else if(row[0] == "pft")
                    {
                        foundPft = true;
                        gGroup = new InsGroup();
                        gGroup.Name = row[1];
                    }

                    else if (row[0] == "st")
                    {
                        foundSt= true;
                        sGroup = new InsGroup();
                        sGroup.Name = row[1];
                    }

                    else if (row[0] == "param")
                    {
                        inputDriverCreator.ParseLine(row);
                    }

                    else
                    {
                        generalParameterCreator.ParseLine(row);
                    }


                }

               
            }



            foreach (InsGroup pftGroup in pftGroups.Groups)
            {
                pftGroup.AccumulateParameters();
            }

            foreach (InsGroup pftGroup in stgroups.Groups)
            {
                pftGroup.AccumulateParameters();
            }


            foreach (var stGroup in stgroups.Groups)
            {
                standParameterCreator.Parse(stGroup);
            }

            foreach (InsGroup pftGroup in pftGroups.Groups)
            {
                PftInsCreator pftInsCreator = new PftInsCreator(pftGroup, insFile.PftType);
                insFile.Pfts.Add(pftInsCreator.Pft);
            }
            

        }


        private string filename;
    }
}
