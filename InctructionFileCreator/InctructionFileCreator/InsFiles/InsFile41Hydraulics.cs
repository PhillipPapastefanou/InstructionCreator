using System;
using InctructionFileCreator.Parameters;

namespace InctructionFileCreator.InsFiles
{
    class InsFile41Hydraulics : IInsFile
    {
        public IGeneralParameters GeneralParameters { get; set; }
        public IDriverFiles DriverFiles { get; set; }
        public PftList Pfts { get; set; }
        public PftType PftType { get; set; }
        public StandParameters StandParameters { get; set ; }

        public void Compare(IInsFile other)
        {
            throw new NotImplementedException();
        }

        public InsFile41Hydraulics()
        {
            //GeneralParameters = new GeneralParameters41HydraulcisWaterParams();
            GeneralParameters = new GeneralParameters41Hydraulics();
            DriverFiles = new DriverFilesHyd41();
            Pfts = new PftList();
            StandParameters = new StandParameters();
            //PftType = PftType.Hydraulics41mp;
            PftType = PftType.Hydraulics41;
        }

        public object Clone()
        {
            InsFile41Hydraulics newFile = new InsFile41Hydraulics();
            //newFile.GeneralParameters = (GeneralParameters41HydraulcisWaterParams)GeneralParameters.Clone();
            newFile.GeneralParameters = (GeneralParameters41Hydraulics)GeneralParameters.Clone();
            newFile.DriverFiles = (DriverFilesHyd41)DriverFiles.Clone();
            newFile.StandParameters = (StandParameters)StandParameters.Clone();
            foreach (PftHyd41 pft in Pfts)
            {
                newFile.Pfts.Add((PftHyd41)pft.Clone());
            }

            return newFile;
        }
    }
}
