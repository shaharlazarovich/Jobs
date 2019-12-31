using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Persistence;

namespace Test.Job
{
    [TestFixture]
    public class Edit_Test
    {
        [Test]
        public async Task TestMethod_WithFactory()
        {
            using (var factory = new SampleDbContextFactory())
            {
            // Get a context
                using (var context = factory.CreateContext())
                {
                    //var user = new User() { Email = "test@sample.com" };
                    //context.Users.Add(user);
                    await context.SaveChangesAsync();
                }

            // Get another context using the same connection
                using (var context = factory.CreateContext())
                {
                    var count = await context.Users.CountAsync();
                    Assert.AreEqual(1, count);

                    var u = await context.Users.FirstOrDefaultAsync(user => user.Email == "test@sample.com");
                    Assert.IsNotNull(u);
                }
            }
        }       
    }
}