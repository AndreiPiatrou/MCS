using MCS.Desktop.ViewModels;

using NUnit.Framework;

namespace MCS.Desktop.Tests.Models
{
    [TestFixture]
    public class CriteriaSearchSettingsTests
    {
        [Test]
        public void IsValid_InvalidPointsCount()
        {
            var settings = CreateSettings();

            settings.PointsCount = 0;

            Assert.IsFalse(settings.IsValid);
        }

        [Test]
        public void IsValid_InvalidBounds_InvalidParameter1Bounds()
        {
            var settings = CreateSettings();

            settings.Parameter1Max = settings.Parameter1Min;

            Assert.IsFalse(settings.IsValid);
        }

        [Test]
        public void IsValid_InvalidBounds_InvalidParameter2Bounds()
        {
            var settings = CreateSettings();

            settings.Parameter2Max = settings.Parameter2Min;

            Assert.IsFalse(settings.IsValid);
        }

        [Test]
        public void IsValid_InvalidBounds_InvalidParameter3Bounds()
        {
            var settings = CreateSettings();

            settings.Parameter3Max = settings.Parameter3Min;

            Assert.IsFalse(settings.IsValid);
        }

        [Test]
        public void IsValid_InvalidBounds_InvalidParameter4Bounds()
        {
            var settings = CreateSettings();

            settings.Parameter4Max = settings.Parameter4Min;

            Assert.IsFalse(settings.IsValid);
        }

        [Test]
        public void UseMonteCarlo_IsFalse()
        {
            var settings = CreateSettings();

            settings.Method = GenerationMethod.SimpleRandom;

            Assert.IsFalse(settings.UseMonteCarlo);
        }

        [Test]
        public void UseMonteCarlo_IsTrue()
        {
            var settings = CreateSettings();

            settings.Method = GenerationMethod.MonteCarlo;

            Assert.IsTrue(settings.UseMonteCarlo);
        }

        private static CriteriaSearchSettings CreateSettings()
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
    }
}
