using ForumApp.Data;
using ForumApp.Data.Models;
using ForumApp.Services.Core.Interfaces;
using ForumApp.Web.ViewModels.Post;
using ForumApp.Web.ViewModels.Reply;
using ForumApp.Web.ViewModels.Tag;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static ForumApp.GCommon.GlobalConstants;

namespace ForumApp.Services.Core;

public class PostService : IPostService
{
    private readonly ForumAppDbContext dbContext;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IReplyService replyService;
    private readonly ITagService tagService;

    public PostService(ForumAppDbContext dbContext, UserManager<ApplicationUser> userManager, IReplyService replyService, ITagService tagService)
    {
        this.dbContext = dbContext;
        this.userManager = userManager;
        this.replyService = replyService;
        this.tagService = tagService;
    }

    public async Task<IEnumerable<PostForBoardDetailsViewModel>?> GetPostsForBoardDetailsAsync(Guid boardId)
    {
        IEnumerable<PostForBoardDetailsViewModel>? posts = await dbContext
            .Posts
            .Include(p => p.Board)
            .Include(p => p.PostTags)
            .ThenInclude(p => p.Tag)
            .Where(p => p.BoardId == boardId)
            .Select(p => new PostForBoardDetailsViewModel
            {
                Id = p.Id,
                Title = p.Title,
                CreatedAt = p.CreatedAt.ToString(DateTimeFormat),
                Tags = p.PostTags
                    .Select(pt => new TagViewModel
                    {
                        Id = pt.Tag.Id,
                        Name = pt.Tag.Name,
                        ColorHex = pt.Tag.ColorHex,
                    })
                    .ToArray()
            })
            .ToArrayAsync();

        return posts;
    }
    public async Task<PostEditInputModel?> GetPostForEditAsync(Guid userId, Guid id)
    {
        PostEditInputModel? model = null;

        Post? post = await dbContext
            .Posts
            .Include(post => post.PostTags)
            .ThenInclude(post => post.Tag)
            .AsNoTracking()
            .SingleOrDefaultAsync(p => p.Id == id && p.ApplicationUserId == userId);

        if (post == null)
        {
            return model;
        }

        ICollection<TagViewModel> tags = await tagService
            .GetTagsAsync();

        model = new PostEditInputModel
        {
            Id = id,
            Title = post.Title,
            Content = post.Content,
            ImageUrl = post.ImageUrl,
            AvailableTags = tags,
            Tags = post.PostTags
                .Select(pt => new TagViewModel
                {
                    Id = pt.Tag.Id,
                    Name = pt.Tag.Name,
                    ColorHex = pt.Tag.ColorHex,
                })
                .ToHashSet(),
            TagIds = post.PostTags
                .Select(pt=>pt.TagId)  
                .ToHashSet(),
        };

        return model;
    }
    public async Task<bool> EditPostAsync(Guid userId, PostEditInputModel model)
    {
        Post? post = await dbContext
            .Posts
            .Where(p => p.Id == model.Id)
            .FirstOrDefaultAsync();

        if (post == null || post.ApplicationUserId != userId)
        {
            return false;
        }

        post.Title = model.Title;
        post.Content = model.Content;
        post.ModifiedAt = DateTime.UtcNow;
        post.ImageUrl = model.ImageUrl;

        await dbContext.SaveChangesAsync();
        return true;
    }
    public async Task<PostDetailsViewModel?> GetPostDetailsAsync(Guid? userId, Guid id)
    {
        PostDetailsViewModel? post = await dbContext
            .Posts
            .Include(p => p.Board)
            .Include(p => p.ApplicationUser)
            .Include(p => p.PostTags)
            .ThenInclude(pt => pt.Tag)
            .AsNoTracking()
            .Where(p => p.Id == id)
            .Select(p => new PostDetailsViewModel
            {
                Id = p.Id,
                Title = p.Title,
                Content = p.Content,
                CreatedAt = p.CreatedAt.ToString(DateTimeFormat),
                Author = p.ApplicationUser.DisplayName,
                ImageUrl = p.ImageUrl,
                BoardId = p.BoardId,
                BoardName = p.Board.Name,
                IsPublisher = userId != null && p.ApplicationUserId == userId,
                Tags = p.PostTags
                    .Select(pt => new TagViewModel
                    {
                        Id = pt.Tag.Id,
                        Name = pt.Tag.Name,
                        ColorHex = pt.Tag.ColorHex,
                    })
                    .ToArray()
            })
            .FirstOrDefaultAsync();

        if (post != null)
        {
            post.Replies = await replyService
                .GetRepliesForPostDetailsAsync(userId, post.Id);
        }

        return post;
    }
    public async Task<bool> AddPostAsync(Guid userId, PostCreateInputModel model)
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
            ImageUrl = model.ImageUrl,
            ApplicationUserId = userId,
        };

        await dbContext.AddAsync(post);
        await dbContext.SaveChangesAsync();

        return true;
    }
    public async Task<PostDeleteViewModel?> GetPostForDeleteAsync(Guid userId, Guid id)
    {
        PostDeleteViewModel? post = await dbContext
            .Posts
            .Where(p => p.Id == id && p.ApplicationUserId == userId)
            .AsNoTracking()
            .Select(p => new PostDeleteViewModel
            {
                Id = p.Id,
                Content = p.Content,
                Title = p.Title,
                CreatedAt = p.CreatedAt,
                ImageUrl = p.ImageUrl,
                BoardId = p.BoardId,
            })
            .FirstOrDefaultAsync();

        return post;
    }
    public async Task<bool> DeletePostAsync(Guid userId, PostDeleteViewModel model)
    {
        Post? post = await dbContext
            .Posts
            .Where(p => p.Id == model.Id && p.ApplicationUserId == userId)
            .SingleOrDefaultAsync();

        if (post == null)
        {
            return false;
        }

        bool boardExists =
            await dbContext
            .Boards
            .AnyAsync(b => b.Id == model.BoardId);

        if (!boardExists)
        {
            return false;
        }

        post.IsDeleted = true;

        await dbContext.SaveChangesAsync();

        return true;
    }
}
