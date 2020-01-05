using System;
using System.Linq;
using System.Threading;
using Application.Jobs;
using Application.Interfaces;
using AutoMapper;
using Domain;
using Moq;
using NUnit.Framework;
 
namespace Application.Tests.Jobs
{
    [TestFixture]
    public class CreateTest : TestBase
    {
        private readonly IMapper _mapper;
 
        public CreateTest()
        {
            var mockMapper = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); });
            _mapper = mockMapper.CreateMapper();
        }
 
        [Test]
        public void Should_Create_Activity()
        {
            var userAccessor = new Mock<IUserAccessor>();
            userAccessor.Setup(u => u.GetCurrentUsername()).Returns("test");
            
            var context = GetDbContext();
 
            context.Users.AddAsync(new AppUser
            {
                Id = "1",
                Email = "test@test.com",
                UserName = "test"
            });
            context.SaveChangesAsync();
 
            var jobCommand = new Create.Command
            {
                Id = new Guid(),
                JobName = "TestJob1",
                Company = "Netapp",
                Replication = "Zerto",
                Servers = "7",
                LastRun = DateTime.Now,
                RTA = 10,
                Results = "OK",
                Key = "AAAA-BBBB-CCCC-DDDD",
                RTONeeded = 20,
            };
            
            var sut = new Create.Handler(context);
            var result = sut.Handle(jobCommand, CancellationToken.None).Result;
            
            Assert.NotNull(result);
            Assert.That(result,Is.EqualTo("TestJob1"));
        }
    }
}