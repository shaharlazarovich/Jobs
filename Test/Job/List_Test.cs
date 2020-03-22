using System;
using System.Threading;
using Application.Jobs;
using AutoMapper;
using NUnit.Framework;

namespace Test.Jobs
{
    [TestFixture]
    public class List_Test: TestBase
    {
        public List_Test()
        {
            var mockMapper = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); });
            testMapper = mockMapper.CreateMapper();
        }

        [OneTimeSetUp]
        public void setUp(){
            //first we'll create a couple of new jobactions - as our db is currently empty
            var jobCommand1 = new Create.Command
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
            
            var handler1 = new Create.Handler(testContext);
            var result = handler1.Handle(jobCommand1, CancellationToken.None).Result;

            var jobCommand2 = new Create.Command
            {
                JobName = "TestJob2",
                Company = "EMC",
                Replication = "Veeam",
                Servers = "30",
                LastRun = DateTime.Now,
                RTA = "20",
                Results = "OK",
                Key = "AAAA-BBBB-CCCC-DDDD",
                RTONeeded = "30"
            };
            
            var handler2 = new Create.Handler(testContext);
            result = handler2.Handle(jobCommand2, CancellationToken.None).Result;
        }

        [Test]
        [TestCase(5,0)]
        [TestCase(2,0)]
        [TestCase(100,0)]
        public void GetJobsList_ProvidingLimitAndOffset_ShouldGetJobsListAccordingToLimitAndOffset(int limit, int offset)
        {
            //Arrange
            var query = new Application.Jobs.List.Query(limit,offset,new DateTime(2019,1,31,17,59,26,548));//use a year ago date to get correct query
            var listHandler = new Application.Jobs.List.Handler(testContext,testMapper,testUserAccessor);
            //Act
            var listResult = listHandler.Handle(query,CancellationToken.None).Result;
            //Assert
            Assert.NotNull(listResult);
            Assert.AreEqual(listResult.JobsCount,2);
            Assert.That(listResult.Jobs[0].JobName,Is.EqualTo("TestJob1"));
            Assert.That(listResult.Jobs[0].Company,Is.EqualTo("Netapp"));
            Assert.That(listResult.Jobs[0].Replication,Is.EqualTo("Zerto"));
            Assert.That(listResult.Jobs[0].Servers,Is.EqualTo("7"));
            Assert.That(listResult.Jobs[0].RTA,Is.EqualTo("10"));
            Assert.That(listResult.Jobs[0].Results,Is.EqualTo("OK"));
            Assert.That(listResult.Jobs[0].Key,Is.EqualTo("AAAA-BBBB-CCCC-DDDD"));
            Assert.That(listResult.Jobs[0].RTONeeded,Is.EqualTo("20"));
            Assert.That(listResult.Jobs[1].JobName,Is.EqualTo("TestJob2"));
            Assert.That(listResult.Jobs[1].Company,Is.EqualTo("EMC"));
            Assert.That(listResult.Jobs[1].Replication,Is.EqualTo("Veeam"));
            Assert.That(listResult.Jobs[1].Servers,Is.EqualTo("30"));
            Assert.That(listResult.Jobs[1].RTA,Is.EqualTo("20"));
            Assert.That(listResult.Jobs[1].Results,Is.EqualTo("OK"));
            Assert.That(listResult.Jobs[1].Key,Is.EqualTo("AAAA-BBBB-CCCC-DDDD"));
            Assert.That(listResult.Jobs[1].RTONeeded,Is.EqualTo("30"));
        }

        [Test]
        [TestCase(0,0)]
        [TestCase(1,3)]
        [TestCase(-1,0)]
        public void GetJobsList_ProvidingWrongLimitAndOffset_ShouldGetZeroJobs(int limit, int offset)
        {
            //Arrange
            var query = new Application.Jobs.List.Query(limit,offset,new DateTime(2019,1,31,17,59,26,548));//use a year ago date to get correct query
            var listHandler = new Application.Jobs.List.Handler(testContext,testMapper,testUserAccessor);
            //Act
            var listResult = listHandler.Handle(query,CancellationToken.None).Result;
            //Assert
            Assert.NotNull(listResult);
            Assert.AreEqual(listResult.JobsCount,0);
        }
        
    }
}