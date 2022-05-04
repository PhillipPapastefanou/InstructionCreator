using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InctructionFileCreator.Parameters
{
    class GeneralParametersTrunk41 : IGeneralParameters
    {
        public string Title { get; set; }
        public string Outputdirectory { get; set; }
        public VegetationMode Vegmode { get; set; }
        public int Nyear_spinup { get; set; }
        public bool IfCalcSLA { get; set; }
        public bool IfCalcCtoN { get; set; }
        public bool IfFire { get; set; }
        public int NPatch { get; set; }
        public int PatchArea { get; set; }
        public int EstInterval { get; set; }
        public bool IfDisturb { get; set; }
        public int DistInterval { get; set; }
        public bool IfBgEstab { get; set; }
        public bool IfsMe { get; set; }
        public bool IfStochEstab { get; set; }
        public bool IfStochMort { get; set; }
        public bool IfCDebt { get; set; }
        public WaterUptakeType WaterUptake { get; set; }
        public bool Textured_Soil { get; set; }
        public bool IfSmoothGreffMort { get; set; }
        public bool IfDroughtLimitedEstab { get; set; }
        public bool IfRainOnWetDaysOnly { get; set; }
        public bool IfBvoc { get; set; }
        public bool Run_Landcover { get; set; }
        public bool IfNLim { get; set; }
        public bool IfCentury { get; set; }
        public int FreeNYears { get; set; }
        public double NFix_a { get; set; }
        public double NFix_b { get; set; }
        public double NRelocFrac { get; set; }
        public bool Restart { get; set; }
        public bool Save_State { get; set; }
        string IGeneralParameters.Title { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        string IGeneralParameters.Outputdirectory { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        VegetationMode IGeneralParameters.Vegmode { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        int IGeneralParameters.Nyear_spinup { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        bool IGeneralParameters.IfCalcSLA { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        bool IGeneralParameters.IfCalcCtoN { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        bool IGeneralParameters.IfFire { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        int IGeneralParameters.NPatch { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        int IGeneralParameters.PatchArea { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        int IGeneralParameters.EstInterval { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        bool IGeneralParameters.IfDisturb { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        int IGeneralParameters.DistInterval { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        bool IGeneralParameters.IfBgEstab { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        bool IGeneralParameters.IfsMe { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        bool IGeneralParameters.IfStochEstab { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        bool IGeneralParameters.IfStochMort { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        bool IGeneralParameters.IfCDebt { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        WaterUptakeType IGeneralParameters.WaterUptake { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        bool IGeneralParameters.Textured_Soil { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        bool IGeneralParameters.IfSmoothGreffMort { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        bool IGeneralParameters.IfDroughtLimitedEstab { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        bool IGeneralParameters.IfRainOnWetDaysOnly { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        bool IGeneralParameters.IfBvoc { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        bool IGeneralParameters.Run_Landcover { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        bool IGeneralParameters.IfNLim { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        bool IGeneralParameters.IfCentury { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        int IGeneralParameters.FreeNYears { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        double IGeneralParameters.NFix_a { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        double IGeneralParameters.NFix_b { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        double IGeneralParameters.NRelocFrac { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        double IGeneralParameters.Soildepth_upper { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        double IGeneralParameters.Soildepth_lower { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        bool IGeneralParameters.Restart { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        bool IGeneralParameters.Save_State { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public virtual object Clone()
        {
            IGeneralParameters generalParameters = new GeneralParametersTrunk();

            var properties = this.GetType().GetProperties();

            foreach (var p in properties.Where(prop => prop.CanRead && prop.CanWrite))
            {
                object copyValue = p.GetValue(this);
                p.SetValue(generalParameters, copyValue);
            }

            return generalParameters;
        }

        public virtual void Compare(IGeneralParameters other)
        {
            GeneralParametersTrunk otherT = other as GeneralParametersTrunk;

            var properties = this.GetType().GetProperties();

            foreach (var prop in properties)
            {
                Console.WriteLine(prop.Name + " " + prop.GetValue(this) + " " + prop.GetValue(otherT));
            }
        }

        object IGeneralParameters.Clone()
        {
            throw new NotImplementedException();
        }

        void IGeneralParameters.Compare(IGeneralParameters other)
        {
            throw new NotImplementedException();
        }
    }
}
