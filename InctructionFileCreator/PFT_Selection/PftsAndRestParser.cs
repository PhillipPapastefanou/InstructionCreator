using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InctructionFileCreator;
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
using ComLib;
using ComLib.CsvParse;
using ComLib.Lang.Plugins;
using File = System.IO.File;

namespace PFT_Selection
{
    class PftsAndRestParser
    {
        private List<string[]> rest;
        private List<string> pft_names;
        private InsReader reader;
        private List<List<string[]>> pftList;


        public PftsAndRestParser(string filename)
        {
            this.filename = filename;
            this.reader = new InsReader(filename);
            this.rest = new List<string[]>();
            this.pftList = new List<List<string[]>>();
            this.pft_names = new List<string>();
            

        }


        public void Read()
        {
            List<string[]> rawInput = reader.GetData();

            bool foundGroup = false;
            bool foundPft = false;

            List<string[]> pft = new List<string[]>();

            foreach (string[] row in rawInput)
            {
                if (foundPft)
                {
                    if (row.Length > 0)
                    {
                        pft.Add(row);

                        if (row[0].Contains(')'))
                        {
                            foundPft = false;
                            pftList.Add(pft);
                        }
                    }
                }

                else if (row.Length > 0)
                {

                        // Check for group or Pft first:
                        if (row[0] == "pft")
                        {
                            foundPft = true;
                            pft_names.Add(row[1]);
                            pft = new List<string[]>();
                            pft.Add(row);
                        }

                        else
                        {
                            rest.Add(row);
                        }

                }


            }



            //foreach (InsGroup pftGroup in pftGroups.Groups)
            //{
            //    pftGroup.AccumulateParameters();
            //}


            //foreach (InsGroup pftGroup in pftGroups.Groups)
            //{
            //    PftInsCreator pftInsCreator = new PftInsCreator(pftGroup, insFile.PftType);
            //    insFile.Pfts.Add(pftInsCreator.Pft);
            //}


        }

        public void WriteIndividualPftFiles()
        {

            StreamWriter fileWriter = new StreamWriter("Insfiles.txt");

            string rootFolderBase = "EuropeInses";
            string rootFolderIns = "Insfiles";

            if (!Directory.Exists(rootFolderBase))
            {
                Directory.CreateDirectory(rootFolderBase);
            }
            
            if (!Directory.Exists(rootFolderIns))
            {
                Directory.CreateDirectory(rootFolderIns);
            }
            List<string> specialThings = new List<string>();

            specialThings.Add("(");
            specialThings.Add(")");
            specialThings.Add("pft");
            specialThings.Add("group");


            for (int i = 0; i < pft_names.Count; i++)
            {
                string name = pft_names[i];
                StreamWriter writer = new StreamWriter("EuropeInses/europe_" + name + ".ins");
                fileWriter.Write("Insfiles/" + name + ".ins" +  "\n");

                string s = "import \"/home/phillip/src/guess4_hydraulics_dnn/data/ins/europe_" + name + ".ins\"";

                lineChanger(s, "F:\\Dropbox\\UNI\\Projekte\\A07_DNN_LPJ-GUESS\\3_Insfiles\\europe_dnn_simba.ins", "Insfiles/" + name + ".ins", 9);
                
                foreach (string[] row in rest)
                {
                    if (!(row[0] == "title"))
                    {
                        for (int j = 0; j < row.Length; j++)
                        {
                            if (j == 0 || j == 2)
                            {
                                writer.Write(row[j] + " ");
                            }
                            else
                            {
                                double x;
                                bool isNumeric = double.TryParse(row[j], out x);

                                if (isNumeric)
                                {
                                    if (j != row.Length - 1 )
                                    {

                                        writer.Write(row[j] + " ");
                                    }
                                    else
                                    {

                                        writer.Write(row[j]);
                                    }
                                }

                                else
                                {
                                    writer.Write("\"" + row[j] + "\"");
                                }
                            }
                        }

                        writer.WriteLine();
                    }


                    else
                    {
                        writer.WriteLine("title 'LPJ'");
                    }
                }

                writer.WriteLine();
                writer.WriteLine();

                List<string[]> pft = pftList[i];
                foreach (string[] row in pft)
                {
                    for (int j = 0; j < row.Length; j++)
                    {

                        if (j == 0 || j == 2)
                        {
                            writer.Write(row[j] + " ");
                        }
                        else
                        {
                            double x;
                            bool isNumeric = double.TryParse(row[j], out x);

                            if (isNumeric)
                            {
                                writer.Write(row[j] + " ");
                            }

                            else
                            {
                                writer.Write("\"" + row[j] + "\"" + " ");
                            }
                        }
                    }
                    writer.WriteLine();

                }
                writer.Close();


            }
            fileWriter.Close();

        }
        private string filename;

        private void lineChanger(string newText, string fileNameBase, string fileNameNew, int line_to_edit)
        {
            string[] arrLine = File.ReadAllLines(fileNameBase);
            arrLine[line_to_edit - 1] = newText;
            File.WriteAllLines(fileNameNew, arrLine);
        }
    }


}
