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
        IEnumerable<BoardAdminViewModel>? model = await boardService
            .GetAllBoardsForAdminAsync();

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Approve(Guid id)
    {
        await boardService
            .ApproveBoardAsync(id);

        return RedirectToAction(nameof(Index));
    }
}
