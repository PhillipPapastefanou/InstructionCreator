using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SensitivitySetup;

namespace InctructionFileCreator
{
    class BrienenAnalysis
    {
        public BrienenAnalysis()
        {
            UniformParameter psi50 = new UniformParameter("Psi50", -3.5, -1.0);

            psi50.DivideEqually(100);

            
        }

    }
}
