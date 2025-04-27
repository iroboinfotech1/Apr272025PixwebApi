using sms.space.management.api.BusinessLogic.Common;

namespace sms.space.management.api.BusinessLogic.CustomExceptions;

public class UnableToProcessException : ApplicationException, ICustomException
{
    public UnableToProcessException()
    {
        Errors.Add(LogConstants.FieldNameError, new List<string> { "Unable To Process" });
    }

    public UnableToProcessException(string reason)
    {
        Errors.Add(LogConstants.FieldNameError, new List<string> { reason });
    }

    public IDictionary<string, List<string>> Errors { get; set; } = new Dictionary<string, List<string>>();
}
