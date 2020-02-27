using System;
using Microsoft.Extensions.Http;
using Microsoft.Extensions.Http.Logging;
using Microsoft.Extensions.Logging;

namespace API.Middleware
{

//this class allows us to create logging filters for everything going through the pipeline
public class HTTPLoggingFilter : IHttpMessageHandlerBuilderFilter
{
    private readonly ILoggerFactory _loggerFactory;

    public HTTPLoggingFilter(ILoggerFactory loggerFactory)
    {
        _loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
    }

    public Action<HttpMessageHandlerBuilder> Configure(Action<HttpMessageHandlerBuilder> next)
    {
        if (next == null)
        {
            throw new ArgumentNullException(nameof(next));
        }

        return (builder) =>
        {
            // Run other configuration first, we want to decorate.
            next(builder);

            // We want all of our logging message to show up as-if they are coming from HttpClient,
            // but also to include the name of the client for more fine-grained control
            var outerLogger =
                _loggerFactory.CreateLogger($"System.Net.Http.HttpClient.{builder.Name}.LogicalHandler");

            var innerLogger = 
                _loggerFactory.CreateLogger($"System.Net.Http.HttpClient.{builder.Name}.ClientHandler");

            // The 'scope' handler goes first so it can surround everything.
            builder.AdditionalHandlers.Insert(0, new CustomLoggingScopeHttpMessageHandler(outerLogger));

            // We want this handler to be last so we can log details about the request after
            // service discovery and security happen.
            builder.AdditionalHandlers.Add(new LoggingHttpMessageHandler(innerLogger));
        };
    }
}
}