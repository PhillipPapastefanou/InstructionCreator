using System;
using InctructionFileCreator.Parameters;


namespace InctructionFileCreator
{
    class InsfileHydraulicsMort : IInsFile
    {
        public IGeneralParameters GeneralParameters { get; set; }
        public IDriverFiles DriverFiles { get; set; }
        public PftList Pfts { get; set; }
        public PftType PftType { get; set; }
        public void Compare(IInsFile other)
        {
            Console.WriteLine("Comparing Insfiles:");
            GeneralParameters.Compare(other.GeneralParameters);
            for (int i = 0; i < Pfts.Count; i++)
            {
                Pfts[i].Compare(other.Pfts[i]);
            }

        }


        public InsfileHydraulicsMort()
        {
            GeneralParameters = new GeneralParametersHydraulics();
            DriverFiles = new DriverFilesHydraulics();
            Pfts = new PftList();
            PftType = PftType.HydraulicsMort;
        }

        public object Clone()
        {
            InsfileHydraulicsMort newFile = new InsfileHydraulicsMort();
            newFile.GeneralParameters = (GeneralParametersHydraulics)GeneralParameters.Clone();
            newFile.DriverFiles = (DriverFilesHydraulics)DriverFiles.Clone();
            foreach (IPft pft in Pfts)
            {
                newFile.Pfts.Add((PftHydMort)pft.Clone());
            }
            newFile.PftType = this.PftType;
            return newFile;
        }
    }
}
