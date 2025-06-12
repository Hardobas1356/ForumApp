using ForumApp.Data;
using ForumApp.Services.Core.Interfaces;
using ForumApp.Web.ViewModels.Post;
using ForumApp.Web.ViewModels.Reply;
using Microsoft.EntityFrameworkCore;

using static ForumApp.GCommon.GlobalConstants;

namespace ForumApp.Services.Core;

public class PostService : IPostService
{
    private readonly ForumAppDbContext dbContext;

    public PostService(ForumAppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<bool> EditPostAsync(EditPostViewModel model)
    {
        var post = await dbContext
            .Posts
            .Where(p => p.Id == model.Id && !p.IsDeleted)
            .FirstOrDefaultAsync();

        if (post == null)
        {
            return false;
        }

        post.Title = model.Title;
        post.Content = model.Content;
        post.ModifiedAt = DateTime.Now;

        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<EditPostViewModel?> GetPostForEditAsync(int id)
    {
        EditPostViewModel? model = null;

        model = await dbContext
            .Posts
            .Where(p => !p.IsDeleted && p.Id == id)
            .Select(p => new EditPostViewModel
            {
                Id = p.Id,
                Title = p.Title,
                Content = p.Content,
            })
            .FirstOrDefaultAsync();

        return model;
    }

    public async Task<PostDetailsViewModel?> GetPostDetailsAsync(int id)
    {
        var post = await dbContext
            .Posts
            .AsNoTracking()
            .Where(p => p.Id == id && !p.IsDeleted)
            .Select(p => new PostDetailsViewModel
            {
                Id = p.Id,
                Title = p.Title,
                Content = p.Content,
                CreatedAt = p.CreatedAt.ToString(DateTimeFormat),
                BoardId = p.BoardId,
                BoardName = dbContext
                                .Boards
                                .AsNoTracking()
                                .Where(b => b.Id == p.BoardId && !b.IsDeleted)
                                .Select(b => b.Name)
                                .First(),
                Replies = p
                                .Replies
                                .Select(r => new ReplyDetailForPostDetailViewModel
                                {
                                    Id = r.Id,
                                    Content = r.Content,
                                    CreatedAt = r.CreatedAt.ToString(DateTimeFormat)
                                })
                                .ToList()
            })
            .FirstOrDefaultAsync();

        return post;
    }
}
