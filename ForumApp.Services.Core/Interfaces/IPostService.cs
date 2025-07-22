using ForumApp.GCommon;
using ForumApp.Web.ViewModels.Post;

using static ForumApp.GCommon.Enums.SortEnums.Post;
using static ForumApp.GCommon.Enums.SortEnums.Reply;

namespace ForumApp.Services.Core.Interfaces;

public interface IPostService
{
    Task<PostDetailsViewModel?> GetPostDetailsAsync(Guid? userId, Guid id, ReplySortBy sortBy, int pageNumber, int pageSize);
    Task<PaginatedResult<PostForBoardDetailsViewModel>?> GetPostsForBoardDetailsAsync(Guid boardId, PostSortBy sortOrder, string? searchTerm, int pageNumber, int pageSize);
    Task<PostEditInputModel?> GetPostForEditAsync(Guid userId, Guid id);
    Task<bool> EditPostAsync(Guid userId, PostEditInputModel model);
    Task<bool> AddPostAsync(Guid userId, PostCreateInputModel model);
    Task<PostDeleteViewModel?> GetPostForDeleteAsync(Guid userId, Guid id);
    Task<bool> DeletePostAsync(Guid userId, PostDeleteViewModel model);
}
