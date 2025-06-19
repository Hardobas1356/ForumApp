using ForumApp.Web.ViewModels.Post;

namespace ForumApp.Services.Core.Interfaces;

public interface IPostService
{
    Task<PostDetailsViewModel?> GetPostDetailsAsync(Guid id);
    Task<IEnumerable<PostBoardDetailsViewModel>?> GetPostsForBoardDetailsAsync(Guid boardId);
    Task<PostEditInputModel?> GetPostForEditAsync(Guid id);
    Task<bool> EditPostAsync(PostEditInputModel model);
    Task<bool> AddPostAsync(PostCreateInputModel model);
    Task<PostDeleteViewModel?> GetPostForDeleteAsync(Guid id);
    Task<bool> DeletePostAsync(PostDeleteViewModel model);
}
