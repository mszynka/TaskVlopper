using System.Collections.Generic;
using System.Linq;
using TaskVlopper.Base.Model;
using TaskVlopper.Base.Repository;
using TaskVlopper.Repository.Base;


namespace TaskVlopper.Repository
{
    public class TestRepository : BaseRepository<Test>, ITestRepository
    {
        public IEnumerable<Test> GetAllThatAverageIsNotEqualToZero()
        {
            return this.GetAll().Where(x => x.Result != 0);
        }
    }
}
