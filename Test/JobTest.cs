using NUnit.Framework;
using Domain;
 
namespace Test
{
    [TestFixture]
    public class JobTest
    {
 
        [Test]
        public void Test_IsCompanyNameEmpty()
        {
            var job = new Domain.Job();
            var result = job.Company;
            if (result==null)
                result = "";
            Assert.AreEqual("", result);
        }
 
        [Ignore("Ignore test")]
        public void Test_To_Ignore()
        {
        }
            
    }
}