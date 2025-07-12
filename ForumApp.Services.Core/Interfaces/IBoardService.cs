using ForumApp.Web.ViewModels.Admin.Board;
using ForumApp.Web.ViewModels.Board;

using static ForumApp.GCommon.FilterEnums;

namespace ForumApp.Services.Core.Interfaces;

public interface IBoardService
{
    Task<IEnumerable<BoardAdminViewModel>?> GetAllBoardsForAdminAsync(BoardAdminFilter filter);
    Task<IEnumerable<BoardAllIndexViewModel>> GetAllBoardsAsync();
    Task<BoardDetailsViewModel?> GetBoardDetailsAsync(Guid boardId);
    Task<BoardDeleteViewModel?> GetBoardForDeletionAsync(Guid id);
    Task<string?> GetBoardNameByIdAsync(Guid id);
    Task<bool> CreateBoardAsync(Guid userId,BoardCreateInputModel model);
    Task<bool> RestoreBoardAsync(Guid id);
    Task<bool> SoftDeleteBoardAsync(BoardDeleteViewModel model);
    Task<bool> ApproveBoardAsync(Guid id);
}
