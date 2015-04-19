using MCS.Desktop.Services;
using MCS.Desktop.ViewModels;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MCS.Desktop.Tests.Services
{
    [TestClass]
    public class ParametersSetGeneratorServiceTests
    {
        private ParametersSetGeneratorService service;
        private CriteriaSearchSettings settings;

        [TestMethod]
        public void GenerateParametersSet_SimpleRandom_Bounds_PositiveCase()
        {
            var parameter = service.GenerateParametersSet(settings);

            Assert.IsTrue(AreInBounds(parameter.Parameter1, settings.Parameter1Min, settings.Parameter1Max));
            Assert.IsTrue(AreInBounds(parameter.Parameter2, settings.Parameter2Min, settings.Parameter2Max));
            Assert.IsTrue(AreInBounds(parameter.Parameter3, settings.Parameter3Min, settings.Parameter3Max));
            Assert.IsTrue(AreInBounds(parameter.Parameter4, settings.Parameter4Min, settings.Parameter4Max));
        }

        [TestMethod]
        public void GenerateParametersSet_MonteCarlo_Bounds_PositiveCase()
        {
            settings.Method = GenerationMethod.MonteCarlo;
            var parameter = service.GenerateParametersSet(settings);

            Assert.IsTrue(AreInBounds(parameter.Parameter1, settings.Parameter1Min, settings.Parameter1Max));
            Assert.IsTrue(AreInBounds(parameter.Parameter2, settings.Parameter2Min, settings.Parameter2Max));
            Assert.IsTrue(AreInBounds(parameter.Parameter3, settings.Parameter3Min, settings.Parameter3Max));
            Assert.IsTrue(AreInBounds(parameter.Parameter4, settings.Parameter4Min, settings.Parameter4Max));
        }
        
        [TestInitialize]
        public void Setup()
        {
            service = new ParametersSetGeneratorService();
            settings = new CriteriaSearchSettings(10)
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
