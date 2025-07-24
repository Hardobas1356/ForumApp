using ForumApp.GCommon;
using ForumApp.Services.Core.Interfaces;
using ForumApp.Web.ViewModels.Admin.ApplicationUser;
using Microsoft.AspNetCore.Mvc;

using static ForumApp.GCommon.Enums.SortEnums.UserSort;
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

    [HttpGet]
    public async Task<IActionResult> Index(
        string? searchTerm, UserSortBy sortOrder = UserSortBy.IsModeratorFirst, int pageNumber = 1)
    {
        try
        {
            PaginatedResult<UserAdminViewModel> users = await applicationUserService
                .GetAllUsersAdminAsync(pageNumber, USER_PAGE_SIZE, searchTerm, sortOrder);

            return View(users);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error while getting user index");
            return BadRequest();
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await applicationUserService.SoftDeleteUserAsync(id);
            TempData["SuccessMessage"] = "User deleted successfully.";

        }
        catch (Exception e)
        {
            logger.LogError(e, "Error while deleting user with ID {id}", id);
            TempData["ErrorMessage"] = "An error occurred while deleting the user.";
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Restore(Guid id)
    {
        try
        {
            await applicationUserService.RestoreUserAsync(id);
            TempData["SuccessMessage"] = "User restored successfully.";

        }
        catch (Exception e)
        {
            logger.LogError(e, "Error while restoring user with ID {id}", id);
            TempData["ErrorMessage"] = "An error occurred while restoring the user.";
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        try
        {
            UserEditInputModel model = await applicationUserService.GetUserForEditAsync(id);

            return View(model);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error while getting user with ID {id}", id);
            TempData["ErrorMessage"] = "An error occurred while getting user.";
            return RedirectToAction(nameof(Index));
        }
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(UserEditInputModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            await applicationUserService.EditUserAsync(model);

            TempData["SuccessMessage"] = "User updated successfully.";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error updating user");
            ModelState.AddModelError(string.Empty, ex.Message);
            return View(model);
        }
    }
}
