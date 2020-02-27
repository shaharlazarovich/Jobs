using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace API.Middleware
{
    //this class logs the start and end of each request going through the pipeline- and logs this time to our logger
    public class TimingHandler : DelegatingHandler
{
    private readonly ILogger<TimingHandler> _logger;

    public TimingHandler(ILogger<TimingHandler> logger)
    {
        _logger = logger;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, 
        CancellationToken cancellationToken)
    {
        var sw = Stopwatch.StartNew();

        _logger.LogInformation("Starting request");

        var response = await base.SendAsync(request, cancellationToken);

        _logger.LogInformation($"Finished request in {sw.ElapsedMilliseconds}ms");

        return response;
    }
}
}