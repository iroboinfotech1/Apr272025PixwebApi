using FluentValidation.Results;
using sms.space.management.api.BusinessLogic.Common;

namespace sms.space.management.api.BusinessLogic.CustomExceptions;

public class InputValidationException : ApplicationException, ICustomException
{
    public InputValidationException()
    {
        Errors.Add(LogConstants.FieldNameError, new List<string> { "One or more validation failures have occurred." });
    }

    public InputValidationException(IEnumerable<ValidationFailure> failures)
    {
        Errors = failures.GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToList());
    }

    public InputValidationException(IDictionary<string, List<string>> errors)
    {
        if (errors == null)
        {
            Errors.Add(LogConstants.FieldNameError, new List<string> { "One or more validation failures have occurred." });
            return;
        }

        foreach (var item in errors)
        {
            Errors.Add(item.Key, item.Value);
        }


    }

    public InputValidationException(IDictionary<string, string> errors)
    {

        foreach (var item in errors)
        {
            Errors.Add(item.Key, new List<string> { item.Value });
        }
    }

    public IDictionary<string, List<string>> Errors { get; set; } = new Dictionary<string, List<string>>();

}
