using System.Collections.Generic;
using System.Linq;

using MCS.Desktop.DataModels;
using MCS.Desktop.ViewModels;

using NUnit.Framework;

namespace MCS.Desktop.Tests.Models
{
    [TestFixture]
    public class CriteriaTests
    {
        [Test]
        public void CalculateCriteriaByParametersSet_CalculateFunctionTest_Positive()
        {
            var criteria = CreateCriteria();
            var points = CreateParametersSets();

            var result = points.First().Parameter1 + points.First().Parameter2 + 
                         points.First().Parameter3 + points.First().Parameter4;

            Assert.AreEqual(criteria.CalculateCriteriaByParametersSet(points).First().Value, result);
        }

        [Test]
        public void CalculateCriteriaByParametersSet_CalculateFunctionTest_Negative()
        {
            var criteria = CreateCriteria();
            var points = CreateParametersSets();

            Assert.AreNotEqual(criteria.CalculateCriteriaByParametersSet(points).First().Value, 0);
        }

        private Criteria CreateCriteria()
        {
            return new Criteria("TestCriteria", set => set.Parameter1 + set.Parameter2 + set.Parameter3 + set.Parameter4);
        }

        private List<ParametersSet> CreateParametersSets()
        {
            return new List<ParametersSet> 
                            {
                                new ParametersSet(1, 1, 1, 1)
                            };
        }
    }
}
