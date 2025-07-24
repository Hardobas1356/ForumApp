using ForumApp.Services.Core.Interfaces;
using ForumApp.Web.ViewModels.Board;
using ForumApp.Web.ViewModels.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using static ForumApp.GCommon.Enums.SortEnums.BoardSort;
using static ForumApp.GCommon.Enums.SortEnums.PostSort;
using static ForumApp.GCommon.GlobalConstants.Pages;

namespace ForumApp.Web.Controllers;

public class BoardController : BaseController
{
    private readonly IBoardService boardService;
    private readonly ICategoryService categoryService;
    private readonly ILogger<BoardController> logger;

    public BoardController(IBoardService boardService, ICategoryService categoryService, ILogger<BoardController> logger)
    {
        this.boardService = boardService;
        this.categoryService = categoryService;
        this.logger = logger;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Index(string? searchTerm, BoardAllSortBy sortOrder = BoardAllSortBy.CreateTimeDescending,
        int pageNumber = 1)
    {
        try
        {
            ViewBag.CurrentSortingOrder = sortOrder;
            ViewBag.SearchTerm = searchTerm;

            var boards = await boardService
                .GetAllBoardsAsync(this.GetUserId(), sortOrder, searchTerm, pageNumber, BOARD_PAGE_SIZE);

            return View(boards);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Failed to load boards for Index view.");
            return RedirectToAction(nameof(Index));
        }
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        try
        {
            IEnumerable<CategoryViewModel> categories = await this.categoryService
                .GetCategoriesAsync();

            BoardCreateInputModel model = new BoardCreateInputModel()
            {
                AvailableCategories = categories
            };

            return View(model);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Failed to load categories for board creation form.");
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
                logger.LogWarning("Board creation form is invalid.");

                IEnumerable<CategoryViewModel> categories = await this.categoryService
                    .GetCategoriesAsync();
                model.AvailableCategories = categories;

                return View(model);
            }

            bool createResult = await boardService
                .CreateBoardAsync((Guid)this.GetUserId()!, model);

            if (!createResult)
            {
                logger.LogWarning("Board creation failed. BoardService returned false.");

                IEnumerable<CategoryViewModel> categories = await this.categoryService
                    .GetCategoriesAsync();
                model.AvailableCategories = categories;

                ModelState.AddModelError(nameof(model.Name), "A board with this name already exists.");

                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }
        catch (Exception e)
        {
            logger.LogError(e, "Unexpected error while creating a board.");
            ModelState.AddModelError(string.Empty, "Unexpected error occurred while creating the board.");
            model.AvailableCategories = await categoryService.GetCategoriesAsync();
            return View(model);
        }
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Details(Guid id, PostSortBy sortOrder, string? searchTerm, int pageNumber = 1)
    {
        try
        {
            BoardDetailsViewModel? board = await boardService
                .GetBoardDetailsAsync(id, sortOrder, searchTerm, pageNumber, BOARD_PAGE_SIZE);

            if (board == null)
            {
                logger.LogWarning("Board with ID {BoardId} not found.", id);
                return RedirectToAction(nameof(Index));
            }

            return View(board);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Failed to load board details for board ID {BoardId}", id);
            return RedirectToAction(nameof(Index));
        }
    }
}
