using System;
using System.Threading;
using Application.Errors;
using Application.Jobs;
using AutoMapper;
using NUnit.Framework;

namespace Test.Jobs
{
    [TestFixture]
    public class Details_Test: TestBase
    {
        public Details_Test()
        {
            var mockMapper = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); });
            testMapper = mockMapper.CreateMapper();
        }

        [OneTimeSetUp]
        public void SetUp(){
                        //first we'll create a new jobaction - as our db is currently empty
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
                RTONeeded = "20"
            };
            
            var handler = new Create.Handler(testContext);
            var result = handler.Handle(jobCommand, CancellationToken.None).Result;
        }

        [Test]
        public void GetJobDetails_WithValidJobId_ShouldGetJobDetails()
        {
            //Arrange
            var query = new Details.Query{ Id = 1};
            var detailsHandler = new Details.Handler(testContext,testMapper);
            //Act
            var detailsResult = detailsHandler.Handle(query,CancellationToken.None).Result;
            //Assert
            Assert.NotNull(detailsResult);
            Assert.That(detailsResult.JobName,Is.EqualTo("TestJob1"));
            Assert.That(detailsResult.Company,Is.EqualTo("Netapp"));
            Assert.That(detailsResult.Replication,Is.EqualTo("Zerto"));
            Assert.That(detailsResult.Servers,Is.EqualTo("7"));
            Assert.That(detailsResult.RTA,Is.EqualTo("10"));
            Assert.That(detailsResult.Results,Is.EqualTo("OK"));
            Assert.That(detailsResult.Key,Is.EqualTo("AAAA-BBBB-CCCC-DDDD"));
            Assert.That(detailsResult.RTONeeded,Is.EqualTo("20"));
        }

        [Test]
        public void GetJobDetails_WithEmptyJobId_ShouldThrowException()
        {
           //Arrange
           var query = new Details.Query{ Id = 0};
           var handler = new Details.Handler(testContext,testMapper);
            //Assert
           var ex = Assert.CatchAsync<Exception>(() => handler.Handle(query,CancellationToken.None));
           if (ex.Equals(typeof(RestException)))
               Assert.That(ex.Message, Is.EqualTo("Not Found"));
        }
        
    }
}