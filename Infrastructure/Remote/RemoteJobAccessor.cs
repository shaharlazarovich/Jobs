using System.Net.Http;
using System.Threading.Tasks;
using Application.Interfaces;

namespace Infrastructure.Remote
{
    public class RemoteJobAccessor: IRemoteJobAccessor
    {
        private readonly HttpClient _httpClient;  
  
        public RemoteJobAccessor(IHttpClientFactory clientFactory)  
        {  
            _httpClient = clientFactory.CreateClient("HTTPJobs");
        }

        public async Task<string> PostRemote(string URL, string remoteCommand, string remoteParam)
        {
            string remoteURL = $"{URL}{remoteCommand}?jobname={remoteParam}";
            var request = new HttpRequestMessage(
             HttpMethod.Post,
             remoteURL); 
    //      request.Headers.Add("Accept", "application/api");
    //      request.Headers.Add("User-Agent", "YourApp");
            var response = await _httpClient.SendAsync(request);
            var retString = await response.Content.ReadAsStringAsync();
            return retString; 
        } 
        
    }
}