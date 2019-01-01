using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InctructionFileCreator.Parameters;

namespace InctructionFileCreator
{
    class InsFileHydraulics:IInsFile
    {
        public IGeneralParameters GeneralParameters { get; set; }
        public IDriverFiles DriverFiles { get; set; }
        public List<IPft> Pfts { get; set; }
        public PftType PftType { get; set; }


        public InsFileHydraulics()
        {
            GeneralParameters = new GeneralParametersHydraulics();
            DriverFiles = new DriverFilesHydraulics();
            Pfts = new List<IPft>();
            PftType = PftType.Hydraulics;
        }
    }
}
