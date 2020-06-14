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
            else
            {
                var errors = new[]
                {
                    new ErrorModel()
                    {
                        ErrorMessage = context.Exception.Message,
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