using System;
using System.Linq;
namespace InctructionFileCreator.Parameters
{

    public enum Intercroptype
    {
        Nointercrop
    }

    public enum Naturalvegtype
    {
        All
    }
    public class StandParameters
    {

        public bool Stinclude { get; set; }
        public LandcoverType Landcover { get; set; }
        public Intercroptype Intercrop { get; set; }
        public Naturalvegtype Naturalveg { get; set; }
        public bool Restrictpfts { get; set; }
        public string Name { get; set; }


        public StandParameters()
        {

        }


        public object Clone()
        {
            StandParameters generalParameters = new StandParameters();

            var properties = this.GetType().GetProperties();

            foreach (var p in properties.Where(prop => prop.CanRead && prop.CanWrite))
            {
                object copyValue = p.GetValue(this);
                p.SetValue(generalParameters, copyValue);
            }

            return generalParameters;
        }
    }
}
