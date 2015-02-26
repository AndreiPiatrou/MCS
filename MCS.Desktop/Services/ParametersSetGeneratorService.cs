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
            if (settings.Method == GenerationMethod.SimpleRandom)
            {
                return new ParametersSet(SimpleRandom(settings.Parameter1Min, settings.Parameter1Max),
                    SimpleRandom(settings.Parameter2Min, settings.Parameter2Max),
                    SimpleRandom(settings.Parameter3Min, settings.Parameter3Max),
                    SimpleRandom(settings.Parameter4Min, settings.Parameter4Max));
            }

            return
                new ParametersSet(
                    GenerateByMonteCarlo(settings.Parameter1Min,
                        settings.Parameter1Max,
                        settings.AdditionalParameter1,
                        settings.AdditionalParameter2,
                        settings.MeasureChange),
                    GenerateByMonteCarlo(settings.Parameter2Min,
                        settings.Parameter2Max,
                        settings.AdditionalParameter1,
                        settings.AdditionalParameter2,
                        settings.MeasureChange),
                    GenerateByMonteCarlo(settings.Parameter3Min,
                        settings.Parameter3Max,
                        settings.AdditionalParameter1,
                        settings.AdditionalParameter2,
                        settings.MeasureChange),
                    GenerateByMonteCarlo(settings.Parameter4Min,
                        settings.Parameter4Max,
                        settings.AdditionalParameter1,
                        settings.AdditionalParameter2,
                        settings.MeasureChange));
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

            result.AdditionalParameter1 = oldSettings.AdditionalParameter1;
            result.AdditionalParameter2 = oldSettings.AdditionalParameter2;
            result.MeasureChange = oldSettings.MeasureChange;
            result.Method = oldSettings.Method;

            return result;
        }

        private double SimpleRandom(double minValue, double maxValue)
        {
            return random.NextDouble() * (maxValue - minValue) + minValue;
        }

        private double GenerateByMonteCarlo(
            double minValue,
            double maxValue,
            double leftMeasure,
            double rightMeasure,
            double measureChance)
        {
            var inMeasures = false;
            if (measureChance > 0d)
            {
                inMeasures = random.Next() % (1.0 / measureChance) < measureChance;
            }

            return inMeasures
                ? SimpleRandom(minValue + (maxValue - minValue) * leftMeasure, minValue + (maxValue - minValue) * rightMeasure)
                : SimpleRandom(minValue, maxValue);
        }

        private readonly Random random = new Random();
    }
}
