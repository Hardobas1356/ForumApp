using ForumApp.Services.Core.Interfaces;
using ForumApp.Web.ViewModels.Admin.Board;
using Microsoft.AspNetCore.Mvc;

namespace ForumApp.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class DashboardController : Controller
{
    private readonly IBoardService boardService;
    public DashboardController(IBoardService boardService)
    {
        this.boardService = boardService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        try
        {
            IEnumerable<BoardAdminViewModel>? model = await boardService
                .GetAllBoardsForAdminAsync();

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
