using Microsoft.AspNetCore.Mvc;

namespace ClientHub.WebApi.Controllers;

public static class ControllerBaseExtension
{
    public static OkObjectResult SendOkResult<T>(this ControllerBase controllerBase, T? value)
    {
        var response = new ApiResponse<T>() { Message = ApiResponseStatuses.Success, Data = value };

        return controllerBase.Ok(response);
    }
    
    public static OkObjectResult SendOkResult(this ControllerBase controllerBase)
    {
        var response = new ApiResponse() { Message = ApiResponseStatuses.Success };

        return controllerBase.Ok(response);
    }
}
