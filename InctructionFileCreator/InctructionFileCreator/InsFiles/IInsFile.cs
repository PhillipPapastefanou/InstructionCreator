using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InctructionFileCreator
{
    public interface IInsFile : ICloneable
    { 
        IGeneralParameters GeneralParameters { get; set; }
        IDriverFiles DriverFiles { get; set; }
        PftList Pfts { get; set; }
        PftType PftType { get; set; }

        void Compare(IInsFile other);
    }
}
