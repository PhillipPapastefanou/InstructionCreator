using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InctructionFileCreator;
using InctructionFileCreator.InitialSetup;

namespace PFT_Selection
{
    class Program
    {
        static void Main(string[] args)
        {
            PftsAndRestParser parser = new PftsAndRestParser("F:\\Dropbox\\UNI\\Projekte\\A07_DNN_LPJ-GUESS\\3_Insfiles\\europe_all_pft.ins");

            parser.Read();

            parser.WriteIndividualPftFiles();

        }
    }
}
