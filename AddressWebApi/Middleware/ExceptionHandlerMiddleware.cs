using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace AddressWebApi.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _Next;
        private readonly ILogger<ExceptionHandlerMiddleware> _Logger;
        public ExceptionHandlerMiddleware(RequestDelegate Next, ILogger<ExceptionHandlerMiddleware> Logger)
        {
            _Next = Next;
            _Logger = Logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _Next.Invoke(httpContext);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex.Message);
            }
        }
    }
}
