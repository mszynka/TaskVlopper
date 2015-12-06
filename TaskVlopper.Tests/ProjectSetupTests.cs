using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Practices.Unity;
using TaskVlopper.Base.Repository;
using TaskVlopper.Base.Model;
using System.Collections.Generic;
using System.Linq;
using TaskVlopper.Base.Logic;
using TaskVlopper.Controllers;
using System.Web.Mvc;
using TaskVlopper.ServiceLocator;

namespace TaskVlopper.Tests
{
    [TestClass]
    public class ProjectSetupTests
    {
        [TestMethod]
        public void SqlAndServiceLocatorInitializationTest()
        {
            //Sql database creation test, and Service locator resolver test
            using (IUnityContainer container = UnityConfig.GetConfiguredContainer())
            {
                var repository = container.Resolve<ITestRepository>();
                Assert.IsNotNull(repository);


            }
        }

        [TestMethod]
        public void SqlAddNameTooShortTest()
        {
            using (IUnityContainer container = UnityConfig.GetConfiguredContainer())
            {
                var repository = container.Resolve<ITestRepository>();
                try
                {
                    Test test = new Test();
                    test.Name = ""; //Can't add when length is 0
                    repository.Add(test);

                    Assert.Fail();
                }
                catch (Exception ex)
                {
                    string debuggerCheck = ex.Message;
                    //Passed 
                }
            }
        }

        [TestMethod]
        public void SqlAddNameTooLongTest()
        {
            using (IUnityContainer container = UnityConfig.GetConfiguredContainer())
            {
                var repository = container.Resolve<ITestRepository>();
                try
                {
                    Test test = new Test();
                    test.Name = "1234567890123456789012345678901"; //Can't add when length is 31
                    repository.Add(test);

                    Assert.Fail();
                }
                catch (Exception ex)
                {
                    string debuggerCheck = ex.Message;
                    //Passed 
                }
            }
        }

        [TestMethod]
        public void SqlAddUniqueDuplicateTest()
        {
            using (IUnityContainer container = UnityConfig.GetConfiguredContainer())
            {
                var repository = container.Resolve<ITestRepository>();
                //Adding test duplicate test
                try
                {
                    Test test = new Test();
                    test.Name = "111 333";
                    repository.Add(test);
                    test.Name = "111 333";
                    repository.Add(test);

                    Assert.Fail();
                }
                catch (Exception ex)
                {
                    string debuggerCheck = ex.Message;
                    //Test passes   
                }

            }
        }

        [TestMethod]
        public void SqlRemoveAllTest()
        {
            using (IUnityContainer container = UnityConfig.GetConfiguredContainer())
            {
                //Removes all elements
                var repository = container.Resolve<ITestRepository>();
                repository.RemoveAll();

                IEnumerable<Test> Tests = repository.GetAll();
                Assert.AreEqual(Tests.Count(), 0);
            }
        }

        [TestMethod]
        public void SqlAddTest()
        {
            using (IUnityContainer container = UnityConfig.GetConfiguredContainer())
            {
                var repository = container.Resolve<ITestRepository>();
                repository.RemoveAll();

                //Adding test
                Test test = new Test();
                test.Name = "1";
                test.Result = 32.5;
                test.Type = Base.Enums.TestTypeEnum.Dos;
                test.Date = DateTime.Now;
                repository.Add(test); // 1

                test.Name = "2";
                repository.Add(test); // 2

                test.Name = "3";
                repository.Add(test); // 3

                test.Name = "4";
                repository.Add(test); // 4
                Assert.AreEqual(repository.GetAll().Count(), 4);
            }
        }

        [TestMethod]
        public void SqlUpdateTest()
        {
            using (IUnityContainer container = UnityConfig.GetConfiguredContainer())
            {
                var repository = container.Resolve<ITestRepository>();
                repository.RemoveAll();

                Test test = new Test();
                test.Name = "2";
                test.Result = 32.5;
                test.Type = Base.Enums.TestTypeEnum.Dos;
                test.Date = DateTime.Now;
                test.ID = 2;
                repository.Add(test); 

                //Updating test, where ID is 2
                
                test = repository.GetAll().Single(x => x.Name == "2");
                test.Result = 123.123;

                repository.Update(test);
                Assert.AreEqual(repository.GetAll().Single(x => x.Name == "2").Result, 123.123);
            }
        }

        [TestMethod]
        public void SqlRemoveTest()
        {
            using (IUnityContainer container = UnityConfig.GetConfiguredContainer())
            {
                var repository = container.Resolve<ITestRepository>();
                repository.RemoveAll();

                Test test = new Test();
                test.Name = "3";
                test.Result = 32.5;
                test.Type = Base.Enums.TestTypeEnum.Dos;
                test.Date = DateTime.Now;
                test.ID = 2;
                repository.Add(test);

                //Deleting Test with ID 3
                test = repository.GetAll().Single(x => x.Name == "3");
                repository.Remove(test);

                //Element now should be null if we try to get ID 3
                test = repository.GetAll().Where(x => x.Name == "3").SingleOrDefault();
                Assert.IsNull(test);
            }
        }


        [TestMethod]
        public void TestLogicAverageCountTest()
        {
            using (IUnityContainer container = UnityConfig.GetConfiguredContainer())
            {
                //Already tested
                AddTests(container);

                //Actual logic test
                var logic = container.Resolve<ITestLogic>();
                double average = logic.GetAverageResult();

                Assert.AreEqual(17.375, average);
            }
        }

        public void AddTests(IUnityContainer container)
        {
            var repository = container.Resolve<ITestRepository>();
            repository.RemoveAll();

            Test test = new Test();
            test.Name = "10";
            test.Result = 32.49;
            repository.Add(test); // 1

            test.Name = "11";
            test.Result = 12.51;
            repository.Add(test); // 2

            test.Name = "12";
            test.Result = 12.25;
            repository.Add(test); // 3

            test.Name = "13";
            test.Result = 12.25;
            repository.Add(test); // 4
        }

        [TestMethod]
        public void TestViewTest()
        {
            // Arrange
            TestController controller = new TestController();

            // Act
            ViewResult result = controller.Test() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void AllRepositoriesTest()
        {
            try
            {
                using (IUnityContainer container = UnityConfig.GetConfiguredContainer())
                {
                    var projectRepo = container.Resolve<IProjectRepository>();
                    var taskRepo = container.Resolve<ITaskRepository>();
                    var workRepo = container.Resolve<IWorklogRepository>();
                    var userTaskAssignmentRepo = container.Resolve<IUserTaskAssignmentRepository>();
                    var userProjectAssignmentRepo = container.Resolve<IUserProjectAssignmentRepository>();
                    var meetingParticipantsRepo = container.Resolve<IMeetingParticipantsRepository>();
                    var meetingRepo = container.Resolve<IMeetingRepository>();

                    projectRepo.RemoveAll();
                    taskRepo.RemoveAll();
                    workRepo.RemoveAll();
                    userTaskAssignmentRepo.RemoveAll();
                    userProjectAssignmentRepo.RemoveAll();
                    meetingParticipantsRepo.RemoveAll();
                    meetingRepo.RemoveAll();
                }
            }
            catch (Exception ex)
            {
                string debuggerMessage = ex.Message;
                Assert.Fail();
            }
        }
    }
}