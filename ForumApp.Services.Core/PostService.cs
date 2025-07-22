using ForumApp.Data.Models;
using ForumApp.GCommon;
using ForumApp.Services.Core.Interfaces;
using ForumApp.Web.ViewModels.Post;
using ForumApp.Web.ViewModels.Tag;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using static ForumApp.GCommon.GlobalConstants;
using static ForumApp.GCommon.Enums.SortEnums.Post;
using static ForumApp.GCommon.Enums.SortEnums.Reply;

namespace ForumApp.Services.Core;

public class PostService : IPostService
{
    private readonly IGenericRepository<Post> postRepository;
    private readonly IGenericRepository<Board> boardRepository;
    private readonly IGenericRepository<PostTag> postTagRepository;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IReplyService replyService;
    private readonly ITagService tagService;
    private readonly IPermissionService permissionService;


    public PostService(
        IGenericRepository<Post> postRepository,
        IGenericRepository<Board> boardRepository,
        IGenericRepository<PostTag> postTagRepository,
        UserManager<ApplicationUser> userManager,
        IReplyService replyService,
        ITagService tagService,
        IPermissionService permissionService)
    {
        this.postRepository = postRepository;
        this.boardRepository = boardRepository;
        this.postTagRepository = postTagRepository;
        this.userManager = userManager;
        this.replyService = replyService;
        this.tagService = tagService;
        this.permissionService = permissionService;
    }

    public async Task<PaginatedResult<PostForBoardDetailsViewModel>?> GetPostsForBoardDetailsAsync(Guid boardId,
        PostSortBy sortOrder, string? searchTerm,
        int pageNumber, int pageSize)
    {
        IQueryable<Post> query = postRepository
            .GetQueryable()
            .Where(p => p.BoardId == boardId)
            .Include(p => p.Replies)
            .Include(p => p.ApplicationUser)
            .Include(p => p.Board)
            .Include(p => p.PostTags)
            .ThenInclude(pt => pt.Tag);

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            string loweredTerm = searchTerm.ToLower();

            query = query.Where(p => p.Title.ToLower().Contains(loweredTerm)
                                     || p.Content.ToLower().Contains(loweredTerm)
                                     || p.PostTags.Any(pt => pt.Tag.Name.ToLower().Contains(loweredTerm)));
        }

        switch (sortOrder)
        {
            case PostSortBy.CreateTimeAscending:
                query = query.OrderBy(p => p.CreatedAt);
                break;
            case PostSortBy.CreateTimeDescending:
                query = query.OrderByDescending(p => p.CreatedAt);
                break;
            case PostSortBy.TitleAscending:
                query = query.OrderBy(p => p.Title);
                break;
            case PostSortBy.TitleDescending:
                query = query.OrderByDescending(p => p.Title);
                break;
            case PostSortBy.Popularity:
                query = query.OrderByDescending(p => p.Replies.Count);
                break;
            case PostSortBy.Default:
                query = query.OrderByDescending(p => p.IsPinned);
                break;
        }

        IQueryable<PostForBoardDetailsViewModel> posts = query
            .Select(p => new PostForBoardDetailsViewModel
            {
                Id = p.Id,
                Title = p.Title,
                CreatedAt = p.CreatedAt.ToString(ApplicationDateTimeFormat),
                Author = p.ApplicationUser.DisplayName,
                Handle = p.ApplicationUser.UserName ?? "Unknown",
                Tags = p.PostTags
                        .Select(pt => new TagViewModel
                        {
                            Id = pt.Tag.Id,
                            Name = pt.Tag.Name,
                            ColorHex = pt.Tag.ColorHex,
                        })
                        .ToArray()
            });

