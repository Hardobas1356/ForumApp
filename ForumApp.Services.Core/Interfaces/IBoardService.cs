using ForumApp.GCommon;
using ForumApp.Web.ViewModels.Admin.Board;
using ForumApp.Web.ViewModels.Board;

using static ForumApp.GCommon.Enums.FilterEnums;
using static ForumApp.GCommon.Enums.SortEnums.BoardSort;
using static ForumApp.GCommon.Enums.SortEnums.PostSort;

namespace ForumApp.Services.Core.Interfaces;

public interface IBoardService
{
    Task<PaginatedResult<BoardAdminViewModel>> GetAllBoardsForAdminAsync(BoardAdminFilter filter, BoardAllSortBy sortOrder,
        string? searchTerm, int pageNumber, int pageSize);
    Task<PaginatedResult<BoardAllIndexViewModel>> GetAllBoardsAsync(Guid? userId, BoardAllSortBy sortOrder,
        string? searchTerm, int pageNumber, int pageSize);
    Task<BoardDetailsViewModel> GetBoardDetailsAsync(Guid boardId, PostSortBy sortOrder,
        string? searchTerm, int pageNumber, int pageSize);
    Task<BoardDetailsAdminViewModel> GetBoardDetailsAdminAsync(Guid boardId, PostSortBy sortBy,
        int postPageNumber, int postPageSize);
    Task<BoardDeleteViewModel> GetBoardForDeletionAsync(Guid boardId);
    Task<string> GetBoardNameByIdAsync(Guid boardId);
    Task CreateBoardAsync(Guid userId, BoardCreateInputModel model);
    Task RestoreBoardAsync(Guid boardId);
    Task SoftDeleteBoardAsync(BoardDeleteViewModel model);
    Task ApproveBoardAsync(Guid boardId);
    Task AddModeratorAsync(Guid userId, Guid boardId);
    Task RemoveModeratorAsync(Guid userId, Guid boardId);
}
