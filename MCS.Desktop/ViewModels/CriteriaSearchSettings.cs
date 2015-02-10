using Caliburn.Micro;

namespace MCS.Desktop.ViewModels
{
    public class CriteriaSearchSettings : PropertyChangedBase
    {
        public CriteriaSearchSettings(int pointsCount,
            double parameter1Min,
            double parameter2Min,
            double parameter3Min,
            double parameter4Min,
            double parameter1Max,
            double parameter2Max,
            double parameter3Max,
            double parameter4Max)
        {
            this.pointsCount = pointsCount;
            Parameter1Min = parameter1Min;
            Parameter2Min = parameter2Min;
            Parameter3Min = parameter3Min;
            Parameter4Min = parameter4Min;
            Parameter1Max = parameter1Max;
            Parameter2Max = parameter2Max;
            Parameter3Max = parameter3Max;
            Parameter4Max = parameter4Max;
        }

        public CriteriaSearchSettings(int pointsCount)
        {
            this.pointsCount = pointsCount;
        }

        public int PointsCount
        {
            get
            {
                return pointsCount;
            }
            set
            {
                if (value == pointsCount)
                {
                    return;
                }
                pointsCount = value;
                NotifyOfPropertyChange(() => PointsCount);
            }
        }

        public double Parameter1Min { get; set; }
        public double Parameter2Min { get; set; }
        public double Parameter3Min { get; set; }
        public double Parameter4Min { get; set; }
        public double Parameter1Max { get; set; }
        public double Parameter2Max { get; set; }
        public double Parameter3Max { get; set; }
        public double Parameter4Max { get; set; }

        private int pointsCount;
    }
}
