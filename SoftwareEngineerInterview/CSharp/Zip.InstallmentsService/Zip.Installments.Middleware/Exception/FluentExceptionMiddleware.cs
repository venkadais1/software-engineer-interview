using FluentValidation;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;
using System.Security.Authentication;
using Zip.Installments.Validations.Exception;
using Zip.InstallmentsService.Interface;

namespace Zip.Installments.Middleware.Exceptions
{
    public class FluentExceptionMiddleware
    {
        private const string JsonContentType = "application/json";
        private readonly RequestDelegate request;
        private readonly INLogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="FluentExceptionMiddleware"/>.
        /// </summary>
        /// <param name="next">The next.</param>
        public FluentExceptionMiddleware(RequestDelegate next, INLogger logger)
        {
            this.request = next;
            this.logger = logger;
        }

        /// <summary>
        /// Invokes the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public Task Invoke(HttpContext context) => this.InvokeAsync(context);

        async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await this.request(context);
            }
            catch (Exception exception)
            {
                var httpStatusCode = ConfigurateExceptionTypes(exception);

                context.Response.StatusCode = httpStatusCode;
                context.Response.ContentType = JsonContentType;

                await context.Response.WriteAsync(
                    JsonConvert.SerializeObject(new ErrorModelViewModel
                    {
                        StatusCode = httpStatusCode,
                        Message = exception.Message
                    }));
            }
        }

        /// <summary>
        /// Configurations/maps exception to the proper HTTP error Type
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <returns></returns>
        private int ConfigurateExceptionTypes(Exception exception)
        {
            int httpStatusCode = default;

            switch (exception)
            {
                case var _ when exception is ValidationException:
                    httpStatusCode = GetStatusCode(HttpStatusCode.BadRequest, exception);
                    break;
                case var _ when exception is ArgumentException:
                    httpStatusCode = GetStatusCode(HttpStatusCode.BadRequest, exception);
                    break;
                case var _ when exception is AuthenticationException:
                    httpStatusCode = GetStatusCode(HttpStatusCode.Unauthorized, exception);
                    break;
                case var _ when exception is UnauthorizedAccessException:
                    httpStatusCode = GetStatusCode(HttpStatusCode.Forbidden, exception);
                    break;
                case var _ when exception is InvalidOperationException:
                    httpStatusCode = GetStatusCode(HttpStatusCode.BadRequest, exception);
                    break;
                case var _ when exception is KeyNotFoundException:
                    httpStatusCode = GetStatusCode(HttpStatusCode.NotFound, exception);
                    break;
                case var _ when exception is InvalidDataException:
                    httpStatusCode = GetStatusCode(HttpStatusCode.Conflict, exception);
                    break;
                default:
                    httpStatusCode = GetStatusCode(HttpStatusCode.InternalServerError, exception);
                    break;
            };
            return httpStatusCode;
        }

        private int GetStatusCode(HttpStatusCode httpStatusCode, object ex)
        {
            this.logger.LogError($"Error Code:{httpStatusCode}:{ex}");
            return (int)httpStatusCode;
        }

    }
}
