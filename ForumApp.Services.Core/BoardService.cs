using ForumApp.Data.Models;
using ForumApp.GCommon;
using ForumApp.Services.Core.Interfaces;
using ForumApp.Web.ViewModels.Admin.Board;
using ForumApp.Web.ViewModels.Admin.BoardModerators;
using ForumApp.Web.ViewModels.Board;
using ForumApp.Web.ViewModels.Category;
using ForumApp.Web.ViewModels.Post;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using static ForumApp.GCommon.Enums.FilterEnums;
using static ForumApp.GCommon.GlobalConstants;
using static ForumApp.GCommon.Enums.SortEnums.BoardSort;
using static ForumApp.GCommon.Enums.SortEnums.PostSort;

namespace ForumApp.Services.Core;

public class BoardService : IBoardService
{
    private readonly IGenericRepository<BoardManager> boardManagerRepository;
    private readonly IGenericRepository<Board> boardRepository;
    private readonly IPostService postService;
    private readonly ICategoryService categoryService;
    private readonly UserManager<ApplicationUser> userManager;

    public BoardService(IGenericRepository<BoardManager> boardManagerRepository, IGenericRepository<Board> boardRepository,
        IPostService postService, ICategoryService categoryService,
        UserManager<ApplicationUser> userManager)
    {
        this.boardManagerRepository = boardManagerRepository;
        this.boardRepository = boardRepository;
        this.postService = postService;
        this.categoryService = categoryService;
        this.userManager = userManager;
    }

    public async Task<PaginatedResult<BoardAllIndexViewModel>> GetAllBoardsAsync(Guid? userId, BoardAllSortBy sortOrder,
        string? searchTerm, int pageNumber, int pageSize)
    {
        IQueryable<Board> query = boardRepository
            .GetQueryable();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            string loweredTerm = searchTerm.ToLower();
            query = query
                .Where(b => b.Name.ToLower().Contains(loweredTerm)
                            || (b.Description != null && b.Description.ToLower().Contains(loweredTerm))
                            || b.BoardCategories.Any(bc => bc.Category.Name.ToLower().Contains(loweredTerm)));
        }

        var projectedQuery = query
            .Select(b => new
            {
                b.Id,
                b.Name,
                b.ImageUrl,
                b.Description,
                b.CreatedAt,
                PostCount = b.Posts.Count(p => !p.IsDeleted),
                IsModerator = userId != null
                              && b.BoardManagers.Any(m => m.ApplicationUserId == userId),
                Categories = b.BoardCategories
                    .Select(bc => new CategoryViewModel
                    {
                        Id = bc.CategoryId,
                        Name = bc.Category.Name,
                        ColorHex = bc.Category.ColorHex,
                    })
                    .ToArray()
            });

        switch (sortOrder)
        {
            case BoardAllSortBy.None:
                break;
            case BoardAllSortBy.CreateTimeAscending:
                projectedQuery = projectedQuery.OrderBy(b => b.CreatedAt);
                break;
            case BoardAllSortBy.CreateTimeDescending:
                projectedQuery = projectedQuery.OrderByDescending(b => b.CreatedAt);
                break;
            case BoardAllSortBy.NameAscending:
                projectedQuery = projectedQuery.OrderBy(b => b.Name);
                break;
            case BoardAllSortBy.NameDescending:
                projectedQuery = projectedQuery.OrderByDescending(b => b.Name);
                break;
            case BoardAllSortBy.Popularity:
                projectedQuery = projectedQuery.OrderByDescending(b => b.PostCount);
                break;
        }

        IQueryable<BoardAllIndexViewModel> result = projectedQuery
            .Select(b => new BoardAllIndexViewModel
            {
                Id = b.Id,
                Name = b.Name,
                ImageUrl = b.ImageUrl,
                Description = b.Description,
                IsModerator = b.IsModerator,
                Categories = b.Categories
            });

        if (pageNumber < 1)
        {
            pageNumber = 1;
        }

