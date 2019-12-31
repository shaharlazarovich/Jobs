using System;
using System.Net;
using System.Threading.Tasks;
using Application.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace API.Middleware
{
    //the middleware class basically creates a pipeline of actions
    //between the reques and response, and either we perform an action,
    //or move on to the next part in the pipeline.
    //in our case - we will either do "next" or create our own customized
    //error message with separation between rest error and server error
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;
        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _logger = logger;
            _next = next;

        }

        public async Task Invoke(HttpContext context){
            try {
                await _next(context);
            } catch(Exception ex){
                await HandleExceptionAsync(context, ex, _logger);
            }
        }

        private async Task HandleExceptionAsync(
            HttpContext context, 
            Exception ex, 
            ILogger<ErrorHandlingMiddleware> logger)
        {
            object errors = null;

            switch(ex) {
               case RestException re:  
                    logger.LogError(ex, "REST ERROR");
                    errors = re.Errors;
                    context.Response.StatusCode = (int)re.Code;
                    break;
               case Exception e: 
                    logger.LogError(ex, "Server Error");
                    errors = string.IsNullOrWhiteSpace(e.Message) ? "Error" : e.Message;
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            context.Response.ContentType = "application/json";
            if (errors!=null){
                var result = JsonConvert.SerializeObject(new {
                    errors
                });

                await context.Response.WriteAsync(result);
            }
            
        }
    }
}