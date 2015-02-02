using Caliburn.Micro;

namespace MCS.Desktop.ViewModels
{
    public class Criteria: PropertyChangedBase
    {
        public Criteria(string name, double currentValue, double maxValue, double minValue)
        {
            this.name = name;
            this.currentValue = currentValue;
            this.maxValue = maxValue;
            this.minValue = minValue;
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                if (value == name)
                {
                    return;
                }

                name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }

        public double CurrentValue
        {
            get
            {
                return currentValue;
            }
            set
            {
                if (value.Equals(currentValue))
                {
                    return;
                }

                currentValue = value;
                NotifyOfPropertyChange(() => CurrentValue);
            }
        }

        public double MinValue
        {
            get
            {
                return minValue;
            }
            set
            {
                if (value.Equals(minValue))
                {
                    return;
                }

                minValue = value;
                NotifyOfPropertyChange(() => MinValue);
            }
        }

        public double MaxValue
        {
            get
            {
                return maxValue;
            }
            set
            {
                if (value.Equals(maxValue))
                {
                    return;
                }

                maxValue = value;
                NotifyOfPropertyChange(() => MaxValue);
            }
        }

        private string name;
        private double currentValue;
        private double minValue;
        private double maxValue;
    }
}
