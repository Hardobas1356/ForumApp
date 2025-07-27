using Microsoft.AspNetCore.Mvc;

namespace ForumApp.Web.Controllers;

public class ErrorController : Controller
{
    [Route("Error/{statusCode}")]
    public IActionResult HttpStatusCodeHandler(int statusCode)
    {
        switch (statusCode)
        {
            case 404:
                return View("404");
            case 403:
                return View("403");
            default:
                return View("Generic");
        }
    }

    [Route("Error/500")]
    public IActionResult InternalServerError()
    {
        return View("500");
    }
}
