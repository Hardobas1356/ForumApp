using ForumApp.Web.ViewModels.Post;

namespace ForumApp.Services.Core.Interfaces;

public interface IPostService
{
    Task<PostDetailsViewModel?> GetPostDetailsAsync(int id);

    Task<EditPostViewModel?> GetPostForEditAsync(int id);
    Task<bool> EditPostAsync(EditPostViewModel model);
}
