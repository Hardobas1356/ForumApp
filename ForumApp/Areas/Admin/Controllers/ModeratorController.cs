using ForumApp.Services.Core.Interfaces;
using ForumApp.Web.ViewModels.Admin.Board;
using ForumApp.Web.ViewModels.ApplicationUser;
using Microsoft.AspNetCore.Mvc;

using static ForumApp.GCommon.SortEnums.Post;

namespace ForumApp.Web.Areas.Admin.Controllers
{
    public class ModeratorController : BaseController
    {
        private const int pageSize = 10;

        private readonly IBoardService boardService;
        private readonly IApplicationUserService applicationUserService;
        private readonly ILogger<DashboardController> logger;

        public ModeratorController(IBoardService boardService, IApplicationUserService applicationUserService,
            ILogger<DashboardController> logger)
        {
            this.boardService = boardService;
            this.applicationUserService = applicationUserService;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> SearchUsers(Guid boardId, string handle, PostSortBy sortBy, int pageNumber = 1)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(handle))
                {
                    logger.LogWarning("No handle provided");
                    return RedirectToAction("Details", "Board", new { id = boardId });
                }

                BoardDetailsAdminViewModel? board = await boardService
                    .GetBoardDetailsAdminAsync(boardId, sortBy,pageNumber, pageSize);

                if (board == null)
                {
                    logger.LogWarning("No board found ID: {id}", boardId);
                    return RedirectToAction("Index", "Board");
                }

                ICollection<UserModeratorViewModel>? searchResults = await applicationUserService
                    .SearchUsersWithModeratorStatusAsync(boardId, handle);

                board.SearchResults = searchResults;
                board.SearchPerformed = true;
                return View("~/Areas/Admin/Views/Board/Details.cshtml", board);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error occurred while searching for users in dashboard board details.");
                return RedirectToAction("Index", "Board");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddModerator(Guid boardId, Guid userId)
        {
            try
            {
                bool addResult = await boardService.AddModeratorAsync(userId, boardId);

                if (!addResult)
                {
                    logger.LogWarning("Failed to add moderator. UserId: {UserId}, BoardId: {BoardId}", userId, boardId);
                }

                return RedirectToAction("Details", "Board", new { id = boardId });
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error occcured while adding moderator");
                return RedirectToAction("Details", "Board", new { id = boardId });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveModerator(Guid boardId, Guid userId)
        {
            try
            {
                bool removeResult = await boardService.RemoveModeratorAsync(userId, boardId);

                if (!removeResult)
                {
                    logger.LogWarning("Failed to remove moderator. UserId: {UserId}, BoardId: {BoardId}", userId, boardId);
                }

                return RedirectToAction("Details", "Board", new { id = boardId });
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error occcured while removing moderator");
                return RedirectToAction("Details", "Board", new { id = boardId });
            }
        }
    }
}
