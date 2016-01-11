using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskVlopper.Base;
using TaskVlopper.Base.Enums;
using TaskVlopper.Repository.Base;
using TaskVlopper.ServiceLocator;

namespace TaskVlopper.Tests
{
    public class BaseSerializerTestModel : IBaseModel
    {
        public int ID { get; set; }

        public int intTest { get; set; }
        public double doubleTest { get; set; }
        public string stringTest { get; set; }
        public DateTime dateTimeTest { get; set; }

        public int? nullableIntTest { get; set; }
        public double? nullableDoubleTest { get; set; }
        public DateTime? nullableDateTimeTest { get; set; }

        public TaskStatusEnum taskStatusEnumTest { get; set; }
        public TaskStatusEnum? nullableTaskStatusEnumTest { get; set; }

        public enum PitbullEnum
        {
            uno, dos, tres, quatro
        }

        public PitbullEnum pitbullEnumTest { get; set; }
        public PitbullEnum? nullablePitbullEnumTest { get; set; }


    }

    public interface IBaseSerializerTests : IBaseSerializer<BaseSerializerTestModel>
    {

    }

    [TestClass]
    public class BaseSerializerTests : BaseSerializer<BaseSerializerTestModel>, IBaseSerializerTests
    {


        [TestMethod]
        public void TestVariousTypes()
        {
            NameValueCollection someTestForm = new NameValueCollection();
            someTestForm.Add("BaseSerializerTestModel.intTest", "1");
            someTestForm.Add("BaseSerializerTestModel.nullableIntTest", null);
            someTestForm.Add("BaseSerializerTestModel.doubleTest", "2.5");
            someTestForm.Add("BaseSerializerTestModel.nullableDoubleTest", null);
            someTestForm.Add("BaseSerializerTestModel.dateTimeTest", new DateTime(2000, 12, 12).ToString());
            someTestForm.Add("BaseSerializerTestModel.nullableDateTimeTest", "");
            someTestForm.Add("BaseSerializerTestModel.taskStatusEnumTest", TaskStatusEnum.Removed.ToString());
            someTestForm.Add("BaseSerializerTestModel.nullableTaskStatusEnumTest", "");
            someTestForm.Add("BaseSerializerTestModel.pitbullEnumTest", BaseSerializerTestModel.PitbullEnum.tres.ToString());
            someTestForm.Add("BaseSerializerTestModel.nullablePitbullEnumTest", BaseSerializerTestModel.PitbullEnum.quatro.ToString());

            using (IUnityContainer container = UnityConfig.GetConfiguredContainer())
            {
                container.RegisterType<IBaseSerializerTests, BaseSerializerTests>();
                var repository = container.Resolve<IBaseSerializerTests>();
                var test = repository.Serialize(someTestForm);

                Assert.AreEqual(1, test.intTest);
                Assert.AreEqual(null, test.nullableIntTest);
                Assert.AreEqual(2.5, test.doubleTest);
                Assert.AreEqual(null, test.nullableDoubleTest);
                Assert.AreEqual(new DateTime(2000, 12, 12), test.dateTimeTest);
                Assert.AreEqual(null, test.nullableDateTimeTest);
                Assert.AreEqual(TaskStatusEnum.Removed, test.taskStatusEnumTest);
                Assert.AreEqual(null, test.nullableTaskStatusEnumTest);
                Assert.AreEqual(BaseSerializerTestModel.PitbullEnum.tres, test.pitbullEnumTest);
                Assert.AreEqual(BaseSerializerTestModel.PitbullEnum.quatro, test.nullablePitbullEnumTest);
            }


        }
    }
}
