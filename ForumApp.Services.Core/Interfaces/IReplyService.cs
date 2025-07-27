using ForumApp.GCommon;
using ForumApp.Web.ViewModels.Reply;

using static ForumApp.GCommon.Enums.SortEnums.ReplySort;

namespace ForumApp.Services.Core.Interfaces;

public interface IReplyService
{
    Task CreateReplyForPostAsync(Guid userId, ReplyCreateInputModel model);
    Task<PaginatedResult<ReplyForPostDetailViewModel>> GetRepliesForPostDetailsAsync(Guid? userId,
        Guid postId, bool canModerate,
        ReplySortBy sortBy, int pageNumber, int pageSize);
    Task<ReplyDeleteViewModel> GetReplyForDeleteAsync(Guid userId, Guid postId, Guid id);
    Task SoftDeleteReplyAsync(Guid userId, ReplyDeleteViewModel model);
    Task<ReplyEditInputModel> GetReplyForEditAsync(Guid userId, Guid postId, Guid id);
    Task EditReplyAsync(Guid userId, ReplyEditInputModel model);
}