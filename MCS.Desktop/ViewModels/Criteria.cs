using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reactive.Subjects;

using Caliburn.Micro;

using MCS.Desktop.DataModels;

namespace MCS.Desktop.ViewModels
{
    public class Criteria : PropertyChangedBase
    {
        public Criteria(string name, Func<ParametersSet, double> calculationFunc)
        {
            Contract.Requires(!string.IsNullOrEmpty(name));
            Contract.Requires(calculationFunc != null);

            this.name = name;
            this.calculationFunc = calculationFunc;
        }

        public IEnumerable<PointInfo> CalculateCriteriaByParametersSet(IEnumerable<ParametersSet> sets)
        {
            Contract.Requires(sets != null);

            var list = new List<PointInfo>();
            var parametersSets = sets as ParametersSet[] ?? sets.ToArray();

            for (var i = 0; i < parametersSets.Count(); i++)
            {
                list.Add(new PointInfo(i, parametersSets[i]) { Value = calculationFunc(parametersSets[i]) });
            }

            Points = list;

            return Points;
        }

        public PointInfo CalculateMinPointByWeigths(CriteriaSearchSettings settings)
        {
            throw new NotImplementedException();
        } 

        public IList<PointInfo> Points
        {
            get
            {
                return points;
            }

            private set
            {
                if (Equals(value, points))
                {
                    return;
                }

                points = value;
                NotifyOfPropertyChange(() => Points);
                NotifyOfPropertyChange(() => PointValues);
            }
        }

        public ObservableCollection<PointInfo> FilteredPoints
        {
            get
            {
                if (points == null)
                {
                    return new ObservableCollection<PointInfo>();
                }

                return
                    new ObservableCollection<PointInfo>(
                        points.Where(p => p.Value >= MinValue && p.Value <= MaxValue));
            }
        }

        public ObservableCollection<double> PointValues
        {
            get
            {
                return new ObservableCollection<double>(points == null ? new List<double>() : points.Select(p => p.Value));
            }
        }
        
        public Subject<IEnumerable<PointInfo>> FilteredPointsSubject = new Subject<IEnumerable<PointInfo>>();

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
                InvokeFIlteredPointsWereChanged();
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
                InvokeFIlteredPointsWereChanged();
            }
        }

        private void InvokeFIlteredPointsWereChanged()
        {
            FilteredPointsSubject.OnNext(FilteredPoints.ToList());
        }

        private readonly Func<ParametersSet, double> calculationFunc;
        private string name;
        private double minValue;
        private double maxValue;
        private IList<PointInfo> points;
    }
}
