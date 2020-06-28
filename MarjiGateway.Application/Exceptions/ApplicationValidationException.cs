using System;

namespace MarjiGateway.Application.Exceptions
{
    public class ApplicationValidationException : Exception
    {
        public ApplicationValidationException()
        {
            
        }

        public ApplicationValidationException(string message) : base(message)
        {
            
        }
    }
}