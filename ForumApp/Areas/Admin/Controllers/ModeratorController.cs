using ForumApp.Services.Core.Interfaces;
using ForumApp.Web.ViewModels.Admin.Board;
using ForumApp.Web.ViewModels.ApplicationUser;
using Microsoft.AspNetCore.Mvc;

using static ForumApp.GCommon.Enums.SortEnums.PostSort;
using static ForumApp.GCommon.GlobalConstants.Pages;

namespace ForumApp.Web.Areas.Admin.Controllers;

public class ModeratorController : BaseAdminController
{
    private readonly IBoardService boardService;
    private readonly IApplicationUserService applicationUserService;
    private readonly ILogger<DashboardController> logger;

    public ModeratorController(IBoardService boardService, IApplicationUserService applicationUserService,
        ILogger<DashboardController> logger)
    {
        this.boardService = boardService;
        this.applicationUserService = applicationUserService;
        this.logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> SearchUsers(Guid boardId, string handle, PostSortBy sortBy, int postPageNumber = 1)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(handle))
            {
                logger.LogWarning("No handle provided");
                return RedirectToAction("Details", "Board", new { id = boardId });
            }

            BoardDetailsAdminViewModel? board = await boardService
                .GetBoardDetailsAdminAsync(boardId, sortBy, postPageNumber, POST_PAGE_SIZE);

            if (board == null)
            {
                logger.LogWarning("No board found ID: {id}", boardId);
                return RedirectToAction("Index", "Board");
            }

            ICollection<UserModeratorViewModel>? searchResults = await applicationUserService
                .SearchUsersByHandleFirstTenAsync(boardId, handle);

            board.SearchResults = searchResults;
            board.SearchPerformed = true;
            return View("~/Areas/Admin/Views/Board/Details.cshtml", board);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error occurred while searching for users in dashboard board details.");
            return RedirectToAction("Index", "Board");
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddModerator(Guid boardId, Guid userId)
    {
        try
        {
            await boardService.AddModeratorAsync(userId, boardId);

            return RedirectToAction("Details", "Board", new { id = boardId });
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error occcured while adding moderator");
            return RedirectToAction("Details", "Board", new { id = boardId });
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RemoveModerator(Guid boardId, Guid userId)
    {
        try
        {
            await boardService.RemoveModeratorAsync(userId, boardId);

            return RedirectToAction("Details", "Board", new { id = boardId });
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error occcured while removing moderator");
            return RedirectToAction("Details", "Board", new { id = boardId });
        }
    }
}
