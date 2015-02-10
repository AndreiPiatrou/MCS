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

        public ParametersSet ParametersSet
        {
            get;
            set;
        }

        public double Value { get; set; }

        public int PoinIndex { get; set; }
    }
}