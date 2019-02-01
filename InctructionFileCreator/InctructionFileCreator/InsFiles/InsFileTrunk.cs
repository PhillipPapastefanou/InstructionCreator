using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InctructionFileCreator
{
    class InsFileTrunk: IInsFile
    {
        public IGeneralParameters GeneralParameters { get; set; }
        public IDriverFiles DriverFiles { get; set; }
        public PftList Pfts { get; set; }
        public PftType PftType { get; set; }
        public void Compare(IInsFile other)
        {
            throw new NotImplementedException();
        }

        public InsFileTrunk()
        {
            GeneralParameters = new GeneralParametersTrunk();
            DriverFiles = new DriverFilesTrunk();
            Pfts = new PftList();
            PftType = PftType.Trunk;
        }

        public object Clone()
        {
            IInsFile newFile = new InsFileTrunk();
            newFile.GeneralParameters = (GeneralParametersTrunk)GeneralParameters.Clone();
            newFile.DriverFiles = (DriverFilesTrunk)DriverFiles.Clone();
            foreach (IPft pft in Pfts)
            {
                newFile.Pfts.Add((Pft)pft.Clone());
            }

            newFile.PftType = this.PftType;
            return newFile;
        }
    }
}
