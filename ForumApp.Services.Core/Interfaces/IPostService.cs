using ForumApp.Web.ViewModels.Post;

namespace ForumApp.Services.Core.Interfaces;

public interface IPostService
{
    Task<PostDetailsViewModel?> GetPostDetailsAsync(Guid? userId, Guid id);
    Task<IEnumerable<PostBoardDetailsViewModel>?> GetPostsForBoardDetailsAsync(Guid boardId);
    Task<PostEditInputModel?> GetPostForEditAsync(Guid userId, Guid id);
    Task<bool> EditPostAsync(Guid userId, PostEditInputModel model);
    Task<bool> AddPostAsync(Guid userId, PostCreateInputModel model);
    Task<PostDeleteViewModel?> GetPostForDeleteAsync(Guid userId, Guid id);
    Task<bool> DeletePostAsync(Guid userId, PostDeleteViewModel model);
}
