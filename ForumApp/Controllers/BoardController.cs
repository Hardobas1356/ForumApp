using ForumApp.Services.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ForumApp.Web.Controllers;

public class BoardController : BaseController
{
    private readonly IBoardService boardService;

    public BoardController(IBoardService boardService)
    {
        this.boardService = boardService;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        var boards = await boardService.GetAllBoardsAsync();

        return View(boards);
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Details(Guid id)
    {
        var board = await boardService
            .GetBoardDetailsAsync(id);

        if (board == null)
        {
            return NotFound();
        }

        return View(board);
    }
}
