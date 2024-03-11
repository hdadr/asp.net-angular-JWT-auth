using backend.APIs;
using System;
using System.Collections.Generic;

namespace backend.Exceptions
{
    public class RequestValidationException: Exception
    {
        public IList<RequestHandlingError> Errors { get; }
        public RequestValidationException(string message, IList<RequestHandlingError> errors) : base(message)
        {
            Errors = errors;
        }
    }
}
