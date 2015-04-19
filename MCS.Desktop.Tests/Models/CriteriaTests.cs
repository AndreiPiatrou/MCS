using System.Collections.Generic;
using System.Linq;

using MCS.Desktop.DataModels;
using MCS.Desktop.ViewModels;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MCS.Desktop.Tests.Models
{
    [TestClass]
    public class CriteriaTests
    {
        private Criteria criteria;
        private List<ParametersSet> points;

        [TestMethod]
        public void CalculateCriteriaByParametersSet_CalculateFunctionTest_Positive()
        {
            var result = points.First().Parameter1 + points.First().Parameter2 + 
                         points.First().Parameter3 + points.First().Parameter4;
            Assert.AreEqual(criteria.CalculateCriteriaByParametersSet(points).First().Value, result);
        }

        [TestMethod]
        public void CalculateCriteriaByParametersSet_CalculateFunctionTest_Negative()
        {
            Assert.AreNotEqual(criteria.CalculateCriteriaByParametersSet(points).First().Value, 0);
        }

        [TestInitialize]
        public void Setup()
        {
            criteria = new Criteria("TestCriteria", set => set.Parameter1 + set.Parameter2 + set.Parameter3 + set.Parameter4);
            points = new List<ParametersSet> 
                            {
                                new ParametersSet(1, 1, 1, 1)
                            };
        }
    }
}
