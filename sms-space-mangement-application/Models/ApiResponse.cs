namespace sms.space.management.application.Models;

public abstract record BaseReponse(ApiStatusCodes StatusCode, bool Status, string Message);

public record ApiResponse(object Data, ApiStatusCodes StatusCode, bool Status, string Message)
    : BaseReponse(StatusCode, Status, Message);

//public record ErrorModel(string FieldName, string Message);

public record ApiErrorResponse(IDictionary<string, List<string>>? Errors, ApiStatusCodes StatusCode, string Message)
  : BaseReponse(StatusCode, false, Message);


public enum ApiStatusCodes
{
    NotFound = 404,
    Success = 200,
    Error = 500,
    InputValidationFailure = 400,
    UnableToProcess = 499,
    UnAuthorized = 401,
    ForbiddenAccess = 403,
    Ddos = 503,
    None = 0
}