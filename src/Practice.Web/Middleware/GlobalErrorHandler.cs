using Newtonsoft.Json;
using Practice.Common.ViewModels;

namespace Practice.Web.Middlewares
{
    public class GlobalErrorHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalErrorHandler> _logger;

        public GlobalErrorHandler(RequestDelegate next, ILogger<GlobalErrorHandler> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"handle error: {ex.Message}, url: {context.Request.Path}");

                await HandleExceptionAsync(context);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context)
        {
            context.Response.StatusCode = StatusCodes.Status200OK;
            context.Response.ContentType = "application/json";

            var responseModel = new ResponseModel(ResponseCode.ERROR);
            await context.Response.WriteAsync(JsonConvert.SerializeObject(responseModel));
        }
    }
}
