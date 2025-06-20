using ForumApp.Web.ViewModels.Reply;

namespace ForumApp.Services.Core.Interfaces;

public interface IReplyService
{
    Task<bool> CreateReplyForPost(Guid userId, ReplyCreateInputModel model);
}
