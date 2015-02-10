using System;

namespace MCS.Desktop.DataModels
{
    public class ParametersSet
    {
        public ParametersSet(double parameter1, double parameter2, double parameter3, double parameter4)
        {
            Parameter1 = Math.Round(parameter1, 4);
            Parameter2 = Math.Round(parameter2, 4);
            Parameter3 = Math.Round(parameter3, 4);
            Parameter4 = Math.Round(parameter4, 4);
        }

        public double Parameter1 { get; set; }
        public double Parameter2 { get; set; }
        public double Parameter3 { get; set; }
        public double Parameter4 { get; set; }
    }
}