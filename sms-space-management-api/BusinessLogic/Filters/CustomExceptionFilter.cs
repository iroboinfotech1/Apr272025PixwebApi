using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using sms.space.management.api.BusinessLogic.Common;
using sms.space.management.api.BusinessLogic.CustomExceptions;
using sms.space.management.application.Models;
using System.Net;
using System.Text;

namespace sms.space.management.api.BusinessLogic.Filters
{
    public class CustomExceptionFilter : IExceptionFilter
    {

        private readonly IConfiguration _configuration;

        public CustomExceptionFilter(IConfiguration configuration)
        {

            _configuration = configuration;
        }

        public void OnException(ExceptionContext context)
        {
            HttpStatusCode status = HttpStatusCode.InternalServerError;
            ApiErrorResponse errors = new(null, ApiStatusCodes.Error, "Something went wrong");
            try
            {
                var exception = context.Exception;

                status = typeof(ICustomException).IsAssignableFrom(exception.GetType()) ? HttpStatusCode.OK : HttpStatusCode.InternalServerError;
                errors = GetErrors(exception);

                context.ExceptionHandled = true;

                LogInternalServerErrors(exception);

            }
            catch (Exception ex)
            {
                errors = GetErrors(ex);
                LogInternalServerErrors(ex);
            }
            finally
            {
                var response = context.HttpContext.Response;
                response.StatusCode = (int)status;
                response.ContentType = "application/json";
                var responseData = JsonConvert.SerializeObject(errors, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

                response.WriteAsync(responseData, Encoding.UTF8);
            }

        }

        private void LogInternalServerErrors(Exception exception)
        {
            if (exception.GetType() != typeof(ICustomException))
            {
                var location = exception.TargetSite?.DeclaringType?.FullName + "." + exception.TargetSite?.Name;

            }
        }

        private ApiErrorResponse GetErrors(Exception exception)
        {
            if (exception.GetType() == typeof(InputValidationException))
            {
                ICustomException result = (ICustomException)exception;
                return new ApiErrorResponse(result.Errors, ApiStatusCodes.InputValidationFailure, "Input Validation Error");
            }
            if (exception.GetType() == typeof(NotFoundException))
            {
                var result = (ICustomException)exception;
                return new ApiErrorResponse(result.Errors, ApiStatusCodes.NotFound, "No Record Found");
            }
            if (exception.GetType() == typeof(UnableToProcessException))
            {
                var result = (ICustomException)exception;
                return new ApiErrorResponse(result.Errors, ApiStatusCodes.UnableToProcess, "Unable To Process");
            }
            else
            {
                var location = exception.TargetSite?.DeclaringType?.FullName + "." + exception.TargetSite?.Name;
                IDictionary<string, List<string>> errors = new Dictionary<string, List<string>>();
                var errorMessage = "Something went wrong. Please contact administrator";

                if (Convert.ToBoolean(_configuration["Application:ReturnException"]) == true)
                {
                    var error = new { LoggingInDB = false, Message = exception.Message, Location = location, Overview = exception.ToString(), stackTrace = exception.StackTrace, Source = exception.Source };
                    errorMessage = JsonConvert.SerializeObject(error);
                }

                errors.Add(LogConstants.FieldNameError, new List<string> { errorMessage });

                return new ApiErrorResponse(errors, ApiStatusCodes.Error, "Something went wrong");
            }
        }
    }
}
