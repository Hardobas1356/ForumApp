using ForumApp.Web.ViewModels.Post;

using static ForumApp.GCommon.SortEnums.Post;
using static ForumApp.GCommon.SortEnums.Reply;

namespace ForumApp.Services.Core.Interfaces;

public interface IPostService
{
    Task<PostDetailsViewModel?> GetPostDetailsAsync(Guid? userId, Guid id, ReplySortBy sortBy);
    Task<IEnumerable<PostForBoardDetailsViewModel>?> GetPostsForBoardDetailsAsync(Guid boardId, PostSortBy sortOrder);
    Task<PostEditInputModel?> GetPostForEditAsync(Guid userId, Guid id);
    Task<bool> EditPostAsync(Guid userId, PostEditInputModel model);
    Task<bool> AddPostAsync(Guid userId, PostCreateInputModel model);
    Task<PostDeleteViewModel?> GetPostForDeleteAsync(Guid userId, Guid id);
    Task<bool> DeletePostAsync(Guid userId, PostDeleteViewModel model);
}
