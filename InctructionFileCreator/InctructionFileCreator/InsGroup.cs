using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace InctructionFileCreator
{
    public class InsGroup
    {

        public Dictionary<string, string> Parameters { get; set; }
        public string Name { get; set; }
        public List<InsGroup> SubGroups { get; set; }
        public InsGroup()
        {
            Parameters = new Dictionary<string, string>();
            SubGroups = new List<InsGroup>();
        }

        public void AccumulateParameters()
        {
            foreach (InsGroup subGroup in SubGroups)
            {
                subGroup.AccumulateParameters();
            }


            foreach (InsGroup subGroup in SubGroups)
            {

                foreach (KeyValuePair<string, string> subGroupParameter in subGroup.Parameters)
                {
                    string dummyKey = String.Empty;
                    if (Parameters.TryGetValue(subGroupParameter.Key, out dummyKey))
                    {

                        //When iterating through parameters of this list, does new parameters have to be over
                        //written?
                        //Resp_coeff says yes for tropical trees
                        // the cordex tree list however tells something very different...
                        Parameters[subGroupParameter.Key] = subGroupParameter.Value;
                    }

                    else
                    {
                        Parameters.Add(subGroupParameter.Key, subGroupParameter.Value);
                    }
                }
               
            }
        }

    }

    public class InsGroupCollection 
    {
        public InsGroupCollection()
        {
            Groups = new List<InsGroup>();
        }

        public InsGroup this[int i]
        {
            get { return this.Groups[i]; }
            set { this.Groups[i] = value; }
        }


        public InsGroup this[string s]
        {
            

        get
        {
            foreach (InsGroup group in Groups)
            {
                if (group.Name == s)
                {
                    return group;
                }
            }

            return null;
        }

    }

        public List<InsGroup> Groups { get; set; }
    }
}
