using System;
using System.Net;

namespace Sdk.Core.Exceptions
{
    public class CustomException : Exception
    {
        public CustomException(string message, HttpStatusCode statusCode) : base(message)
        {
            StatusCode = statusCode;
        }

        public HttpStatusCode StatusCode { get; set; }
    }
}
