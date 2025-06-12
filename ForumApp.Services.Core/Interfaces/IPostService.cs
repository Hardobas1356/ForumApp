using ForumApp.Web.ViewModels.Post;

namespace ForumApp.Services.Core.Interfaces;

public interface IPostService
{
    Task<PostDetailsViewModel?> GetPostDetailsAsync(string id);

    Task<EditPostViewModel?> GetPostForEditAsync(string id);
    Task<bool> EditPostAsync(EditPostViewModel model);
}
