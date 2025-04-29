using Microsoft.AspNetCore.Mvc;
using NextgenInovasiTest.Models;

namespace NextgenInovasiTest.Controllers;

public class BaseController : Controller
{
    private readonly ILogger _logger;

    public BaseController(ILogger logger)
    {
        _logger = logger;
    }
    public async Task<JsonResult> HandleResponse<TResult>(Func<Task<TResult>> action)
    {
        try
        {
            var responseData = await action();
            return new JsonResult(responseData);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while processing the request.");
            return new JsonResult(new { error = "Internal server error" })
            {
                StatusCode = 500
            };
        }
    }
    public async Task<JsonResult> HandleResponse(Func<Task> action)
    {
        try
        {
            await action();
            return new JsonResult(new { success = true });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while processing the request.");
            return new JsonResult(new { error = "Internal server error" })
            {
                StatusCode = 500
            };
        }
    }
}
