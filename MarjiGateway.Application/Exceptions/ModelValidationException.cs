using System;
using System.Collections.Generic;
using MarjiGateway.Application.Models;

namespace MarjiGateway.Application.Exceptions
{
    public class ModelValidationException : Exception
    {
        public IEnumerable<ErrorModel> Errors { get; }
        public ModelValidationException(IEnumerable<ErrorModel> errors, string message = null)
        {
            Errors = errors;
        }
    }
}