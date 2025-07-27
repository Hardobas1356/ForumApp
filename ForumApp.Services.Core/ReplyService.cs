using ForumApp.Data.Models;
using ForumApp.GCommon;
using ForumApp.Services.Core.Interfaces;
using ForumApp.Web.ViewModels.Reply;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using static ForumApp.GCommon.Enums.SortEnums.ReplySort;
using static ForumApp.GCommon.GlobalConstants.Roles;
using static ForumApp.GCommon.GlobalConstants;

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

    public async Task CreateReplyForPostAsync(Guid userId, ReplyCreateInputModel model)
    {
        Post? post = await postRepository
            .GetByIdAsync(model.PostId, true);

        if (post == null)
        {
            throw new ArgumentException($"Post not found {model.PostId}");
        }

        ApplicationUser? user = await userManager
            .FindByIdAsync(userId.ToString());

        if (user == null)
        {
            throw new ArgumentException($"User not found {userId}");
        }

        Reply reply = new Reply()
        {
            PostId = model.PostId,
            ApplicationUserId = userId,
            Content = model.Content,
            CreatedAt = DateTime.UtcNow,
        };

        try
        {
            await replyRepository.AddAsync(reply);
            await replyRepository.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception("Error while creating reply", e);
        }
    }
    public async Task<PaginatedResult<ReplyForPostDetailViewModel>> GetRepliesForPostDetailsAsync(Guid? userId,
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
                Author = r.ApplicationUser == null || r.ApplicationUser.IsDeleted
                        ? DeletedUser.DELETED_DISPLAYNAME : r.ApplicationUser.DisplayName!,
                CreatedAt = r.CreatedAt.ToString(APPLICATION_DATE_TIME_FORMAT),
                IsPublisher = userId != null && r.ApplicationUserId == userId,
                CanModerate = canModerate
            });

        return await PaginatedResult<ReplyForPostDetailViewModel>
            .CreateAsync(replies, pageNumber, pageSize);
    }

    public async Task<ReplyDeleteViewModel> GetReplyForDeleteAsync(Guid userId, Guid postId, Guid id)
    {
        Reply? reply = await replyRepository
            .SingleOrDefaultWithIncludeAsync(r => r.Id == id
                                                    && r.PostId == postId,
                                             q => q.Include(r => r.Post)
                                                   .ThenInclude(p => p.Board)
                                                   .ThenInclude(b => b.BoardManagers));

        if (reply == null)
        {
            throw new ArgumentException($"Reply not found. Id:{id} , post id: {postId}");
        }

        if (!await UserHasRights(reply, userId))
        {
            throw new OperationCanceledException("User does not permission to delete reply");
        }

        return new ReplyDeleteViewModel
        {
            Id = id,
            PostId = postId,
            Content = reply.Content,
            CreatedAt = reply.CreatedAt.ToString(APPLICATION_DATE_TIME_FORMAT)
        };
    }
    public async Task SoftDeleteReplyAsync(Guid userId, ReplyDeleteViewModel model)
    {
        Reply? reply = await replyRepository
           .SingleOrDefaultWithIncludeAsync(r => r.Id == model.Id
                                                   && r.PostId == model.PostId,
                                            q => q.Include(r => r.Post.Board.BoardManagers),
                                            asNoTracking: false);

        if (reply == null)
        {
            throw new ArgumentException($"Reply not found. Id:{model.Id} , post id: {model.PostId}");
        }

        if (!await UserHasRights(reply, userId))
        {
            throw new OperationCanceledException("User does not permission to delete reply");
        }

        try
        {
            reply.IsDeleted = true;
            await replyRepository.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception("Error occured while deleting reply", e);
        }
    }
    public async Task<ReplyEditInputModel> GetReplyForEditAsync(Guid userId, Guid postId, Guid id)
    {
        Reply? reply = await replyRepository
            .SingleOrDefaultAsync(r => r.Id == id
                                  && r.PostId == postId
                                  && r.ApplicationUserId == userId,
                                  asNoTracking: true);

        if (reply == null)
        {
            throw new ArgumentException($"Reply not found. Id:{id} , post id: {postId}");
        }

        return new ReplyEditInputModel
        {
            Id = reply.Id,
            PostId = reply.PostId,
            Content = reply.Content,
        };
    }

    public async Task EditReplyAsync(Guid userId, ReplyEditInputModel model)
    {
        Reply? reply = await replyRepository
            .SingleOrDefaultAsync(r => r.Id == model.Id
                                  && r.PostId == model.PostId
                                  && r.ApplicationUserId == userId,
                                  asNoTracking: false);

        if (reply == null)
        {
            throw new ArgumentException($"Reply not found. Id:{model.Id} , post id: {model.PostId}");
        }

        try
        {
            reply.Content = model.Content;
            await replyRepository.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception("Error occured while editing reply", e);
        }
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
            .IsInRoleAsync(user, ADMIN_ROLE_NAME);
    }
}
