using Caliburn.Micro;

namespace MCS.Desktop.ViewModels
{
    public class Criteria : PropertyChangedBase
    {
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

        public double Value
        {
            get
            {
                return value;
            }

            set
            {
                if (Equals(this.value, value))
                {
                    return;
                }

                this.value = value;
                NotifyOfPropertyChange(() => Value);
            }
        }

        private string name;
        private double minValue;
        private double maxValue;
        private double value;
    }
}
