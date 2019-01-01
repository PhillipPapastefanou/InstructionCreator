using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InctructionFileCreator
{
    interface IInsFile : ICloneable
    {
        IGeneralParameters GeneralParameters { get; set; }
        IDriverFiles DriverFiles { get; set; }
        List<IPft> Pfts { get; set; }
        PftType PftType { get; set; }

        void Compare(IInsFile other);
    }
}
