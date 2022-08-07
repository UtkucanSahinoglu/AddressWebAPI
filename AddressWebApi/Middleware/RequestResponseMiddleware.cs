using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AddressWebApi.Middleware
{
    public class RequestResponseMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestResponseMiddleware> _logger;

        public RequestResponseMiddleware(RequestDelegate Next, ILogger<RequestResponseMiddleware> logger)
        {
            _next = Next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            //Request
            //httpContext.Request.

            //orginal stream yedek
            var originalBodyStream = httpContext.Request.Body;

            _logger.LogInformation($"Query Keys: {httpContext.Request.QueryString}");

            MemoryStream requestBody = new MemoryStream();
            await httpContext.Request.Body.CopyToAsync(requestBody);
            requestBody.Seek(0, SeekOrigin.Begin);
            string requestText = await new StreamReader(requestBody).ReadToEndAsync();
            requestBody.Seek(0,SeekOrigin.Begin);

            var tempStream = new MemoryStream();
            httpContext.Response.Body = tempStream;

            await _next.Invoke(httpContext); //response oluştu


            httpContext.Response.Body.Seek(0, SeekOrigin.Begin);
            string responseText = await new StreamReader(httpContext.Response.Body,Encoding.UTF8).ReadToEndAsync();
            httpContext.Response.Body.Seek(0, SeekOrigin.Begin);

            await httpContext.Request.Body.CopyToAsync(originalBodyStream);

            _logger.LogInformation($"Request: {requestText}");
            _logger.LogInformation($"Response: {responseText}");

        }
    }
}
