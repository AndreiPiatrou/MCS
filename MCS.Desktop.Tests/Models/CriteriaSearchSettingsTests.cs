using MCS.Desktop.ViewModels;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MCS.Desktop.Tests.Models
{
    [TestClass]
    public class CriteriaSearchSettingsTests
    {
        private CriteriaSearchSettings settings;

        [TestMethod]
        public void IsValid_InvalidPointsCount()
        {
            settings.PointsCount = 0;

            Assert.IsFalse(settings.IsValid);
        }

        [TestMethod]
        public void IsValid_InvalidBounds_InvalidParameter1Bounds()
        {
            settings.Parameter1Max = settings.Parameter1Min;

            Assert.IsFalse(settings.IsValid);
        }

        [TestMethod]
        public void IsValid_InvalidBounds_InvalidParameter2Bounds()
        {
            settings.Parameter2Max = settings.Parameter2Min;

            Assert.IsFalse(settings.IsValid);
        }

        [TestMethod]
        public void IsValid_InvalidBounds_InvalidParameter3Bounds()
        {
            settings.Parameter3Max = settings.Parameter3Min;

            Assert.IsFalse(settings.IsValid);
        }

        [TestMethod]
        public void IsValid_InvalidBounds_InvalidParameter4Bounds()
        {
            settings.Parameter4Max = settings.Parameter4Min;

            Assert.IsFalse(settings.IsValid);
        }

        [TestMethod]
        public void UseMonteCarlo_IsFalse()
        {
            settings.Method = GenerationMethod.SimpleRandom;

            Assert.IsFalse(settings.UseMonteCarlo);
        }

        [TestMethod]
        public void UseMonteCarlo_IsTrue()
        {
            settings.Method = GenerationMethod.MonteCarlo;

            Assert.IsTrue(settings.UseMonteCarlo);
        }

        [TestInitialize]
        public void Setup()
        {
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
    }
}
