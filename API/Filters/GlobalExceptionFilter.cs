using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace API.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            Exception ex = context.Exception;
            string msj = string.Empty;
            if (ex.InnerException != null)
            {
                if (ex.InnerException.Message != null)
                {
                    msj = (ex.InnerException.Message);
                    if (ex.InnerException.InnerException != null)
                    {
                        msj += ex.InnerException.InnerException.Message;
                    }
                }
            }
            else
            {
                msj = (ex.Message);
            }

            int statusCode;

            switch (true)
            {
                case bool _ when ex is UnauthorizedAccessException:
                    statusCode = (int)HttpStatusCode.Unauthorized;
                    break;


                case bool _ when ex is InvalidOperationException:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    break;


                default:
                    statusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            context.Result = 
                new ObjectResult
                (
                    new
                    {
                        ErrorCode = statusCode,
                        Message = msj
                        //,Trace = ex.StackTrace
                    }
                )
                { StatusCode = statusCode };
        }
    }
}
