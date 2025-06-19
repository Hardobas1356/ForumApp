using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ForumApp.Web.Controllers;

[Authorize]
public class BaseController : Controller
{
    public bool IsUserAuthenticated()
    {
        return this.User.Identity?.IsAuthenticated ?? false;
    }

    public Guid? GetUserId()
    {
        Guid? userId = null;

        bool IsAuthenticated = this.IsUserAuthenticated();

        if (IsAuthenticated)
        {
            var identifier = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Guid.TryParse(identifier, out Guid id);
            userId = id;
        }

        return userId;
    }
}
