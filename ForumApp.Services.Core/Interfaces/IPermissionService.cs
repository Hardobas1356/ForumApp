namespace ForumApp.Services.Core.Interfaces;

public interface IPermissionService
{
    Task<bool> IsOwnerOfPost(Guid userId, Guid postId);
    Task<bool> CanManagePostAsync(Guid userId, Guid postId);
    Task<bool> CanManageReplyAsync(Guid userId, Guid replyId);
}
