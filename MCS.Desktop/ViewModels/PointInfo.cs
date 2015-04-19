using MCS.Desktop.DataModels;

namespace MCS.Desktop.ViewModels
{
    public class PointInfo
    {
        public PointInfo(int poinIndex, ParametersSet parametersSet)
        {
            PoinIndex = poinIndex;
            ParametersSet = parametersSet;
        }

        public PointInfo(double value, double param1, double param2, double param3, double param4)
        {
            Value = value;
            ParametersSet = new ParametersSet(param1, param2, param3, param4);
        }

        public ParametersSet ParametersSet
        {
            get;
            set;
        }

        public double Value { get; set; }

        public int PoinIndex { get; set; }

        public int PointIndexOnView
        {
            get
            {
                return PoinIndex + 1;
            }
        }
    }
}