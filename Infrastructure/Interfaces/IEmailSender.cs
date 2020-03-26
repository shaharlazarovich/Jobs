using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IEmailSender // don't forget the public modifier
    {
       //void Send(string toAddress, string subject, string body, bool sendAsync = true);
       Task<string> Send(string toAddress, string subject, string body, bool sendAsync = true);
       
    }
}