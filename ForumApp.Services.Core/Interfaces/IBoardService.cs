using ForumApp.Web.ViewModels.Admin.Board;
using ForumApp.Web.ViewModels.Board;

namespace ForumApp.Services.Core.Interfaces;

public interface IBoardService
{
    Task<IEnumerable<BoardAdminViewModel>?> GetAllBoardsForAdminAsync();
    Task<IEnumerable<BoardAllIndexViewModel>> GetAllBoardsAsync();
    Task<BoardDetailsViewModel?> GetBoardDetailsAsync(Guid boardId);
    Task<BoardDeleteViewModel?> GetBoardForDeletionAsync(Guid id);
    Task<bool> SoftDeleteBoardAsync(BoardDeleteViewModel model);
    Task<bool> ApproveBoardAsync(Guid id);
}
