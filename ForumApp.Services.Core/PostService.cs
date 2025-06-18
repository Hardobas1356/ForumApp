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

    public async Task<bool> EditPostAsync(EditPostInputModel model)
    {
        if (!Guid.TryParse(model.Id, out Guid id))
        {
            return false;
        }

        var post = await dbContext
            .Posts
            .Where(p => p.Id == id && !p.IsDeleted)
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

    public async Task<EditPostInputModel?> GetPostForEditAsync(string id)
    {
        EditPostInputModel? model = null;

        if (!Guid.TryParse(id, out Guid guidId))
        {
            return model;
        }

        model = await dbContext
            .Posts
            .Where(p => !p.IsDeleted && p.Id == guidId)
            .Select(p => new EditPostInputModel
            {
                Id = id,
                Title = p.Title,
                Content = p.Content,
            })
            .FirstOrDefaultAsync();

        return model;
    }

    public async Task<PostDetailsViewModel?> GetPostDetailsAsync(string id)
    {
        if (!Guid.TryParse(id, out Guid guidId))
        {
            return null;
        }

        var post = await dbContext
            .Posts
            .AsNoTracking()
            .Where(p => p.Id == guidId && !p.IsDeleted)
            .Select(p => new PostDetailsViewModel
            {
                Id = id,
                Title = p.Title,
                Content = p.Content,
                CreatedAt = p.CreatedAt.ToString(DateTimeFormat),
                BoardId = p.BoardId.ToString(),
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
                                    Id = r.Id.ToString(),
                                    Content = r.Content,
                                    CreatedAt = r.CreatedAt.ToString(DateTimeFormat)
                                })
                                .ToList()
            })
            .FirstOrDefaultAsync();

        return post;
    }
}
