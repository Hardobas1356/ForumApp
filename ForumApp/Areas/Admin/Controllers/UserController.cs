using ForumApp.Services.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ForumApp.Web.ViewModels.Admin.ApplicationUser;
using ForumApp.GCommon;

using static ForumApp.GCommon.GlobalConstants.Pages;

namespace ForumApp.Web.Areas.Admin.Controllers;

public class UserController : BaseController
{
    private IApplicationUserService applicationUserService;
    private ILogger<UserController> logger;
    public UserController(IApplicationUserService applicationUserService, ILogger<UserController> logger)
    {
        this.applicationUserService = applicationUserService;
        this.logger = logger;
    }

    public async Task<IActionResult> Index(int pageNumber = 1)
    {
        try
        {
            PaginatedResult<UserAdminViewModel> users = await applicationUserService
                .GetAllUsersAdminAsync(pageNumber, USER_PAGE_SIZE);

            return View(users);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error while getting user index");
            return BadRequest();
        }
    }
}
