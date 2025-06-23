using ForumApp.Web.ViewModels.Reply;

namespace ForumApp.Services.Core.Interfaces;

public interface IReplyService
{
    Task<bool> CreateReplyForPostAsync(Guid userId, ReplyCreateInputModel model);
    Task<ICollection<ReplyDetailForPostDetailViewModel>?> GetRepliesForPostDetailsAsync(Guid? userId, Guid postId);
    Task<ReplyDeleteViewModel?> GetReplyForDeleteAsync(Guid userId, Guid postId, Guid id);
    Task<bool> SoftDeleteReplyAsync(Guid userId, ReplyDeleteViewModel model);
}