        return await PaginatedResult<BoardAllIndexViewModel>
            .CreateAsync(result, pageNumber, pageSize);
    }
    public async Task<PaginatedResult<BoardAdminViewModel>> GetAllBoardsForAdminAsync(BoardAdminFilter filter,
        BoardAllSortBy sortOrder, string? searchTerm, int pageNumber, int pageSize)
    {
        IQueryable<Board> query = boardRepository.GetQueryable(ignoreQueryFilters: true);

        query = filter switch
        {
            BoardAdminFilter.Pending => query.Where(b => !b.IsApproved),
            BoardAdminFilter.Approved => query.Where(b => b.IsApproved && !b.IsDeleted),
            BoardAdminFilter.Deleted => query.Where(b => b.IsDeleted),
            _ => query
        };

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            string loweredTerm = searchTerm.ToLower();
            query = query.Where(b =>
                b.Name.ToLower().Contains(loweredTerm) ||
                (b.Description != null && b.Description.ToLower().Contains(loweredTerm)));
        }

        switch (sortOrder)
        {
            case BoardAllSortBy.None:
                break;
            case BoardAllSortBy.CreateTimeAscending:
                query = query.OrderBy(b => b.CreatedAt);
                break;
            case BoardAllSortBy.CreateTimeDescending:
                query = query.OrderByDescending(b => b.CreatedAt);
                break;
            case BoardAllSortBy.NameAscending:
                query = query.OrderBy(b => b.Name);
                break;
            case BoardAllSortBy.NameDescending:
                query = query.OrderByDescending(b => b.Name);
                break;
            case BoardAllSortBy.Popularity:
                query = query.OrderByDescending(b => b.Posts.Count);
                break;
        }

        var source = query
                .Select(b => new BoardAdminViewModel
                {
                    Id = b.Id,
                    Name = b.Name,
                    Description = b.Description,
                    IsApproved = b.IsApproved,
                    IsDeleted = b.IsDeleted,
                });

        return await PaginatedResult<BoardAdminViewModel>.CreateAsync(source, pageNumber, pageSize);
    }
    public async Task<BoardDetailsViewModel> GetBoardDetailsAsync(Guid boardId, PostSortBy sortOrder,
        string? searchTerm, int pageNumber, int pageSize)
    {
        if (boardId == Guid.Empty)
        {
            throw new ArgumentException("BoardId is null", nameof(boardId));
        }

        Board? board = await boardRepository
            .SingleOrDefaultAsync(b => b.Id == boardId);

        if (board == null)
        {
            throw new ArgumentException("Board not found", nameof(boardId));
        }

        PaginatedResult<PostForBoardDetailsViewModel>? posts =
            await postService.GetPostsForBoardDetailsAsync(boardId, sortOrder, searchTerm, pageNumber, pageSize);

        IEnumerable<CategoryViewModel>? categories =
            await categoryService.GetCategoriesAsyncByBoardId(boardId);

        return new BoardDetailsViewModel
        {
            Id = board.Id,
            Name = board.Name,
            ImageUrl = board.ImageUrl,
            Description = board.Description,
            CreatedAt = board.CreatedAt.ToString(APPLICATION_DATE_TIME_FORMAT),
            Posts = posts,
            Categories = categories ?? new HashSet<CategoryViewModel>(),
        };
    }
    public async Task<BoardDetailsAdminViewModel> GetBoardDetailsAdminAsync(Guid boardId, PostSortBy sortBy, int pageNumber, int pageSize)
    {
        if (boardId == Guid.Empty)
        {
            throw new ArgumentException("BoardId is null", nameof(boardId));
        }

        Board? board = await boardRepository
            .SingleOrDefaultWithIncludeAsync(b => b.Id == boardId,
                                             q => q.Include(b => b.BoardManagers)
                                                   .ThenInclude(bm => bm.ApplicationUser),
                                             ignoreQueryFilters: true);
        if (board == null)
        {
            throw new ArgumentException("Board not found with Id", nameof(boardId));
        }

        PaginatedResult<PostForBoardDetailsViewModel>? posts =
            await postService.GetPostsForBoardDetailsAsync(boardId, sortBy, null, pageNumber, pageSize);

        IEnumerable<CategoryViewModel>? categories =
            await categoryService.GetCategoriesAsyncByBoardId(boardId);

        BoardDetailsAdminViewModel model = new BoardDetailsAdminViewModel
        {
            Id = board.Id,
            Name = board.Name,
            ImageUrl = board.ImageUrl,
            Description = board.Description,
            CreatedAt = board.CreatedAt.ToString(APPLICATION_DATE_TIME_FORMAT),
            Posts = posts,
            Categories = categories ?? new HashSet<CategoryViewModel>(),
            Moderators = board.BoardManagers
                .Where(bm => !bm.IsDeleted)
                .Select(bm => new BoardModeratorViewModel
                {
                    Id = bm.ApplicationUserId,
                    DisplayName = bm.ApplicationUser == null
                            ? DeletedUser.DELETED_DISPLAYNAME : bm.ApplicationUser.DisplayName!,
                    Handle = bm.ApplicationUser == null
                            ? DeletedUser.DELETED_USERNAME : bm.ApplicationUser.UserName!,
                })
                .ToArray()
        };

        return model;
    }
    public async Task<BoardDeleteViewModel> GetBoardForDeletionAsync(Guid boardId)
    {
        if (boardId == Guid.Empty)
        {
            throw new ArgumentException("BoardId is null", nameof(boardId));
        }

        Board? board = await boardRepository
            .GetByIdAsync(boardId);

        if (board == null)
        {
            throw new ArgumentException("Board not found with Id", nameof(boardId));
        }

        BoardDeleteViewModel model = new BoardDeleteViewModel()
        {
            Id = boardId,
            Name = board.Name,
            ImageUrl = board.ImageUrl,

        };

        return model;
    }
    public async Task RestoreBoardAsync(Guid boardId)
    {
        if (boardId == Guid.Empty)
        {
            throw new ArgumentException("BoardId is null", nameof(boardId));
        }

        Board? board = await boardRepository
            .GetByIdAsync(boardId,
                          asNoTracking: false,
                          ignoreQueryFilters: true);

        if (board == null)
        {
            throw new ArgumentException("Board not found with Id", nameof(boardId));
        }

        try
        {
            board.IsDeleted = false;
            await boardRepository.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception("Failed to restore board", e);
        }
    }
    public async Task ApproveBoardAsync(Guid boardId)
    {
        if (boardId == Guid.Empty)
        {
            throw new ArgumentException("BoardId is null", nameof(boardId));
        }

        Board? board = await boardRepository
            .GetByIdAsync(boardId,
                          asNoTracking: false,
                          ignoreQueryFilters: true);

        if (board == null)
        {
            throw new ArgumentException("Board not found", nameof(boardId));
        }

        try
        {
            board.IsApproved = true;
            await boardRepository.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception("Failed to approve board", e);
        }
    }
    public async Task SoftDeleteBoardAsync(BoardDeleteViewModel model)
    {
        Board? board = await boardRepository
            .GetByIdAsync(model.Id,
                          asNoTracking: false);

        if (board == null)
        {
            throw new ArgumentException("Board not found", nameof(model.Id));
        }

        try
        {
            board.IsDeleted = true;
            await boardRepository.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception("Failed to soft delete board", e);
        }
    }
    public async Task CreateBoardAsync(Guid userId, BoardCreateInputModel model)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("User ID cannot be empty", nameof(userId));

        if (model == null)
            throw new ArgumentNullException(nameof(model));

        if (string.IsNullOrWhiteSpace(model.Name))
            throw new ArgumentException("Board name is required", nameof(model));

        if (!string.IsNullOrWhiteSpace(model.ImageUrl))
        {
            if (!Uri.TryCreate(model.ImageUrl, UriKind.Absolute, out Uri? uriResult) ||
                !(uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
            {
                throw new ArgumentException("Image URL is not a valid HTTP or HTTPS URL", nameof(model.ImageUrl));
            }
        }

        IEnumerable<CategoryViewModel> categories = await categoryService
            .GetCategoriesAsync();

        HashSet<Guid> validCategoryIds = categories
            .Select(c => c.Id).ToHashSet();

        List<Guid> invalidCategoryIds = model.SelectedCategoryIds
            .Where(id => !validCategoryIds.Contains(id))
            .ToList();

        if (invalidCategoryIds.Any())
            throw new ArgumentException($"Invalid category IDs: {string.Join(", ", invalidCategoryIds)}",
                nameof(model.SelectedCategoryIds));

        Board board = new Board
        {
            Id = Guid.NewGuid(),
            Name = model.Name,
            Description = model.Description,
            ImageUrl = model.ImageUrl,
            CreatedAt = DateTime.UtcNow,
        };

        BoardManager manager = new BoardManager
        {
            BoardId = board.Id,
            ApplicationUserId = userId,
        };

        board.BoardManagers.Add(manager);

        foreach (var categoryId in model.SelectedCategoryIds)
        {
            board.BoardCategories.Add(new BoardCategory
            {
                BoardId = board.Id,
                CategoryId = categoryId
            });
        }

        try
        {
            await boardRepository.AddAsync(board);
            await boardRepository.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception("Failed to create board in database", e);
        }
    }
    public async Task<string> GetBoardNameByIdAsync(Guid boardId)
    {
        if (boardId == Guid.Empty)
        {
            throw new ArgumentException("BoardId is null", nameof(boardId));
        }

        Board? board = await boardRepository.GetByIdAsync(boardId, ignoreQueryFilters: true);

        if (board == null)
        {
            throw new ArgumentException("Board not found", nameof(boardId));
        }

        return board.Name;
    }
    public async Task AddModeratorAsync(Guid userId, Guid boardId)
    {
        if (boardId == Guid.Empty)
        {
            throw new ArgumentException("BoardId is null", nameof(boardId));
        }

        Board? board = await boardRepository
            .SingleOrDefaultAsync(b => b.Id == boardId,
                                  ignoreQueryFilters: true);

        if (board == null)
        {
            throw new ArgumentException("Board not found", nameof(boardId));
        }

        ApplicationUser? user = await userManager
            .FindByIdAsync(userId.ToString());

        if (user == null)
        {
            throw new ArgumentException("User not found", nameof(userId));
        }

        BoardManager? boardManager = await boardManagerRepository
            .SingleOrDefaultAsync(bm => bm.BoardId == boardId
                                  && bm.ApplicationUserId == userId,
                                  ignoreQueryFilters: true,
                                  asNoTracking: false);

        if (boardManager == null)
        {
            boardManager = new BoardManager()
            {
                ApplicationUserId = userId,
                BoardId = boardId,
                IsDeleted = false
            };
            await boardManagerRepository.AddAsync(boardManager);
        }
        else
        {
            if (!boardManager.IsDeleted)
            {
                throw new InvalidOperationException("Board manager already exists");
            }

            boardManager.IsDeleted = false;
        }

        try
        {
            await boardManagerRepository.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception("Failed to save db", e);
        }
    }
    public async Task RemoveModeratorAsync(Guid userId, Guid boardId)
    {
        if (boardId == Guid.Empty || userId == Guid.Empty)
        {
            throw new ArgumentException("User ID and Board ID must be provided.");
        }

        BoardManager? boardManager = await boardManagerRepository
            .SingleOrDefaultAsync(bm => bm.BoardId == boardId
                                  && bm.ApplicationUserId == userId,
                                  ignoreQueryFilters: true,
                                  asNoTracking: false);

        if (boardManager == null)
        {
            throw new InvalidOperationException("Moderator relationship not found.");
        }

        if (boardManager.IsDeleted == true)
        {
            throw new InvalidOperationException("Moderator already deleted.");
        }

        boardManager.IsDeleted = true;

        try
        {
            await boardManagerRepository.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception("Failed to remove moderator", e);
        }
    }
}
