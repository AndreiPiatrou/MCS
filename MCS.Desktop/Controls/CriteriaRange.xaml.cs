using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Input;

using Caliburn.Micro;

using OxyPlot;
using OxyPlot.Series;

namespace MCS.Desktop.Controls
{
    public partial class CriteriaRange
    {
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(CriteriaRange), new PropertyMetadata(default(string)));
        public static readonly DependencyProperty MinimumProperty = DependencyProperty.Register("Minimum", typeof(double), typeof(CriteriaRange), new PropertyMetadata(default(double)));
        public static readonly DependencyProperty MaximunProperty = DependencyProperty.Register("Maximun", typeof(double), typeof(CriteriaRange), new PropertyMetadata(default(double)));
        public static readonly DependencyProperty MinRangeProperty = DependencyProperty.Register("MinRange", typeof(double), typeof(CriteriaRange), new PropertyMetadata(default(double)));
        public static readonly DependencyProperty UpperValueProperty = DependencyProperty.Register("UpperValue", typeof(double), typeof(CriteriaRange), new PropertyMetadata(default(double)));
        public static readonly DependencyProperty LowerValueProperty = DependencyProperty.Register("LowerValue", typeof(double), typeof(CriteriaRange), new PropertyMetadata(default(double)));
        public static readonly DependencyProperty ItemsProperty = DependencyProperty.Register("Items", typeof(ObservableCollection<double>), typeof(CriteriaRange), new PropertyMetadata(default(IObservableCollection<double>), ItemsPropertyChangedCallback));

        private static void ItemsPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (CriteriaRange)d;

            var value = e.OldValue as ObservableCollection<double>;
            if (value != null)
            {
                control.UnsubscribeOnCollectionChange(value);
            }

            value = e.NewValue as ObservableCollection<double>;
            if (value != null)
            {
                control.SubscribeOnChange(value);
                control.UpdateItems(value);
            }
        }

        public CriteriaRange()
        {
            InitializeComponent();
        }

        public string Title
        {
            get
            {
                return (string)GetValue(TitleProperty);
            }
            set
            {
                SetValue(TitleProperty, value);
            }
        }

        public double Minimum
        {
            get
            {
                return (double)GetValue(MinimumProperty);
            }
            set
            {
                SetValue(MinimumProperty, value);
            }
        }

        public double Maximun
        {
            get
            {
                return (double)GetValue(MaximunProperty);
            }
            set
            {
                SetValue(MaximunProperty, value);
            }
        }

        public double MinRange
        {
            get
            {
                return (double)GetValue(MinRangeProperty);
            }
            set
            {
                SetValue(MinRangeProperty, value);
            }
        }

        public double UpperValue
        {
            get
            {
                return (double)GetValue(UpperValueProperty);
            }
            set
            {
                SetValue(UpperValueProperty, value);
            }
        }

        public double LowerValue
        {
            get
            {
                return (double)GetValue(LowerValueProperty);
            }
            set
            {
                SetValue(LowerValueProperty, value);
            }
        }

        public ObservableCollection<double> Items
        {
            get
            {
                return (ObservableCollection<double>)GetValue(ItemsProperty);
            }
            set
            {
                SetValue(ItemsProperty, value);
            }
        }

        private void SubscribeOnChange(ObservableCollection<double> value)
        {
            value.CollectionChanged += ItemsOnCollectionChanged;
        }

        private void UnsubscribeOnCollectionChange(ObservableCollection<double> value)
        {
            value.CollectionChanged -= ItemsOnCollectionChanged;
        }

        private void ItemsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateItems(Items);
        }

        private void UpdateItems(IEnumerable<double> items)
        {
            ResetView();
            var enumerable = items as IList<double> ?? items.ToList();
            if (!enumerable.Any())
            {
                return;
            }

            var pointsList = new List<Point>();
            var minimum = enumerable.Min();
            var maximum = enumerable.Max();
            var totalDelta = maximum - minimum;
            var delta = totalDelta / ((enumerable.Count() < 50 ? enumerable.Count : 50) * 1.0);

            var current = minimum;
            while (Math.Abs(current - maximum) > delta)
            {
                var value = current + delta / 2d;
                var nextCurrentValue = current + delta;
                var count = enumerable.Count(d => d >= current && d < nextCurrentValue);

                pointsList.Add(new Point(value, count));

                current = nextCurrentValue;
            }

            pointsList.Insert(0, new Point(minimum, 1));
            pointsList.Add(new Point(maximum, 1));

            ShowPoint(pointsList);

            Minimum = minimum;
            Maximun = maximum;

            LowerValue = minimum;
            UpperValue = maximum;
        }

        private void ShowPoint(IEnumerable<Point> pointsList)
        {
            var model = new PlotModel();
            var s1 = new LineSeries(Title)
            {
                Color = OxyColors.SkyBlue,
                MarkerType = MarkerType.Circle,
                MarkerSize = 3,
                MarkerStroke = OxyColors.White,
                MarkerFill = OxyColors.SkyBlue,
                MarkerStrokeThickness = 1,
                Smooth = true,
                TrackerFormatString = "Функция критерия:      {0}\n" +
                                      "X(значение критерия):{2}\n" +
                                      "Y(кол-во точек):           {4}"
            };

            foreach (var point in pointsList)
            {
                s1.Points.Add(new DataPoint(point.X, point.Y));
            }

            model.Series.Add(s1);
            Plot.Model = model;
        }

        private void ResetView()
        {
            Plot.Model = null;
        }

        private void RangeSlider_OnLostMouseCapture(object sender, MouseEventArgs e)
        {
            var be = RangeSlider.GetBindingExpression(MahApps.Metro.Controls.RangeSlider.UpperValueProperty);
            if (be != null)
            {
                be.UpdateSource();
            }

            be = RangeSlider.GetBindingExpression(MahApps.Metro.Controls.RangeSlider.LowerValueProperty);
            if (be != null)
            {
                be.UpdateSource();
            }
        }
    }
}
