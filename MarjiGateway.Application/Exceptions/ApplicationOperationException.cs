using System;
using System.Reflection;

namespace MarjiGateway.Application.Exceptions
{
    public class ApplicationOperationException : Exception
    {
        public ApplicationOperationException() : base()
        {
            
        }

        public ApplicationOperationException(string message) : base(message)
        {
            
        }
    }
}