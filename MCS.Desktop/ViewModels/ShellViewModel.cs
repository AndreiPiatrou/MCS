using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using Caliburn.Micro;

using MCS.Desktop.DataModels;
using MCS.Desktop.Executers;
using MCS.Desktop.Services;

namespace MCS.Desktop.ViewModels
{
    public class ShellViewModel : PropertyChangedBase, IShell
    {
        public ShellViewModel(IWindowManager windowManager,
            IEventAggregator eventAggregator,
            IExecuter executer,
            ParametersSetGeneratorService parametersSetGenerator)
        {
            this.windowManager = windowManager;
            this.eventAggregator = eventAggregator;
            this.executer = executer;
            this.parametersSetGenerator = parametersSetGenerator;

            this.executer.IsRunningChanged += (s, e) => InProcess = e.IsRunning;
            Settings = new CriteriaSearchSettings(100, 1, 10, 5, 1, 100, 20, 400, 30)
            {
                Method = GenerationMethod.MonteCarlo,
                AdditionalParameter1 = 0,
                AdditionalParameter2 = 0.5,
                MeasureChange = 0.7
            };
            Criterias = new ObservableCollection<Criteria>(FillCriterias());
        }

        public void GenerateSearch()
        {
            executer.QueueAction<IEnumerable<ParametersSet>>(e => GenerateParametersSet().ToList(),
                sets =>
                {
                    var parametersSets = sets as ParametersSet[] ?? sets.ToArray();
                    foreach (var criteria in Criterias)
                    {
                        criteria.CalculateCriteriaByParametersSet(parametersSets);
                    }
                });
        }

        public IEnumerable<ParametersSet> GenerateParametersSet()
        {
            for (var i = 0; i < Settings.PointsCount; i++)
            {
                yield return parametersSetGenerator.GenerateParametersSet(Settings);
            }
        }

        public void ConvertFilteredResultsToSettings()
        {
            if (FilteredPoints == null || FilteredPoints.Count < 2)
            {
                return;
            }

            Settings = parametersSetGenerator.PointsInfoToSettings(FilteredPoints, Settings);
            NotifyOfPropertyChange(() => Settings);
        }

        public CriteriaSearchSettings Settings { get; set; }

        public ObservableCollection<Criteria> Criterias
        {
            get
            {
                return criterias;
            }
            set
            {
                if (Equals(value, criterias))
                {
                    return;
                }

                criterias = value;
                NotifyOfPropertyChange(() => Criterias);
            }
        }

        public bool InProcess
        {
            get
            {
                return inProcess;
            }

            set
            {
                if (value.Equals(inProcess))
                {
                    return;
                }

                inProcess = value;
                NotifyOfPropertyChange(() => InProcess);
            }
        }

        public ObservableCollection<PointInfo> FilteredPoints
        {
            get
            {
                return filteredPoints;
            }
            set
            {
                if (Equals(value, filteredPoints))
                {
                    return;
                }
                filteredPoints = value;
                NotifyOfPropertyChange(() => FilteredPoints);
            }
        }

        private IEnumerable<Criteria> FillCriterias()
        {
            var result = new List<Criteria>
            {
                new Criteria("Sin(p1) + p2", set => Math.Round(Math.Sin(set.Parameter1) + set.Parameter2, 2)),
                new Criteria("p2 + p3", set => Math.Round(set.Parameter2 + set.Parameter3, 2)),
                new Criteria("sqrt(p1 + p2 + p3 + p4)",
                    set =>
                        Math.Round(
                            Math.Sqrt(set.Parameter1 + set.Parameter2 + set.Parameter3 + set.Parameter4),
                            2)),
                new Criteria("1/p4", set => Math.Round(1 / set.Parameter4, 2))
            };


            SubscribeOnCriteriaChanged(result);

            return result;
        }

        private void SubscribeOnCriteriaChanged(IEnumerable<Criteria> newCriterias)
        {
            foreach (var criteria in newCriterias)
            {
                criteria.FilteredPointsChanged += CriteriaOnFilteredPointsChanged;
            }
        }

        private void CriteriaOnFilteredPointsChanged(object sender, DataChangedEventArgs<IEnumerable<PointInfo>> e)
        {
            UpdateFilteredPointsAsynch(e.Data.ToList());
        }

        private void UpdateFilteredPointsAsynch(IEnumerable<PointInfo> points)
        {
            executer.QueueAction<IEnumerable<PointInfo>>(ex =>
            {
                var list = new ObservableCollection<PointInfo>();
                foreach (var pointInfo in points.Where(p => Criterias.All(c => c.FilteredPoints.Any(p1 => p1.PoinIndex == p.PoinIndex))))
                {
                    list.Add(pointInfo);
                }
                return list;
            },
            infos => FilteredPoints = new ObservableCollection<PointInfo>(infos));
        }

        private readonly IWindowManager windowManager;
        private readonly IEventAggregator eventAggregator;
        private readonly IExecuter executer;
        private readonly ParametersSetGeneratorService parametersSetGenerator;
        private bool inProcess;
        private int pointsCount;
        private ObservableCollection<Criteria> criterias;
        private ObservableCollection<PointInfo> filteredPoints;
    }
}