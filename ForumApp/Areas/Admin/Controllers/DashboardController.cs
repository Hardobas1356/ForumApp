using ForumApp.Services.Core.Interfaces;
using ForumApp.Web.ViewModels.Admin.Board;
using ForumApp.Web.ViewModels.ApplicationUser;
using Microsoft.AspNetCore.Mvc;

using static ForumApp.GCommon.FilterEnums;
using static ForumApp.GCommon.SortEnums.Board;
using static ForumApp.GCommon.SortEnums.Post;

namespace ForumApp.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class DashboardController : Controller
{
    private readonly IBoardService boardService;
    private readonly IApplicationUserService applicationUserService;
    private readonly ILogger<DashboardController> logger;
    public DashboardController(IBoardService boardService, IApplicationUserService applicationUserService,
        ILogger<DashboardController> logger)
    {
        this.boardService = boardService;
        this.applicationUserService = applicationUserService;
        this.logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Index(BoardAdminFilter filter, BoardAllSortBy sortOrder)
    {
        try
        {
            ViewBag.CurrentFilter = filter;
            ViewBag.CurrentSortingOrder = sortOrder;

            IEnumerable<BoardAdminViewModel>? model = await boardService
                .GetAllBoardsForAdminAsync(filter, sortOrder);

            return View(model);
        }
        catch (Exception e)
        {
            logger.LogError("Error occurred while getting dashboard Index.");
            return RedirectToAction(nameof(Index));
        }
    }

    [HttpGet]
    public async Task<IActionResult> Details(Guid id, PostSortBy sortBy)
    {
        try
        {
            BoardDetailsAdminViewModel? board = await boardService
                .GetBoardDetailsAdminAsync(id, sortBy);

            if (board == null)
            {
                logger.LogWarning("No board was found with ID: {id}", id);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.SortBy = sortBy;

            return View(board);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error occurred while getting board details.");
            return RedirectToAction(nameof(Index));
        }
    }

    [HttpGet]
    public async Task<IActionResult> SearchUsers(Guid boardId, string handle, PostSortBy sortBy)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(handle))
            {
                logger.LogWarning("No handle provided");
                return RedirectToAction(nameof(Details), new { id = boardId });
            }

            BoardDetailsAdminViewModel? board = await boardService
                .GetBoardDetailsAdminAsync(boardId, sortBy);

            if (board == null)
            {
                logger.LogWarning("No board found ID: {id}", boardId);
                return RedirectToAction(nameof(Index));
            }

            ICollection<UserModeratorViewModel>? searchResults = await applicationUserService
                .SearchUsersWithModeratorStatusAsync(boardId, handle);

            board.SearchResults = searchResults;
            board.SearchPerformed = true;
            return View("Details", board);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error occurred while searching for users in dashboard board details.");
            return RedirectToAction(nameof(Index));
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Approve(Guid id)
    {
        try
        {
            await boardService
                .ApproveBoardAsync(id);

            return RedirectToAction(nameof(Index));
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error occcured while approving board");
            return RedirectToAction(nameof(Index));
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddModerator(Guid boardId, Guid userId)
    {
        try
        {
            bool addResult = await boardService.AddModeratorAsync(userId, boardId);

            if (!addResult)
            {
                logger.LogWarning("Failed to add moderator. UserId: {UserId}, BoardId: {BoardId}", userId, boardId);
            }

            return RedirectToAction(nameof(Details), new { id = boardId });
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error occcured while adding moderator");
            return RedirectToAction(nameof(Details), new { Id = boardId });
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RemoveModerator(Guid boardId, Guid userId)
    {
        try
        {
            bool removeResult = await boardService.RemoveModeratorAsync(userId, boardId);

            if (!removeResult)
            {
                logger.LogWarning("Failed to remove moderator. UserId: {UserId}, BoardId: {BoardId}", userId, boardId);
            }

            return RedirectToAction(nameof(Details), new { id = boardId });
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error occcured while removing moderator");
            return RedirectToAction(nameof(Details), new { Id = boardId });
        }
    }

    [HttpGet]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            BoardDeleteViewModel? model = await boardService
                 .GetBoardForDeletionAsync(id);

            if (model == null)
            {
                logger.LogWarning("Could not find board. ID: {id}", id);
                return NotFound();
            }

            return View(model);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error occurred while deleting board");
            return RedirectToAction(nameof(Index));
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SoftDeleteConfirm(BoardDeleteViewModel model)
    {
        try
        {
            bool deleteResult = await boardService.SoftDeleteBoardAsync(model);

            if (!deleteResult)
            {
                logger.LogWarning("Could not delete board. ID: {id}", model.Id);
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error occurred while deleting board");
            return RedirectToAction(nameof(Index));
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RestoreDeletedBoard(Guid id)
    {
        try
        {
            bool actionResult = await boardService
                .RestoreBoardAsync(id);

            if (!actionResult)
            {
                logger.LogWarning("Could not restore board. ID: {id}", id);
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error occurred while restoring board");
            return RedirectToAction(nameof(Index));
        }
    }
}
