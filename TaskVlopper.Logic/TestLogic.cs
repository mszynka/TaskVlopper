using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskVlopper.Base.Logic;
using TaskVlopper.Base.Model;
using TaskVlopper.Base.Repository;

namespace TaskVlopper.Logic
{
    public class TestLogic : ITestLogic
    {
        private readonly ITestRepository TestRepository;
        public TestLogic(ITestRepository testRepository)
        {
            TestRepository = testRepository;
        }

        public double GetAverageResult()
        {
            IEnumerable<Test> Tests = TestRepository.GetAll();
            double result = 0;

            foreach (var test in Tests)
                result += test.Result;

            return result / Tests.Count();
        }
    }
}
