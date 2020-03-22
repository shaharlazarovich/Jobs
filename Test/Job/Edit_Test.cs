using System;
using System.Threading;
using Application.Jobs;
using AutoMapper;
using MediatR;
using NUnit.Framework;

namespace Test.Jobs
{
    [TestFixture]
    public class Edit_Test: TestBase
    {
        public Edit_Test()
        {
            var mockMapper = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); });
            testMapper = mockMapper.CreateMapper();
        }

        private Edit.Command _jobEditCommand;
        
        [OneTimeSetUp]
        public void SetUp(){
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

            _jobEditCommand = new Edit.Command
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
        [Test]
        public void EditJob_WithValidInput_ShouldEditJob()
        {   
            //Arrange
            var handler = new Edit.Handler(testContext);
            //Act
            var result = handler.Handle(_jobEditCommand, CancellationToken.None).Result;
            //Assert
            Assert.NotNull(result);
            Assert.That(result.Equals(Unit.Value));
        }

        [Ignore("Ignore test")]
        public void EditJob_WithEmptyJobId_ShouldThrowException()
        {
            //Arrange
            _jobEditCommand.Id = 0;
            var handler = new Edit.Handler(testContext);   
            //Act+Assert
            var ex = Assert.CatchAsync<Exception>(() => handler.Handle(_jobEditCommand, CancellationToken.None));
            if (ex.Equals(typeof(Exception)))
                Assert.That(ex.Message, Is.EqualTo("problem saving changes"));
        }
    }
}