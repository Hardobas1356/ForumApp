using ForumApp.Services.Core.Interfaces;
using ForumApp.Web.ViewModels.Admin.Board;
using ForumApp.Web.ViewModels.ApplicationUser;
using Microsoft.AspNetCore.Mvc;

using static ForumApp.GCommon.FilterEnums;

namespace ForumApp.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class DashboardController : Controller
{
    private readonly IBoardService boardService;
    private readonly IApplicationUserService applicationUserService;
    public DashboardController(IBoardService boardService, IApplicationUserService applicationUserService)
    {
        this.boardService = boardService;
        this.applicationUserService = applicationUserService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(BoardAdminFilter filter)
    {
        try
        {
            ViewBag.CurrentFilter = filter;

            IEnumerable<BoardAdminViewModel>? model = await boardService
                .GetAllBoardsForAdminAsync(filter);

            return View(model);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return RedirectToAction(nameof(Index));
        }
    }

    [HttpGet]
    public async Task<IActionResult> Details(Guid id)
    {
        try
        {
            BoardDetailsAdminViewModel? board = await boardService
                .GetBoardDetailsAdminAsync(id);

            if (board == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(board);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return RedirectToAction(nameof(Index));
        }
    }

    [HttpGet]
    public async Task<IActionResult> SearchUsers(Guid boardId, string handle)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(handle))
            {
                return RedirectToAction(nameof(Details), new { id = boardId });
            }

            BoardDetailsAdminViewModel? board = await boardService
                .GetBoardDetailsAdminAsync(boardId);

            if (board == null)
            {
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
            Console.WriteLine(e.Message);
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
            Console.WriteLine(e.Message);
            return RedirectToAction(nameof(Index));
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddModerator(Guid boardId, Guid userId)
    {
        try
        {
            await boardService.AddModeratorAsync(userId, boardId);

            //todo add fail message on false

            return RedirectToAction(nameof(Details), new { id = boardId });
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return RedirectToAction(nameof(Details), new { Id = boardId });
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RemoveModerator(Guid boardId, Guid userId)
    {
        try
        {
            await boardService.RemoveModeratorAsync(userId, boardId);

            //todo add fail message on false

            return RedirectToAction(nameof(Details), new { id = boardId });
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
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
                return NotFound();
            }

            return View(model);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
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
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
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
                //Todo add failure message
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return RedirectToAction(nameof(Index));
        }
    }
}
