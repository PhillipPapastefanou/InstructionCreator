using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static InctructionFileCreator.IPft;
using InctructionFileCreator.Parameters;

namespace InctructionFileCreator
{
    public interface IInsFile : ICloneable
    { 
        IGeneralParameters GeneralParameters { get; set; }
        IDriverFiles DriverFiles { get; set; }
        StandParameters StandParameters { get; set; }
        PftList Pfts { get; set; }
        PftType PftType { get; set; }

        void Compare(IInsFile other);
    }
}
