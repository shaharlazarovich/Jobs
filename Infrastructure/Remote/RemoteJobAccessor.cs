using System.Net.Http;
using System.Threading.Tasks;
using Infrastructure.Interfaces;
using Microsoft.Extensions.Options;


namespace Infrastructure.Remote
{
    public class RemoteJobAccessor: IRemoteJobAccessor
    {
        private readonly HttpClient _httpClient;  
        private readonly string remoteTokenKey;
  
        public RemoteJobAccessor(IHttpClientFactory clientFactory, IOptions<RemoteJobSettings> config)  
        {  
            _httpClient = clientFactory.CreateClient("HTTPJobs");
            remoteTokenKey = config.Value.RemoteAPIKey;
        }

        public async Task<string> PostRemote(string URL, string remoteCommand, string remoteParam)
        {
            string remoteURL = $"http://{URL}:5876/api/Jobs/{remoteCommand}?jobName={remoteParam}";
            var request = new HttpRequestMessage(
            HttpMethod.Post,
            remoteURL);
            request.Headers.Add("EDR-API-KEY", remoteTokenKey);
            request.Headers.Add("User-Agent", "EDRM");
            request.Headers.Add("EDRM-Correlation-ID", $"{remoteCommand}: {remoteParam}");
            
            var response = await _httpClient.SendAsync(request);
            var retString = await response.Content.ReadAsStringAsync();
            return retString; 
        } 
        
    }
}