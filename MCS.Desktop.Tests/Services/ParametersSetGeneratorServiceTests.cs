using MCS.Desktop.Services;
using MCS.Desktop.ViewModels;

using NUnit.Framework;

namespace MCS.Desktop.Tests.Services
{
    [TestFixture]
    public class ParametersSetGeneratorServiceTests
    {
        [TestCase(GenerationMethod.SimpleRandom)]
        [TestCase(GenerationMethod.MonteCarlo)]
        public void GenerateParametersSet_SimpleRandom_AreInBounds(GenerationMethod method)
        {
            var settings = CreateSettings();
            var service = CreateService();

            settings.Method = method;
            var parameter = service.GenerateParametersSet(settings);

            Assert.IsTrue(AreInBounds(parameter.Parameter1, settings.Parameter1Min, settings.Parameter1Max));
            Assert.IsTrue(AreInBounds(parameter.Parameter2, settings.Parameter2Min, settings.Parameter2Max));
            Assert.IsTrue(AreInBounds(parameter.Parameter3, settings.Parameter3Min, settings.Parameter3Max));
            Assert.IsTrue(AreInBounds(parameter.Parameter4, settings.Parameter4Min, settings.Parameter4Max));
        }

        private ParametersSetGeneratorService CreateService()
        {
            return new ParametersSetGeneratorService();
        }

        private CriteriaSearchSettings CreateSettings()
        {
            return new CriteriaSearchSettings(10)
            {
                Parameter1Min = 0,
                Parameter1Max = 10,
                Parameter2Min = 0,
                Parameter2Max = 10,
                Parameter3Min = 0,
                Parameter3Max = 10,
                Parameter4Min = 0,
                Parameter4Max = 10,
                Method = GenerationMethod.SimpleRandom
            };
        }

        private bool AreInBounds(double value, double leftBound, double rightBound)
        {
            return value >= leftBound && value <= rightBound;
        }
    }
}
