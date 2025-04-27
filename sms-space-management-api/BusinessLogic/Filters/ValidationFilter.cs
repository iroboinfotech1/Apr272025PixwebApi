using Microsoft.AspNetCore.Mvc.Filters;

namespace sms.space.management.api.BusinessLogic.Filters;

public class ValidationFilter : IActionFilter
{
    public void OnActionExecuted(ActionExecutedContext context)
    {

    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        //if (!context.ModelState.IsValid)
        //{
        //    IDictionary<string, List<string>> errors;
        //    errors = context
        //                    .ModelState
        //                    .Keys
        //                    .ToDictionary(k => k, k => context.ModelState[k]!.Errors.Select(x => x.ErrorMessage).ToList());
        //    //?? new Dictionary<string, List<string>>() { { "error", new List<string>() { "Validation Failed" } } };

        //    throw new CustomExceptions.InputValidationException(errors);
        //}
    }
}
