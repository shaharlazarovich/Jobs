using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IRemoteJobAccessor
    {
         public Task<string> PostRemote(string URL, string remoteCommand, string remoteParam);
    }
}