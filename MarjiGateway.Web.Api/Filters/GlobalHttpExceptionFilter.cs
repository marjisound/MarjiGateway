using System.Collections.Generic;
using System.Net;
using MarjiGateway.Application.Exceptions;
using MarjiGateway.Application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MarjiGateway.Web.Api.Filters
{
    public class GlobalHttpExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is ModelValidationException exception)
            {
                SetErrorModel(context, HttpStatusCode.BadRequest, exception.Errors); 
            }
            else if (context.Exception is ApplicationValidationException validationException)
            {
                SetErrorModel(
                    context,
                    HttpStatusCode.BadRequest,
                    new List<ErrorModel>(){new ErrorModel(){ErrorMessage = validationException.Message, ErrorCode = "ValidationError"}});
            }
            else if (context.Exception is ApplicationOperationException operationException)
            {
                SetErrorModel(
                    context,
                    HttpStatusCode.InternalServerError,
                    new List<ErrorModel>() { new ErrorModel() { ErrorMessage = operationException.Message, ErrorCode = "OperationError" } });
            }
            else
            {
                var errors = new[]
                {
                    new ErrorModel()
                    {
                        ErrorCode = "UnhandledError",
                        ErrorMessage = "Unhandled error occured.",
                        Level = ErrorLevelModel.Error
                    }
                };
                SetErrorModel(context, HttpStatusCode.InternalServerError, errors);
            }
        }

        private static void SetErrorModel(
            ExceptionContext context,
            HttpStatusCode httpStatusCode,
            IEnumerable<ErrorModel> errors)
        {
            context.Result = new JsonResult(errors)
            {
                StatusCode = (int)httpStatusCode
            };
        }
    }
}