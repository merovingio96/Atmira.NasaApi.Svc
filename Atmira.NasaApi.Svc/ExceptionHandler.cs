using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;

namespace Atmira.NasaApi.Svc
{
    public class ExceptionHandler
    {
        public static async Task Run(HttpContext context, bool isDevelopment = false)
        {
            IExceptionHandlerFeature exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();
            int statusCode = CheckStatusCode(exceptionFeature);
            context.Response.Clear();
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            object objResponse;

            if (isDevelopment)
            {
                objResponse = new
                {
                    ErrorMessage = exceptionFeature.Error?.Message,
                    StatusCode = statusCode,
                    exceptionFeature.Error?.StackTrace
                };
            }
            else
            {
                objResponse = new
                {
                    ErrorMessage = exceptionFeature.Error?.Message,
                    StatusCode = statusCode
                };
            }

            await context.Response.WriteAsync(JsonConvert.SerializeObject(objResponse)).ConfigureAwait(false);
        }

        private static int CheckStatusCode(IExceptionHandlerFeature exceptionFeature)
        {
            int statusCode;
            switch (exceptionFeature.Error)
            {
                case NotImplementedException _:
                    statusCode = (int)HttpStatusCode.NotImplemented;
                    break;
                case ValidationException _:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case KeyNotFoundException _:
                    statusCode = (int)HttpStatusCode.NotFound;
                    break;
                case UnauthorizedAccessException _:
                    statusCode = (int)HttpStatusCode.Forbidden;
                    break;
                case InvalidOperationException _:
                    statusCode = (int)HttpStatusCode.Conflict;
                    break;
                default:
                    statusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }
            return statusCode;
        }
    }

    public class ErrorDto
    {
        public string ErrorMessage { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
