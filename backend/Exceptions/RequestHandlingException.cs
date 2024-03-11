using System;

namespace backend.Exceptions
{
    public class RequestHandlingException: Exception
    {
        public object Errors { get; }
        public RequestHandlingException(string message, object errors) : base(message)
        {
            Errors = errors;
        }

    }
}
