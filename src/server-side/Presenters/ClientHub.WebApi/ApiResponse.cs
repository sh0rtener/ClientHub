using Newtonsoft.Json;

namespace ClientHub.WebApi;

public sealed class ApiResponse<T>
{
    [JsonProperty("message")]
    public string Message { get; set; } = ApiResponseStatuses.Info;

    [JsonProperty("data")]
    public T? Data { get; set; }
}

public sealed class ApiResponse
{
    [JsonProperty("message")]
    public string Message { get; set; } = ApiResponseStatuses.Info;
}

public static class ApiResponseStatuses
{
    public const string Trace = "trace";
    public const string Info = "information";
    public const string Success = "success";
    public const string Redirect = "redirect";
    public const string ClientError = "bad request";
    public const string ServerError = "internal error";
}
