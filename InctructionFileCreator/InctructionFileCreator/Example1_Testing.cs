using System;
using InctructionFileCreator.InsFiles;

namespace InctructionFileCreator
{
    public class Example1_Testing
    {
        public Example1_Testing()
        {

            string filename = "/Users/pp/Documents/Repos/guess4.1_hydraulics/cmake-build-release/Europe_Test2_expand.ins";

            // Create a new Insfile. The class type specifies for which version of LPJ-GUESS. In this case it is the
            // 4.1 version using Hydraulics and SmartOutput
            InsFile41Hydraulics insfile = new InsFile41Hydraulics();

            // Creater the parsing object based on the filename and insfile
            InsParser parser = new InsParser(filename, insfile);
            parser.Read();


            // The file has to be cloned because C# usually works with references types, but here we want an actual copy.
            InsFile41Hydraulics hydFile = (IInsFile)insfile.Clone() as InsFile41Hydraulics;


            ////////////////////////////////////////////////////////////////////////////////////////////////
            //
            //
            // the actual change that you want to apply will come here
            //
            //
            ////////////////////////////////////////////////////////////////////////////////////////////////


            // Write out the filename
            Writer ws = new Writer(hydFile, "Parsed_Insfile.ins");
            ws.Write();
            ws.Dispose();


        }
    }
}
