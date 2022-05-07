using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InctructionFileCreator.Parameters;

namespace InctructionFileCreator
{
    class InsFileTrunk41 : IInsFile
    {
        public IGeneralParameters GeneralParameters { get; set; }
        public IDriverFiles DriverFiles { get; set; }
        public PftList Pfts { get; set; }
        public PftType PftType { get ; set ; }
        public StandParameters StandParameters { get; set; }

        public void Compare(IInsFile other)
        {
            throw new NotImplementedException();
        }

        public InsFileTrunk41()
        {
            GeneralParameters = new GeneralParametersTrunk41();
            DriverFiles = new DriverFiles41();
            Pfts = new PftList();
            PftType = PftType.Trunk41;
        }

        public object Clone()
        {
            InsFileTrunk41 newFile = new InsFileTrunk41();
            newFile.GeneralParameters = (GeneralParametersTrunk41)GeneralParameters.Clone();
            newFile.DriverFiles = (DriverFiles41)DriverFiles.Clone();
            foreach (Pft41 pft in Pfts)
            {
                newFile.Pfts.Add((Pft41)pft.Clone());
            }

            return newFile;
        }
    }
}
