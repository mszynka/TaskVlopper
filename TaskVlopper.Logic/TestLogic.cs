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
            IQueryable<Test> Tests = TestRepository.GetAll();

            return Tests.Sum(x => x.Result) / Tests.Count();
        }
    }
}
