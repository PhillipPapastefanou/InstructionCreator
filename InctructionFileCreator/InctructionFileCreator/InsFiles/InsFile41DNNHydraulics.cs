using System;
using InctructionFileCreator.Parameters;
namespace InctructionFileCreator.InsFiles
{
    public class InsFile41DNNHydraulics : IInsFile
    {
        public IGeneralParameters GeneralParameters { get; set; }
        public IDriverFiles DriverFiles { get; set; }
        public PftList Pfts { get; set; }
        public PftType PftType { get; set; }
        public StandParameters StandParameters { get; set; }

        public void Compare(IInsFile other)
        {
            throw new NotImplementedException();
        }

        public InsFile41DNNHydraulics()
        {
            GeneralParameters = new GeneralParameters41HydraulicsDNN();
            DriverFiles = new DriverFilesDNNHyd41();
            Pfts = new PftList();
            StandParameters = new StandParameters();
            //PftType = PftType.Hydraulics41mp;
            PftType = PftType.Hydraulics41;
        }

        public object Clone()
        {
            InsFile41DNNHydraulics newFile = new InsFile41DNNHydraulics();
           
            newFile.GeneralParameters = (GeneralParameters41HydraulicsDNN)GeneralParameters.Clone();
            newFile.DriverFiles = (DriverFilesDNNHyd41)DriverFiles.Clone();
            newFile.StandParameters = (StandParameters)StandParameters.Clone();
            foreach (PftHyd41 pft in Pfts)
            {
                newFile.Pfts.Add((PftHyd41)pft.Clone());
            }

            return newFile;
        }
    }
}
