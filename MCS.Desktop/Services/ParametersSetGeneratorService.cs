using System;
using System.Collections.Generic;
using System.Linq;

using MCS.Desktop.DataModels;
using MCS.Desktop.ViewModels;

namespace MCS.Desktop.Services
{
    public class ParametersSetGeneratorService
    {
        public ParametersSet GenerateParametersSet(CriteriaSearchSettings settings)
        {
            return new ParametersSet(random.NextDouble() * settings.Parameter1Max + settings.Parameter1Min,
                random.NextDouble() * settings.Parameter2Max + settings.Parameter2Min,
                random.NextDouble() * settings.Parameter3Max + settings.Parameter3Min,
                random.NextDouble() * settings.Parameter4Max + settings.Parameter4Min);
        }

        public CriteriaSearchSettings PointsInfoToSettings(IEnumerable<PointInfo> pointInfos,
            CriteriaSearchSettings oldSettings)
        {
            var result = new CriteriaSearchSettings(oldSettings.PointsCount);
            var enumerable = pointInfos as IList<PointInfo> ?? pointInfos.ToList();

            result.Parameter1Min = enumerable.Min(p => p.ParametersSet.Parameter1);
            result.Parameter1Max = enumerable.Max(p => p.ParametersSet.Parameter1);

            result.Parameter2Min = enumerable.Min(p => p.ParametersSet.Parameter2);
            result.Parameter2Max = enumerable.Max(p => p.ParametersSet.Parameter2);

            result.Parameter3Min = enumerable.Min(p => p.ParametersSet.Parameter3);
            result.Parameter3Max = enumerable.Max(p => p.ParametersSet.Parameter3);

            result.Parameter4Min = enumerable.Min(p => p.ParametersSet.Parameter4);
            result.Parameter4Max = enumerable.Max(p => p.ParametersSet.Parameter4);

            return result;
        }

        private readonly Random random = new Random();
    }
}
