using ForumApp.Services.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ForumApp.Web.Controllers;

public class BoardController : Controller
{
    private readonly IBoardService boardService;

    public BoardController(IBoardService boardService)
    {
        this.boardService = boardService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var boards = await boardService.GetAllBoardsAsync();

        return View(boards);
    }
}
