using Microsoft.AspNetCore.Mvc.Filters;

namespace ForumApp.Web.Attributes;

public class BoardManagerOnlyAttrivute : Attribute, IAsyncAuthorizationFilter
{
    public Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        throw new NotImplementedException();
    }
}
