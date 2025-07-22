using ForumApp.Data;
using ForumApp.Data.Models;
using ForumApp.Services.Core.Interfaces;
using ForumApp.Web.ViewModels.Reply;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using static ForumApp.GCommon.GlobalConstants;
using static ForumApp.GCommon.Enums.SortEnums.Reply;
using ForumApp.GCommon;

namespace ForumApp.Services.Core;

public class ReplyService : IReplyService
{
    private readonly IGenericRepository<Reply> replyRepository;
    private readonly IGenericRepository<Post> postRepository;
    private readonly UserManager<ApplicationUser> userManager;

    public ReplyService(
        IGenericRepository<Reply> replyRepository,
        IGenericRepository<Post> postRepository,
        UserManager<ApplicationUser> userManager)
    {
        this.replyRepository = replyRepository;
        this.postRepository = postRepository;
        this.userManager = userManager;
    }

    public async Task<bool> CreateReplyForPostAsync(Guid userId, ReplyCreateInputModel model)
    {
        Post? post = await postRepository
            .GetByIdAsync(model.PostId, true);

        ApplicationUser? user = await userManager
            .FindByIdAsync(userId.ToString());

        if (post == null || user == null)
        {
            return false;
        }

        Reply reply = new Reply()
        {
            PostId = model.PostId,
            ApplicationUserId = userId,
            Content = model.Content,
            CreatedAt = DateTime.UtcNow,
        };

        await replyRepository.AddAsync(reply);
        await replyRepository.SaveChangesAsync();

        return true;
    }
    public async Task<PaginatedResult<ReplyForPostDetailViewModel>?> GetRepliesForPostDetailsAsync(Guid? userId,
        Guid postId, bool canModerate, ReplySortBy sortBy,
        int pageNumber, int pageSize)
    {
        IQueryable<Reply> query = replyRepository
            .GetQueryable()
            .Where(r => r.PostId == postId)
            .Include(r => r.ApplicationUser);

        switch (sortBy)
        {
            case ReplySortBy.Default:
                query = query.OrderBy(r => r.CreatedAt);
                break;
            case ReplySortBy.CreateTimeAscending:
                query = query.OrderBy(r => r.CreatedAt);
                break;
            case ReplySortBy.CreateTimeDescending:
                query = query.OrderByDescending(r => r.CreatedAt);
                break;
            case ReplySortBy.ContentAscending:
                query = query.OrderBy(r => r.Content);
                break;
            case ReplySortBy.ContentDescending:
                query = query.OrderByDescending(r => r.Content);
                break;
        }

        IQueryable<ReplyForPostDetailViewModel> replies = query
            .Select(r => new ReplyForPostDetailViewModel
            {
                Id = r.Id,
                Content = r.Content,
                Author = r.ApplicationUser.DisplayName ?? "Unknown",
                CreatedAt = r.CreatedAt.ToString(APPLICATION_DATE_TIME_FORMAT),
                IsPublisher = userId != null && r.ApplicationUserId == userId,
                CanModerate = canModerate
            });

        return await PaginatedResult<ReplyForPostDetailViewModel>
            .CreateAsync(replies, pageNumber, pageSize);
    }

    public async Task<ReplyDeleteViewModel?> GetReplyForDeleteAsync(Guid userId, Guid postId, Guid id)
    {
        Reply? reply = await replyRepository
            .SingleOrDefaultWithIncludeAsync(r => r.Id == id
                                                    && r.PostId == postId,
                                             q => q.Include(r => r.Post)
                                                   .ThenInclude(p => p.Board)
                                                   .ThenInclude(b => b.BoardManagers));

        if (reply == null || reply.Post == null || reply.Post.Board == null)
        {
            return null;
        }

        if (!await UserHasRights(reply, userId))
        {
            return null;
        }

        return new ReplyDeleteViewModel
        {
            Id = id,
            PostId = postId,
            Content = reply.Content,
            CreatedAt = reply.CreatedAt.ToString(APPLICATION_DATE_TIME_FORMAT)
        };
    }
    public async Task<bool> SoftDeleteReplyAsync(Guid userId, ReplyDeleteViewModel model)
    {
        Reply? reply = await replyRepository
           .SingleOrDefaultWithIncludeAsync(r => r.Id == model.Id
                                                   && r.PostId == model.PostId,
                                            q => q.Include(r => r.Post.Board.BoardManagers),
                                            asNoTracking: false);

        if (reply == null || reply.Post == null || reply.Post.Board == null)
        {
            return false;
        }

        if (!await UserHasRights(reply, userId))
        {
            return false;
        }

        reply.IsDeleted = true;
        await replyRepository.SaveChangesAsync();

        return true;
    }
    public async Task<ReplyEditInputModel?> GetReplyForEditAsync(Guid userId, Guid postId, Guid id)
    {
        Reply? reply = await replyRepository
            .SingleOrDefaultAsync(r => r.Id == id
                                  && r.PostId == postId
                                  && r.ApplicationUserId == userId,
                                  asNoTracking: true);

        if (reply == null) return null;

        return new ReplyEditInputModel
        {
            Id = reply.Id,
            PostId = reply.PostId,
            Content = reply.Content,
        };
    }

    public async Task<bool> EditReplyAsync(Guid userId, ReplyEditInputModel model)
    {
        Reply? reply = await replyRepository
            .SingleOrDefaultAsync(r => r.Id == model.Id
                                  && r.PostId == model.PostId
                                  && r.ApplicationUserId == userId,
                                  asNoTracking: false);

        if (reply == null) return false;

        reply.Content = model.Content;
        await replyRepository.SaveChangesAsync();

        return true;
    }

    private async Task<bool> UserHasRights(Reply reply, Guid userId)
    {
        if (reply.ApplicationUserId == userId)
        {
            return true;
        }

        if (reply.Post.Board.BoardManagers.Any(m => m.ApplicationUserId == userId))
        {
            return true;
        }

        ApplicationUser? user = await userManager
            .FindByIdAsync(userId.ToString());

        if (user == null)
        {
            return false;
        }

        return await userManager
            .IsInRoleAsync(user, "Admin");
    }
}
