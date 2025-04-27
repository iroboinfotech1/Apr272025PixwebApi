using sms.space.management.api.BusinessLogic.Common;

namespace sms.space.management.api.BusinessLogic.CustomExceptions;

public class NotFoundException : ApplicationException, ICustomException
{
    public NotFoundException()
    {
        Errors.Add(LogConstants.FieldNameError, new List<string> { "No Records Found" });
    }

    public NotFoundException(string entityName, object key)
    {
        Errors.Add(LogConstants.FieldNameError, new List<string> { $"No {entityName} found for the requested key ({key})" });
    }

    public IDictionary<string, List<string>> Errors { get; set; } = new Dictionary<string, List<string>>();
}
