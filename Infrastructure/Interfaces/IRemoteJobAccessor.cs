using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IRemoteJobAccessor
    {
         public Task<string> PostRemote(string URL, string remoteCommand, string remoteParam);
    }
}