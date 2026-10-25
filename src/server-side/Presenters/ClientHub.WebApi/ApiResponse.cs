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

    [JsonProperty("data")]
    public object? Data { get; set; } = null;
}

public static class ApiResponseStatuses
{
    public const string Trace = "trace";
    public const string Info = "information";
    public const string Success = "success";
    public const string Redirect = "redirect";
    public const string ClientError = "bad request";
    public const string NotFound = "not found";
    public const string ServerError = "internal error";
}

public sealed class BadRequestApiResponse
{
    /// <summary>
    ///
    /// </summary>
    /// <example>bad request</example>
    public string Message { get; set; } = ApiResponseStatuses.ClientError;

    /// <summary>
    ///
    /// </summary>
    /// <example>some client error</example>
    public string? Data { get; set; } = null!;
}

public sealed class NotFoundApiResponse
{
    /// <summary>
    ///
    /// </summary>
    /// <example>not found</example>
    public string Message { get; set; } = ApiResponseStatuses.NotFound;
    public string? Data { get; set; } = null!;
}

public sealed class ServerErrorApiResponse
{
    /// <summary>
    ///
    /// </summary>
    /// <example>server error</example>
    public string Message { get; set; } = ApiResponseStatuses.ServerError;

    /// <summary>
    ///
    /// </summary>
    /// <example>some client error</example>
    public string? Data { get; set; } = null!;
}
