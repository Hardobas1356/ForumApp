using ForumApp.Services.Core.Interfaces;
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

    public IActionResult Index()
    {
        return View();
    }
}
