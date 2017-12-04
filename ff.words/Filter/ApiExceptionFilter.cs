namespace ff.words.Filter
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            ApiError error;
            Exception exception = context.Exception;

            if (exception is FluentValidation.ValidationException)
            {
                context.Exception = null;
                context.ExceptionHandled = true;
                context.HttpContext.Response.StatusCode = 500;

                error = new ApiError(exception.Message.Replace("Validation failed: \r\n -- ", string.Empty));
            }
            else
            {
                // Unhandled errors
                context.HttpContext.Response.StatusCode = 500;

#if !DEBUG
                var msg = "An unhandled error occurred.";
#else
                var msg = exception.GetBaseException().Message;
                msg += Environment.NewLine + exception.StackTrace;
#endif

                error = new ApiError(msg);
            }

            context.Result = new JsonResult(error);

            base.OnException(context);
        }
    }

    public class ApiError
    {
        public ApiError(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public string ErrorMessage { get; set; }

        public object AdditionalData { get; set; }
    }
}
