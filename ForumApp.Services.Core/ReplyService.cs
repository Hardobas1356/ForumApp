using ForumApp.Data;
using ForumApp.Data.Models;
using ForumApp.Services.Core.Interfaces;
using ForumApp.Web.ViewModels.Reply;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using static ForumApp.GCommon.GlobalConstants;

namespace ForumApp.Services.Core;

public class ReplyService : IReplyService
{
    private readonly ForumAppDbContext dbContext;
    private readonly UserManager<ApplicationUser> userManager;

    public ReplyService(ForumAppDbContext dbContext, UserManager<ApplicationUser> userManager)
    {
        this.dbContext = dbContext;
        this.userManager = userManager;
    }

    public async Task<bool> CreateReplyForPostAsync(Guid userId, ReplyCreateInputModel model)
    {
        Post? post = await dbContext
            .Posts
            .SingleOrDefaultAsync(p => p.Id == model.PostId);

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

        await dbContext.Replies.AddAsync(reply);
        await dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<ICollection<ReplyDetailForPostDetailViewModel>?> GetRepliesForPostDetailsAsync(Guid? userId, Guid postId)
    {
        ICollection<ReplyDetailForPostDetailViewModel>? replies = await dbContext
            .Replies
            .Include(r => r.ApplicationUser)
            .AsNoTracking()
            .Where(r => r.PostId == postId)
            .Select(r => new ReplyDetailForPostDetailViewModel
            {
                Id = r.Id,
                Content = r.Content,
                Author = r.ApplicationUser.DisplayName,
                IsPublisher = userId == null ? false : r.ApplicationUser.Id == userId,
                CreatedAt = r.CreatedAt.ToString(DateTimeFormat)
            })
            .ToArrayAsync();

        return replies;
    }

    public async Task<ReplyDeleteViewModel?> GetReplyForDeleteAsync(Guid userId, Guid postId, Guid id)
    {
        Reply? reply = await dbContext
            .Replies
            .Where(r => r.Id == id && r.PostId == postId && r.ApplicationUserId == userId)
            .AsNoTracking()
            .SingleOrDefaultAsync();

        if (reply == null)
        {
            return null;
        }

        ReplyDeleteViewModel model = new ReplyDeleteViewModel()
        {
            Id = id,
            PostId = postId,
            Content = reply.Content,
            CreatedAt = reply.CreatedAt.ToString(DateTimeFormat),
        };

        return model;
    }

    public async Task<bool> SoftDeleteReplyAsync(Guid userId, ReplyDeleteViewModel model)
    {
        Reply? reply = await dbContext
            .Replies
            .SingleOrDefaultAsync(r => r.Id == model.Id 
                && r.PostId == model.PostId && r.ApplicationUserId==userId);

        if (reply == null)
        {
            return false;
        }

        reply.IsDeleted = true;

        await dbContext.SaveChangesAsync();

        return true;
    }
}
