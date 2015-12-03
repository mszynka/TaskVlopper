using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskVlopper.Base.Logic;
using TaskVlopper.Base.Model;
using TaskVlopper.Base.Repository;
using TaskVlopper.ServiceLocator;

namespace TaskVlopper.Models
{
    public class TestViewModels
    {
        public TestViewModels(IList<Test> tests, double averageResult)
        {
            Tests = tests;
            AverageResult = averageResult;
        }

        public IList<Test> Tests { get; private set; }
        public double AverageResult { get; private set; }
    }
}