        return await PaginatedResult<PostForBoardDetailsViewModel>.CreateAsync(posts, pageNumber, pageSize);
    }

    //Only authors can edit their own posts — not admins or mods.
    public async Task<PostEditInputModel?> GetPostForEditAsync(Guid userId, Guid id)
    {
        Post? post = await postRepository
            .SingleOrDefaultWithIncludeAsync(p => p.ApplicationUserId == userId
                                                  && p.Id == id,
                                             q => q.Include(p => p.Board)
                                                   .Include(p => p.PostTags)
                                                   .ThenInclude(pt => pt.Tag),
                                             asNoTracking: true);

        if (post == null)
        {
            return null;
        }

        ICollection<TagViewModel> tags = await tagService
            .GetTagsAsync();

        return new PostEditInputModel
        {
            Id = id,
            Title = post.Title,
            Content = post.Content,
            ImageUrl = post.ImageUrl,
            AvailableTags = tags,
            BoardId = post.BoardId,
            BoardName = post.Board.Name,
            Tags = post.PostTags
                .Select(pt => new TagViewModel
                {
                    Id = pt.Tag.Id,
                    Name = pt.Tag.Name,
                    ColorHex = pt.Tag.ColorHex,
                })
                .ToHashSet(),
            TagIds = post.PostTags
                .Select(pt => pt.TagId)
                .ToHashSet(),
        };
    }
    //Only authors can edit their own posts — not admins or mods.
    public async Task<bool> EditPostAsync(Guid userId, PostEditInputModel model)
    {
        Post? post = await postRepository
            .SingleOrDefaultWithIncludeAsync(p => p.Id == model.Id
                                                && p.ApplicationUserId == userId,
                                             q => q.Include(p => p.PostTags)
                                                   .ThenInclude(pt => pt.Tag),
                                             asNoTracking: false);

        if (post == null)
        {
            return false;
        }

        post.Title = model.Title;
        post.Content = model.Content;
        post.ModifiedAt = DateTime.UtcNow;
        post.ImageUrl = model.ImageUrl;

        postTagRepository.DeleteRange(post.PostTags);

        if (model.TagIds != null)
        {
            foreach (Guid tagId in model.TagIds)
            {
                post.PostTags.Add(new PostTag
                {
                    PostId = post.Id,
                    TagId = tagId
                });
            }
        }

        await postRepository.SaveChangesAsync();
        return true;
    }
    public async Task<PostDetailsViewModel?> GetPostDetailsAsync(Guid? userId, Guid id,
        ReplySortBy sortBy, int pageNumber, int pageSize)
    {
        Post? post = await postRepository
            .SingleOrDefaultWithIncludeAsync(p => p.Id == id,
                                             q => q.Include(p => p.Board)
                                                   .ThenInclude(b => b.BoardManagers)
                                                   .Include(p => p.ApplicationUser)
                                                   .Include(p => p.PostTags)
                                                   .ThenInclude(pt => pt.Tag));
        if (post == null)
        {
            return null;
        }

        bool canModerate = userId != null ? await permissionService.CanManagePostAsync((Guid)userId, post.Id) : false;

        PostDetailsViewModel model = new PostDetailsViewModel
        {
            Id = post.Id,
            Title = post.Title,
            Content = post.Content,
            CreatedAt = post.CreatedAt.ToString(ApplicationDateTimeFormat),
            ModifiedAt = post.ModifiedAt.ToString(ApplicationDateTimeFormat),
            Author = post.ApplicationUser.DisplayName,
            ImageUrl = post.ImageUrl,
            BoardId = post.BoardId,
            BoardName = post.Board.Name,
            CanModerate = canModerate,
            IsPublisher = userId != null && post.ApplicationUserId == userId,
            Tags = post.PostTags
                       .Select(pt => new TagViewModel
                       {
                           Id = pt.Tag.Id,
                           Name = pt.Tag.Name,
                           ColorHex = pt.Tag.ColorHex,
                       })
                       .ToArray(),
            Replies = await replyService
                            .GetRepliesForPostDetailsAsync(userId, post.Id,
                                canModerate, sortBy, pageNumber, pageSize)
        };

        return model;
    }
    public async Task<bool> AddPostAsync(Guid userId, PostCreateInputModel model)
    {
        bool boardExists = await boardRepository
            .AnyAsync(b => b.Id == model.BoardId);

        if (!boardExists)
        {
            return false;
        }

        Post post = new Post()
        {
            Id = Guid.NewGuid(),
            BoardId = model.BoardId,
            Title = model.Title,
            Content = model.Content,
            CreatedAt = DateTime.UtcNow,
            ModifiedAt = DateTime.UtcNow,
            ImageUrl = model.ImageUrl,
            ApplicationUserId = userId,
        };

        await postRepository.AddAsync(post);

        if (model.TagIds != null)
        {
            foreach (Guid tagId in model.TagIds)
            {
                await postTagRepository.AddAsync(new PostTag
                {
                    PostId = post.Id,
                    TagId = tagId
                });
            }
        }

        await postRepository.SaveChangesAsync();

        return true;
    }
    public async Task<PostDeleteViewModel?> GetPostForDeleteAsync(Guid userId, Guid id)
    {
        Post? post = await postRepository
            .SingleOrDefaultWithIncludeAsync(p => p.Id == id,
                                             q => q.Include(p => p.Board)
                                                   .ThenInclude(b => b.BoardManagers));

        if (post == null || post.Board == null)
        {
            return null;
        }

        bool canDelete = await permissionService.CanManagePostAsync(userId, post.Id);

        if (!canDelete)
        {
            return null;
        }

        PostDeleteViewModel? model = new PostDeleteViewModel
        {
            Id = post.Id,
            Content = post.Content,
            Title = post.Title,
            CreatedAt = post.CreatedAt.ToString(ApplicationDateTimeFormat),
            ImageUrl = post.ImageUrl,
            BoardId = post.BoardId,
            BoardName = post.Board.Name,
        };

        return model;
    }
    public async Task<bool> DeletePostAsync(Guid userId, PostDeleteViewModel model)
    {
        Post? post = await postRepository
            .SingleOrDefaultWithIncludeAsync(p => p.Id == model.Id,
                                             q => q.Include(p => p.Board)
                                                   .ThenInclude(b => b.BoardManagers),
                                             asNoTracking: false);

        if (post == null)
        {
            return false;
        }

        bool canDelete = await permissionService.CanManagePostAsync(userId, post.Id);

        if (!canDelete)
        {
            return false;
        }

        bool boardExists =
            await boardRepository
            .AnyAsync(b => b.Id == model.BoardId);

        if (!boardExists)
        {
            return false;
        }

        post.IsDeleted = true;

        await postRepository.SaveChangesAsync();

        return true;
    }
}
