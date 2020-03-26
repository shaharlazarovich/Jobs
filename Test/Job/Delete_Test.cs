using System;
using System.Net;
using System.Threading;
using Application.Errors;
using Application.Jobs;
using MediatR;
using NUnit.Framework;

namespace Test.Jobs
{
    [TestFixture]
    public class Delete_Test: TestBase
    {
        public Delete_Test() {}

        [OneTimeSetUp]
        public void SetUp() {
        var jobCommand = new Create.Command
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
            var handler = new Create.Handler(testContext);
            var result = handler.Handle(jobCommand, CancellationToken.None).Result;
        }

        [Test]
        public void DeleteJob_WithValidJobId_ShouldDeleteJob()
        {
            //Arrange
            var deleteJobCommand = new Delete.Command
            {
                Id = 1
            };
            var deleteHandler = new Delete.Handler(testContext);
            //Act
            var deleteResult = deleteHandler.Handle(deleteJobCommand, CancellationToken.None).Result;
            //Assert
            Assert.NotNull(deleteResult);
            Assert.That(deleteResult.Equals(Unit.Value));
        }

        [Test]
        public void DeleteJob_WithEmptyJobId_ShouldThrowException()
        {
           //Arrange
           var deleteJobCommand = new Delete.Command
            {
                Id = 0
            };
           var handler = new Delete.Handler(testContext);
           //Assert
           var ex = Assert.CatchAsync<RestException>(() => handler.Handle(deleteJobCommand, CancellationToken.None));
           Assert.That(ex.Code, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Ignore("Ignore test")]
        public void DeleteJob_WithWrongJobId_ShouldThrowException()
        {   
            //Arrange         
            var deleteJobCommand = new Delete.Command
            {
                Id = 9999
            };
            
            var deleteHandler = new Delete.Handler(testContext);
            //Assert
            var ex = Assert.CatchAsync<Exception>(() => deleteHandler.Handle(deleteJobCommand, CancellationToken.None));
            if (ex.GetType().Equals(typeof(Exception)))
                Assert.That(ex.Message, Is.EqualTo("problem saving changes"));
        }

        
    }
}