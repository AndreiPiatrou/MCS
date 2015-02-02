using Caliburn.Micro;

namespace MCS.Desktop.ViewModels
{
    public class JobSeeker : PropertyChangedBase
    {
        public JobSeeker(int age, double height, double iq, double weight)
        {
            this.age = age;
            this.height = height;
            this.iq = iq;
            this.weight = weight;
        }

        public int Age
        {
            get
            {
                return age;
            }
            set
            {
                if (value == age)
                {
                    return;
                }
                age = value;
                NotifyOfPropertyChange(() => Age);
            }
        }

        public double Height
        {
            get
            {
                return height;
            }
            set
            {
                if (value.Equals(height))
                {
                    return;
                }
                height = value;
                NotifyOfPropertyChange(() => Height);
            }
        }

        public double IQ
        {
            get
            {
                return iq;
            }
            set
            {
                if (value.Equals(iq))
                {
                    return;
                }
                iq = value;
                NotifyOfPropertyChange(() => IQ);
            }
        }

        public double Weight
        {
            get
            {
                return weight;
            }
            set
            {
                if (value.Equals(weight))
                {
                    return;
                }
                weight = value;
                NotifyOfPropertyChange(() => Weight);
            }
        }

        private int age;
        private double height;
        private double iq;
        private double weight;
    }
}
