using System;
using System.Collections.Generic;
using System.Linq;

using MCS.Desktop.ViewModels;

namespace MCS.Desktop.Services
{
    public class ExtremumPointsSearchService
    {
        public PointInfo FindBestByWeightsFunc(IList<PointInfo> points, CriteriaSearchSettings settings)
        {
            var weight1 = (1d /
                           (points.Max(p => p.ParametersSet.Parameter1) -
                            points.Min(p => p.ParametersSet.Parameter1))) /
                          (settings.MeasureChange * 1d /
                           (settings.AdditionalParameter2 * points.Max(p => p.ParametersSet.Parameter1) -
                            settings.AdditionalParameter1 * points.Min(p => p.ParametersSet.Parameter1)) +
                           (1 - settings.MeasureChange) * 1d /
                           (points.Max(p => p.ParametersSet.Parameter1) -
                            points.Min(p => p.ParametersSet.Parameter1)));

            var weight2 = (1d /
                           (points.Max(p => p.ParametersSet.Parameter2) -
                            points.Min(p => p.ParametersSet.Parameter2))) /
                          (settings.MeasureChange * 1d /
                           (settings.AdditionalParameter2 * points.Max(p => p.ParametersSet.Parameter2) -
                            settings.AdditionalParameter1 * points.Min(p => p.ParametersSet.Parameter2)) +
                           (1 - settings.MeasureChange) * 1d /
                           (points.Max(p => p.ParametersSet.Parameter2) -
                            points.Min(p => p.ParametersSet.Parameter2)));

            var weight3 = (1d /
                           (points.Max(p => p.ParametersSet.Parameter3) -
                            points.Min(p => p.ParametersSet.Parameter3))) /
                          (settings.MeasureChange * 1d /
                           (settings.AdditionalParameter2 * points.Max(p => p.ParametersSet.Parameter3) -
                            settings.AdditionalParameter1 * points.Min(p => p.ParametersSet.Parameter3)) +
                           (1 - settings.MeasureChange) * 1d /
                           (points.Max(p => p.ParametersSet.Parameter3) -
                            points.Min(p => p.ParametersSet.Parameter3)));

            var weight4 = (1d /
                           (points.Max(p => p.ParametersSet.Parameter4) -
                            points.Min(p => p.ParametersSet.Parameter4))) /
                          (settings.MeasureChange * 1d /
                           (settings.AdditionalParameter2 * points.Max(p => p.ParametersSet.Parameter4) -
                            settings.AdditionalParameter1 * points.Min(p => p.ParametersSet.Parameter4)) +
                           (1 - settings.MeasureChange) * 1d /
                           (points.Max(p => p.ParametersSet.Parameter4) -
                            points.Min(p => p.ParametersSet.Parameter4)));

            var weightV = (1d /
                           (points.Max(p => p.Value) -
                            points.Min(p => p.Value))) /
                          (settings.MeasureChange * 1d /
                           (settings.AdditionalParameter2 * points.Max(p => p.Value) -
                            settings.AdditionalParameter1 * points.Min(p => p.Value)) +
                           (1 - settings.MeasureChange) * 1d /
                           (points.Max(p => p.Value) -
                            points.Min(p => p.Value)));
                
            return new PointInfo(points.Sum(p => weightV * p.Value) / points.Count,
                points.Sum(p => weight1 * p.ParametersSet.Parameter1) / points.Count,
                points.Sum(p => weight2 * p.ParametersSet.Parameter2) / points.Count,
                points.Sum(p => weight3 * p.ParametersSet.Parameter3) / points.Count,
                points.Sum(p => weight4 * p.ParametersSet.Parameter4) / points.Count);
        }

        public PointInfo FindBestByWeightlessFunc(IList<PointInfo> points)
        {
            return new PointInfo(points.Sum(p => p.Value) / points.Count,
                points.Sum(p => p.ParametersSet.Parameter1) / points.Count,
                points.Sum(p => p.ParametersSet.Parameter2) / points.Count,
                points.Sum(p => p.ParametersSet.Parameter3) / points.Count,
                points.Sum(p => p.ParametersSet.Parameter4) / points.Count);
        }

        public PointInfo FindBestByRandomWeightFunc(IEnumerable<PointInfo> points)
        {
            throw new NotImplementedException();
        }
    }
}
