using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace API.Middleware
{
    public class ValidateHeaderHandler : DelegatingHandler
{
    //this class tests every request through the pipieline and verifies the existence of API-KEY (and blocks it otherwise)
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        if (!request.Headers.Contains("EDR-API-KEY"))
        {
            return new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent("You must supply an API key header called EDR-API-KEY")
            };
        }
        else {
            if (request.Headers.TryGetValues("EDR-API-KEY", out var values))
            {
                var remoteKey = values.First();
                if (!remoteKey.Contains("super secret key"))
                {
                    return new HttpResponseMessage(HttpStatusCode.BadRequest)
                    {
                        Content = new StringContent("Your API key header is wrong")
                    };
                }
                
            }
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
}