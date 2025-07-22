using ForumApp.Services.Core.Interfaces;
using ForumApp.Web.ViewModels.Admin.Board;
using Microsoft.AspNetCore.Mvc;

using static ForumApp.GCommon.Enums.FilterEnums;
using static ForumApp.GCommon.Enums.SortEnums.Board;
using static ForumApp.GCommon.Enums.SortEnums.Post;
using static ForumApp.GCommon.GlobalConstants.Pages;

namespace ForumApp.Web.Areas.Admin.Controllers
{
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
        public async Task<IActionResult> Index(BoardAdminFilter filter, BoardAllSortBy sortOrder, string? searchTerm)
        {
            try
            {
                ViewBag.CurrentFilter = filter;
                ViewBag.CurrentSortingOrder = sortOrder;

                IEnumerable<BoardAdminViewModel>? model = await boardService
                    .GetAllBoardsForAdminAsync(filter, sortOrder, searchTerm);

                return View(model);
            }
            catch (Exception e)
            {
                logger.LogError("Error occurred while getting dashboard Index.");
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
}
