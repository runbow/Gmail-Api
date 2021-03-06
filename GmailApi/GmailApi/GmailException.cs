using System;
using System.Collections.Generic;
using System.Net;
using GmailApi.Models;

namespace GmailApi
{
    public class GmailException : Exception
    {
        public HttpStatusCode StatusCode { get; private set; }
        public List<Error> Errors { get; set; }

        public GmailException(ErrorResponse errorResponse, Exception innerException)
            : base(ConstructMessage(errorResponse), innerException)
        {
            StatusCode = (HttpStatusCode)errorResponse.Code;
            Errors = errorResponse.Errors;
        }

        public GmailException(ErrorResponse errorResponse)
            : this(errorResponse, null)
        {
        }

        public GmailException(HttpStatusCode statusCode, string message, Exception innerException)
            : base(ConstructMessage(statusCode, message), innerException)
        {
        }

        public GmailException(HttpStatusCode statusCode, string message)
            : this(statusCode, message, null)
        {
        }

        private static string ConstructMessage(ErrorResponse errorResponse)
        {
            return string.Concat(errorResponse.Code, ": ", errorResponse.Message);
        }

        private static string ConstructMessage(HttpStatusCode statusCode, string message)
        {
            return string.Concat(statusCode, ":", message);
        }
    }
}