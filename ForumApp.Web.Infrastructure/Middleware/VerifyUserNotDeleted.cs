using ForumApp.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace ForumApp.Web.Infrastructure.Middleware;
public class VerifyUserNotDeleted
{
    private readonly RequestDelegate next;

    public VerifyUserNotDeleted(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        SignInManager<ApplicationUser> signInManager
            = context.RequestServices.GetRequiredService<SignInManager<ApplicationUser>>();
        UserManager<ApplicationUser> userManager
            = context.RequestServices.GetRequiredService<UserManager<ApplicationUser>>();

        try
        {
            if (signInManager.IsSignedIn(context.User))
            {
                string? username = context.User.Identity?.Name;
                if (!String.IsNullOrEmpty(username))
                {
                    ApplicationUser? user = await userManager.FindByNameAsync(username!);

                    if (user != null)
                    {
                        if (user.IsDeleted)
                        {
                            await signInManager.SignOutAsync();
                            context.Response.Redirect("/Error/403");
                            return;
                        }
                    }
                }
            }

        }
        catch (Exception e)
        {
            await signInManager.SignOutAsync();
            context.Response.Redirect("/Error/500");
            return;
        }

        await next(context);
    }
}
