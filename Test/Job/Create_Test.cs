using NUnit.Framework;
using MediatR;
using Application.Jobs;
using Moq;
using Persistence;
using Microsoft.EntityFrameworkCore;
using Domain;
using System.Threading.Tasks;
using System.Threading;
using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Test.Job
{
    [TestFixture]   
    public class Create_Test
    {
        [Test]
        public async Task HandleCreateJob_WhenAllDataExists_ReturnsValue()
        {
            //Arrange
            var mediator = new Mock<IMediator>();
            using (var factory = new SampleDbContextFactory())
            {
                using (var context = factory.CreateContext())
                {
                    Create.Command command = new Create.Command();
                    Create.Handler handler = new Create.Handler(context);
                    Unit x = await handler.Handle(command, new System.Threading.CancellationToken());
                    mediator.Setup(m => m.Send(It.IsAny<Create.Command>(), It.IsAny<CancellationToken>()));
                }
            }
            //var context = new Mock<DataContext>();
            //var options = new DbContextOptionsBuilder<IdentityDbContext<AppUser>>()
                //.UseSqlite("Data source=reactivities.db")
                //.UseInMemoryDatabase(databaseName: "reactivities")
              //  .UseNpgsql("Host=localhost;Port=5432;Username=Appuser;Password=Pa$$w0rd;Database=Reactivities;")
              //  .Options;
            //var context = new DataContext(options);
            //var entity = context.Model.FindEntityType(typeof(Domain.Job).FullName);
            //var mockModel = new Mock<DbModelBuilder>();
            //context.Add(Entity Jobs);
            //Create.Command command = new Create.Command();
            /*command.Company = "unittest_company";
            command.Servers = "10";
            command.JobName = "unittest_name";
            command.Replication = "unittest_zerto";
            command.Id = new System.Guid();
            command.RTA = 3;
            command.RTONeeded = 30;
            command.LastRun = DateTime.Now;
            command.Key = "AAAA-BBBB-CCCC-DDDD";
            command.Results = "All tested - server is ok";*/
            //Create.Handler handler = new Create.Handler(context);
 
            //Act
            //Unit x = await handler.Handle(command, new System.Threading.CancellationToken());
            
            //Assert
            //mediator.Setup(m => m.Send(It.IsAny<Create.Command>(), It.IsAny<CancellationToken>()));
        }
        
    }
}