using ForumApp.Services.Core.Interfaces;
using ForumApp.Web.ViewModels.Board;
using ForumApp.Web.ViewModels.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ForumApp.Web.Controllers;

public class BoardController : BaseController
{
    private readonly IBoardService boardService;
    private readonly ICategoryService categoryService;

    public BoardController(IBoardService boardService, ICategoryService categoryService)
    {
        this.boardService = boardService;
        this.categoryService = categoryService;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        try
        {
            IEnumerable<BoardAllIndexViewModel> boards = await boardService
                .GetAllBoardsAsync();

            return View(boards);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return RedirectToAction(nameof(Index));
        }
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        try
        {
            ICollection<CategoryViewModel> categories = await this.categoryService
                .GetCategoriesAsync();

            BoardCreateInputModel model = new BoardCreateInputModel()
            {
                AvailableCategories = categories
            };

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
    public async Task<IActionResult> Create(BoardCreateInputModel model)
    {
        try
        {
            if (!this.ModelState.IsValid)
            {
                ICollection<CategoryViewModel> categories = await this.categoryService
                    .GetCategoriesAsync();
                model.AvailableCategories = categories;

                return View(model);
            }

            bool createResult = await boardService
                .CreateBoardAsync((Guid)this.GetUserId()!, model);

            if (!createResult)
            {
                ICollection<CategoryViewModel> categories = await this.categoryService
                    .GetCategoriesAsync();
                model.AvailableCategories = categories;

                ModelState.AddModelError(nameof(model.Name), "A board with this name already exists.");

                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            ModelState.AddModelError(string.Empty, "Unexpected error occurred while creating the board.");
            model.AvailableCategories = await categoryService.GetCategoriesAsync();
            return View(model);
        }
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Details(Guid id)
    {
        try
        {
            BoardDetailsViewModel? board = await boardService
                .GetBoardDetailsAsync(id);

            if (board == null)
            {
                return NotFound();
            }

            return View(board);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return RedirectToAction(nameof(Index));
        }
    }
}
