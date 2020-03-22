using System;
using System.Threading;
using Application.Jobs;
using NUnit.Framework;
using MediatR;

namespace Test.Jobs
{
    [TestFixture]
    public class Create_Test : TestBase
    {
        public Create_Test() {}
 
        private Create.Command _jobCommand;

        [SetUp]
        public void SetUp() {
            _jobCommand = new Create.Command
            {
                JobName = "TestJob1",
                Company = "Netapp",
                Replication = "Zerto",
                Servers = "7",
                LastRun = DateTime.Now,
                RTA = "10",
                Results = "OK",
                Key = "AAAA-BBBB-CCCC-DDDD",
                RTONeeded = "20",
            };
        }

        [TearDown]
        public void tearDown(){
            _jobCommand = null;
        }

        [Test]
        public void CreateJob_WithValidInput_ShouldCreateJob()
        {
            
            //Arrange
            var handler = new Create.Handler(testContext);
            //Act
            var result = handler.Handle(_jobCommand, CancellationToken.None).Result;
            //Assert
            Assert.NotNull(result);
            Assert.That(result.Equals(Unit.Value));
        }

        [Ignore("Ignore test")]
        public void CreateJob_WhenJobIdExists_ShouldThrowException()
        {
            //Arrange
           var handler = new Create.Handler(testContext);
           //Act
           var result = handler.Handle(_jobCommand, CancellationToken.None).Result;
           //Assert
           var ex = Assert.CatchAsync<Exception>(() => handler.Handle(_jobCommand, CancellationToken.None));
           if (ex.Equals(typeof(Exception)))
               Assert.That(ex.Message, Is.EqualTo("problem saving changes"));
        }

    }
}