using System;
using System.Threading.Tasks;
using Infrastructure.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using Persistence;
 
namespace Test
{
    public class TestBase
    {
        private readonly DataContext _context;
        private readonly DataContext _badContext;
        private readonly IMapper _mapper;
        private readonly IUserAccessor _userAccessor;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRemoteJobAccessor _remoteAccessor;
        public TestBase(){
            _context = GetDbContext();
            _badContext = GetBadDbContext();
            var mockUserAccessor = new Mock<IUserAccessor>();
            _userAccessor = mockUserAccessor.Object;
            testContext =_context;
            testBadContext = _badContext;
            testMapper = _mapper;
            testUserAccessor = _userAccessor;
            var mockRemoteAccessor = new Mock<IRemoteJobAccessor>();
            Task<string> expectedResult = Task.FromResult<string>("Successfully Running Job");
            //mockRemoteAccessor.Setup(p => p.PostRemote()).Returns(expectedResult);
            //_remoteAccessor = mockRemoteAccessor.Object;
            testRemote = _remoteAccessor;
        }

        public DataContext testContext {get; set;}
        public DataContext testBadContext {get; set;}
        public IMapper testMapper {get; set;}
        public IUserAccessor testUserAccessor {get; set;}
        public IRemoteJobAccessor testRemote {get;set;}


        private DataContext GetDbContext(bool useSqlite = false)
        {
            var builder = new DbContextOptionsBuilder<DataContext>();
            if (useSqlite)
            {
                builder.UseSqlite("Data Source=:memory", x => { });
            }
            else
            {
                builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            }
            
            var dbContext = new DataContext(builder.Options,_httpContextAccessor);
 
           if (useSqlite)
            {
               dbContext.Database.OpenConnection();
            }
 
            dbContext.Database.EnsureCreated();
 
            return dbContext;
        }

        private DataContext GetBadDbContext(bool useSqlite = true)
        {
            var builder = new DbContextOptionsBuilder<DataContext>();
            if (useSqlite)
            {
                builder.UseSqlite("Data Source=:memory", x => { });
            }
            else
            {
                builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            }
            
            var dbContext = new DataContext(builder.Options,_httpContextAccessor);
 
            return dbContext;
        }
    }
}