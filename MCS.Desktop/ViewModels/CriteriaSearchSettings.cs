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
            Method = GenerationMethod.SimpleRandom;
        }

        public GenerationMethod Method
        {
            get
            {
                return method;
            }
            set
            {
                if (method == value)
                {
                    return;
                }

                method = value;
                UseMonteCarlo = method == GenerationMethod.MonteCarlo;
            }
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
                NotifyOfPropertyChange(() => IsValid);
            }
        }

        public double Parameter1Min
        {
            get
            {
                return parameter1Min;
            }
            set
            {
                if (value.Equals(parameter1Min))
                {
                    return;
                }

                parameter1Min = value;
                NotifyOfPropertyChange(() => Parameter1Min);
                NotifyOfPropertyChange(() => IsValid);
            }
        }

        public double Parameter2Min
        {
            get
            {
                return parameter2Min;
            }
            set
            {
                if (value.Equals(parameter2Min))
                {
                    return;
                }

                parameter2Min = value;
                NotifyOfPropertyChange(() => Parameter2Min);
                NotifyOfPropertyChange(() => IsValid);
            }
        }

        public double Parameter3Min
        {
            get
            {
                return parameter3Min;
            }
            set
            {
                if (value.Equals(parameter3Min))
                {
                    return;
                }

                parameter3Min = value;
                NotifyOfPropertyChange(() => Parameter3Min);
                NotifyOfPropertyChange(() => IsValid);
            }
        }

        public double Parameter4Min
        {
            get
            {
                return parameter4Min;
            }
            set
            {
                if (value.Equals(parameter4Min))
                {
                    return;
                }

                parameter4Min = value;
                NotifyOfPropertyChange(() => Parameter4Min);
                NotifyOfPropertyChange(() => IsValid);
            }
        }

        public double Parameter1Max
        {
            get
            {
                return parameter1Max;
            }
            set
            {
                if (value.Equals(parameter1Max))
                {
                    return;
                }

                parameter1Max = value;
                NotifyOfPropertyChange(() => Parameter1Max);
                NotifyOfPropertyChange(() => IsValid);
            }
        }

        public double Parameter2Max
        {
            get
            {
                return parameter2Max;
            }
            set
            {
                if (value.Equals(parameter2Max))
                {
                    return;
                }

                parameter2Max = value;
                NotifyOfPropertyChange(() => Parameter2Max);
                NotifyOfPropertyChange(() => IsValid);
            }
        }

        public double Parameter3Max
        {
            get
            {
                return parameter3Max;
            }
            set
            {
                if (value.Equals(parameter3Max))
                {
                    return;
                }

                parameter3Max = value;
                NotifyOfPropertyChange(() => Parameter3Max);
                NotifyOfPropertyChange(() => IsValid);
            }
        }

        public double Parameter4Max
        {
            get
            {
                return parameter4Max;
            }
            set
            {
                if (value.Equals(parameter4Max))
                {
                    return;
                }

                parameter4Max = value;
                NotifyOfPropertyChange(() => Parameter4Max);
                NotifyOfPropertyChange(() => IsValid);
            }
        }

        public bool IsValid
        {
            get
            {
                return RangeIsValid(Parameter1Min, Parameter1Max) &&
                       RangeIsValid(Parameter2Min, Parameter2Max) &&
                       RangeIsValid(Parameter3Min, Parameter3Max) &&
                       RangeIsValid(Parameter4Min, Parameter4Max) &&
                       PointsCount > 0;
            }
        }

        public double AdditionalParameter1
        {
            get
            {
                return additionalParameter1;
            }
            set
            {
                if (value.Equals(additionalParameter1))
                {
                    return;
                }
                additionalParameter1 = value;
                NotifyOfPropertyChange(() => AdditionalParameter1);
            }
        }

        public double AdditionalParameter2
        {
            get
            {
                return additionalParameter2;
            }
            set
            {
                if (value.Equals(additionalParameter2))
                {
                    return;
                }
                additionalParameter2 = value;
                NotifyOfPropertyChange(() => AdditionalParameter2);
            }
        }

        public double MeasureChange
        {
            get
            {
                return measureChange;
            }
            set
            {
                if (value.Equals(measureChange))
                {
                    return;
                }
                measureChange = value;
                NotifyOfPropertyChange(() => MeasureChange);
            }
        }

        public bool UseMonteCarlo
        {
            get
            {
                return useMonteCarlo;
            }
            set
            {
                if (value.Equals(useMonteCarlo))
                {
                    return;
                }

                Method = value ? GenerationMethod.MonteCarlo : GenerationMethod.SimpleRandom;
                useMonteCarlo = value;
                NotifyOfPropertyChange(() => UseMonteCarlo);
            }
        }

        private bool RangeIsValid(double minValue, double maxValue)
        {
            return minValue < maxValue;
        }

        private int pointsCount;
        private double parameter1Min;
        private double parameter2Min;
        private double parameter3Min;
        private double parameter4Min;
        private double parameter1Max;
        private double parameter2Max;
        private double parameter3Max;
        private double parameter4Max;
        private double additionalParameter1;
        private double additionalParameter2;
        private bool useMonteCarlo;
        private GenerationMethod method;
        private double measureChange;
    }
}
