using PlannerApp.Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PlannerApp.Client.Services.Exceptions
{
    public class ApiException : Exception
    {
        public ApiErrorRespose ApiErrorResponse { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public ApiException(ApiErrorRespose error, HttpStatusCode statusCode) : this(error)
        {
            StatusCode = statusCode;
        }

        public ApiException(ApiErrorRespose error)
        {
            ApiErrorResponse = error;
        }
    }
}
