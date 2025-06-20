using ForumApp.Data;
using ForumApp.Data.Models;
using ForumApp.Services.Core.Interfaces;
using ForumApp.Web.ViewModels.Reply;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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

    public async Task<bool> CreateReplyForPost(Guid userId, ReplyCreateInputModel model)
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
}
