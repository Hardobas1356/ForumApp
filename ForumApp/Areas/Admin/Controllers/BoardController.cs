using ForumApp.GCommon;
using ForumApp.Services.Core.Interfaces;
using ForumApp.Web.ViewModels.Admin.Board;
using Microsoft.AspNetCore.Mvc;

using static ForumApp.GCommon.Enums.FilterEnums;
using static ForumApp.GCommon.Enums.SortEnums.BoardSort;
using static ForumApp.GCommon.Enums.SortEnums.PostSort;
using static ForumApp.GCommon.GlobalConstants.Pages;

namespace ForumApp.Web.Areas.Admin.Controllers;

public class BoardController : BaseController
{
    private IBoardService boardService;
    private ILogger<BoardController> logger;

    public BoardController(IBoardService boardService, ILogger<BoardController> logger)
    {
        this.boardService = boardService;
        this.logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Index(BoardAdminFilter filter,
        BoardAllSortBy sortOrder, string? searchTerm, int pageNumber = 1)
    {
        try
        {
            ViewBag.CurrentFilter = filter;
            ViewBag.CurrentSortingOrder = sortOrder;

            PaginatedResult<BoardAdminViewModel> model = await boardService
                .GetAllBoardsForAdminAsync(filter, sortOrder,
                    searchTerm, pageNumber, ADMIN_BOARD_PAGE_SIZE);

            return View(model);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error occurred while getting dashboard Index.");
            return RedirectToAction(nameof(Index));
        }
    }

    [HttpGet]
    public async Task<IActionResult> Details(Guid id, PostSortBy sortBy, int pageNumber = 1)
    {
        try
        {
            BoardDetailsAdminViewModel? board = await boardService
                .GetBoardDetailsAdminAsync(id, sortBy, pageNumber, POST_PAGE_SIZE);

            ViewBag.SortBy = sortBy;

            return View(board);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error occurred while getting board details.");
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

    [HttpGet]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            BoardDeleteViewModel? model = await boardService
                 .GetBoardForDeletionAsync(id);

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
            await boardService.SoftDeleteBoardAsync(model);

            return RedirectToAction(nameof(Index));
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error occurred while deleting board");
            return View(model);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RestoreDeletedBoard(Guid id)
    {
        try
        {
            await boardService.RestoreBoardAsync(id);

            return RedirectToAction(nameof(Index));
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error occurred while restoring board");
            return RedirectToAction(nameof(Index));
        }
    }
}
