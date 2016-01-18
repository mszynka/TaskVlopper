using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskVlopper.Base.Repository;

namespace TaskVlopper.Tests
{
    public abstract class TestsBase
    {
        public TestsBase()
        {
            DatabasePicker.PickTestDatabase();
        }
    }
}
