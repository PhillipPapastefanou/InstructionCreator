﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InctructionFileCreator.V1._7.ClusterSetups;

namespace InctructionFileCreator.Factories
{
    class ISIMIP_Factory
    {


        public ISIMIP_Factory()
        {

            var scenarios = Enum.GetValues(typeof(ScenarioType)).Cast<ScenarioType>();
            var esm_types = Enum.GetValues(typeof(EarthSystemModelType)).Cast<EarthSystemModelType>();

            foreach (var scenario in scenarios)
            {
                Directory.CreateDirectory(scenario.ToString());
                foreach (var esm in esm_types)
                {
                    Directory.CreateDirectory(scenario.ToString() + "//" + esm.ToString());
                    Directory.SetCurrentDirectory(scenario.ToString() + "//" + esm.ToString());
                   

                    File.Copy(@"..\..\..\..\submit3.sh", "submit3.sh");


                    AB_Isimip_Setup_v174 setup = new AB_Isimip_Setup_v174(esm, scenario,  @"..\..\..\..\masterBase174.ins");


                    Directory.SetCurrentDirectory("..//..");

                }
            }




        }
    }
}
