using System.Net;
using System.Text.Json;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using static Api.Helpers.ResponseHelper;

namespace Api.Helpers
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            var response = new ApiResponse<object>
            {
                Success = false,
                Data = null
            };

            // Detectar errores de conexión a SQL Server
            if (ex is SqlException || ex.InnerException is SqlException || ex is DbUpdateException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.ServiceUnavailable; // 503
                response.Message = "No se puede conectar a la base de datos. Intenta más tarde.";
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = ex.Message;
            }

            var json = JsonSerializer.Serialize(response);
            return context.Response.WriteAsync(json);
        }
    }
}
