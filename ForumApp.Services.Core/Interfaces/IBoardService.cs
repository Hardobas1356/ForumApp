using ForumApp.Web.ViewModels.Admin.Board;
using ForumApp.Web.ViewModels.Board;

using static ForumApp.GCommon.FilterEnums;
using static ForumApp.GCommon.SortEnums.Board;
using static ForumApp.GCommon.SortEnums.Post;

namespace ForumApp.Services.Core.Interfaces;

public interface IBoardService
{
    Task<IEnumerable<BoardAdminViewModel>?> GetAllBoardsForAdminAsync(BoardAdminFilter filter, BoardAllSortBy sortOrder);
    Task<IEnumerable<BoardAllIndexViewModel>> GetAllBoardsAsync(Guid? userId, BoardAllSortBy sortOrder);
    Task<BoardDetailsViewModel?> GetBoardDetailsAsync(Guid boardId, PostSortBy sortOrder);
    Task<BoardDetailsAdminViewModel?> GetBoardDetailsAdminAsync(Guid boardId, PostSortBy sortBy);
    Task<BoardDeleteViewModel?> GetBoardForDeletionAsync(Guid boardId);
    Task<string?> GetBoardNameByIdAsync(Guid boardId);
    Task<bool> CreateBoardAsync(Guid userId,BoardCreateInputModel model);
    Task<bool> RestoreBoardAsync(Guid boardId);
    Task<bool> SoftDeleteBoardAsync(BoardDeleteViewModel model);
    Task<bool> ApproveBoardAsync(Guid boardId);
    Task<bool> AddModeratorAsync(Guid userId, Guid boardId);
    Task<bool> RemoveModeratorAsync(Guid userId, Guid boardId);
}
