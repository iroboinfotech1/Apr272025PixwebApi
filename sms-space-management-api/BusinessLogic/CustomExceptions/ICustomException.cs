namespace sms.space.management.api.BusinessLogic.CustomExceptions;

public interface ICustomException
{
    IDictionary<string, List<string>> Errors { get; }
    //List<ErrorDto> ErrorList { get; }
}
