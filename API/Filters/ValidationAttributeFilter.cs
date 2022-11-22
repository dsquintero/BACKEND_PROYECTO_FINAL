using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace API.Filters
{
    public class ValidationAttributeFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                IEnumerable<string> errorMessages = context.ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                int statusCode = (int)HttpStatusCode.UnprocessableEntity;
                context.Result =
                new ObjectResult
                (
                    new
                    {
                        ErrorCode = statusCode,
                        ErrorMessages = errorMessages,
                        //Trace = ex.StackTrace
                    }
                )
                { StatusCode = statusCode };

                //return BadRequest(new ErrorResponse(errorMessages));

                //context.Result = new UnprocessableEntityObjectResult(context.ModelState);
            }
        }
    }
}
