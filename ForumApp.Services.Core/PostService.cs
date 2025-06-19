using ForumApp.Data;
using ForumApp.Data.Models;
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
    public async Task<IEnumerable<PostBoardDetailsViewModel>?> GetPostsForBoardDetailsAsync(Guid boardId)
    {
        IEnumerable<PostBoardDetailsViewModel>? posts = await dbContext
            .Posts
            .Include(p => p.Board)
            .Where(p => p.BoardId == boardId)
            .Select(p => new PostBoardDetailsViewModel
            {
                Id = p.Id,
                Title = p.Title,
                CreatedAt = p.CreatedAt.ToString(DateTimeFormat),
            })
            .ToArrayAsync();

        return posts;
    }
    public async Task<bool> EditPostAsync(PostEditInputModel model)
    {
        var post = await dbContext
            .Posts
            .Where(p => p.Id == model.Id)
            .FirstOrDefaultAsync();

        if (post == null)
        {
            return false;
        }

        post.Title = model.Title;
        post.Content = model.Content;
        post.ModifiedAt = DateTime.UtcNow;

        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<PostEditInputModel?> GetPostForEditAsync(Guid id)
    {
        PostEditInputModel? model = null;

        model = await dbContext
            .Posts
            .Where(p => p.Id == id)
            .Select(p => new PostEditInputModel
            {
                Id = id,
                Title = p.Title,
                Content = p.Content,
            })
            .FirstOrDefaultAsync();

        return model;
    }

    public async Task<PostDetailsViewModel?> GetPostDetailsAsync(Guid id)
    {
        var post = await dbContext
            .Posts
            .AsNoTracking()
            .Where(p => p.Id == id)
            .Select(p => new PostDetailsViewModel
            {
                Id = id,
                Title = p.Title,
                Content = p.Content,
                CreatedAt = p.CreatedAt.ToString(DateTimeFormat),
                BoardId = p.BoardId.ToString(),
                BoardName = p.Board.Name,
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

    public async Task<bool> AddPostAsync(PostCreateInputModel model)
    {
        Board? board = await dbContext
            .Boards
            .AsNoTracking()
            .SingleOrDefaultAsync(b => b.Id == model.BoardId);

        if (board == null)
        {
            return false;
        }

        Post post = new Post()
        {
            BoardId = model.BoardId,
            Title = model.Title,
            Content = model.Content,
            CreatedAt = DateTime.UtcNow,
            ModifiedAt = DateTime.UtcNow,
        };

        await dbContext.AddAsync(post);
        await dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<PostDeleteViewModel?> GetPostForDeleteAsync(Guid id)
    {
        PostDeleteViewModel? post = await dbContext
            .Posts
            .Where(p => p.Id == id)
            .AsNoTracking()
            .Select(p => new PostDeleteViewModel
            {
                Id = p.Id,
                Content = p.Content,
                Title = p.Title,
                CreatedAt = p.CreatedAt,
                BoardId = p.BoardId,
            })
            .FirstOrDefaultAsync();

        return post;
    }

    public async Task<bool> DeletePostAsync(PostDeleteViewModel model)
    {
        Post? post = await dbContext
            .Posts
            .Where(p => p.Id == model.Id)
            .SingleOrDefaultAsync();

        Board? board = await dbContext
            .Boards
            .Where(b => b.Id == model.BoardId)
            .SingleOrDefaultAsync();

        if (post == null||board==null)
        {
            return false;
        }

        post.IsDeleted = true;

        await dbContext.SaveChangesAsync();

        return true;
    }
}
