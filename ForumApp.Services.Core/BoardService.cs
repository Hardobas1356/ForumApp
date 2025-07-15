using ForumApp.Data.Models;
using ForumApp.Services.Core.Interfaces;
using ForumApp.Web.ViewModels.Admin.Board;
using ForumApp.Web.ViewModels.Admin.BoardModerators;
using ForumApp.Web.ViewModels.Board;
using ForumApp.Web.ViewModels.Category;
using ForumApp.Web.ViewModels.Post;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static ForumApp.GCommon.FilterEnums;

using static ForumApp.GCommon.GlobalConstants;

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

    public async Task<IEnumerable<BoardAllIndexViewModel>> GetAllBoardsAsync(Guid? userId)
    {
        IEnumerable<Board> boards = await boardRepository
            .GetAllWithIncludeAsync(q => q
                                    .Include(b => b.BoardManagers)
                                    .Include(b => b.BoardCategories)
                                    .ThenInclude(bc => bc.Category));

        return boards
            .Select(b => new BoardAllIndexViewModel
            {
                Id = b.Id,
                Name = b.Name,
                ImageUrl = b.ImageUrl,
                Description = b.Description,
                IsModerator = userId != null && b.BoardManagers
                    .Any(m => m.ApplicationUserId == userId),
                Categories = b.BoardCategories
                    .Select(bc => new CategoryViewModel
                    {
                        Id = bc.CategoryId,
                        Name = bc.Category.Name,
                        ColorHex = bc.Category.ColorHex,
                    })
                    .ToArray()
            })
            .ToArray();
    }
    public async Task<IEnumerable<BoardAdminViewModel>?> GetAllBoardsForAdminAsync(BoardAdminFilter filter)
    {
        IQueryable<Board> query = boardRepository.GetQueryable(ignoreQueryFilters: true);

        query = filter switch
        {
            BoardAdminFilter.Pending => query.Where(b => !b.IsApproved),
            BoardAdminFilter.Approved => query.Where(b => b.IsApproved),
            BoardAdminFilter.Deleted => query.Where(b => b.IsDeleted),
            _ => query
        };

        IEnumerable<BoardAdminViewModel> boards = await query
                .Select(b => new BoardAdminViewModel
                {
                    Id = b.Id,
                    Name = b.Name,
                    Description = b.Description,
                    IsApproved = b.IsApproved,
                    IsDeleted = b.IsDeleted,
                })
                .ToListAsync();

        return boards;
    }
    public async Task<BoardDetailsViewModel?> GetBoardDetailsAsync(Guid boardId)
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

        IEnumerable<PostForBoardDetailsViewModel>? posts =
            await postService.GetPostsForBoardDetailsAsync(boardId);

        IEnumerable<CategoryViewModel>? categories =
            await categoryService.GetCategoriesAsyncByBoardId(boardId);

        return new BoardDetailsViewModel
        {
            Id = board.Id,
            Name = board.Name,
            ImageUrl = board.ImageUrl,
            Description = board.Description,
            CreatedAt = board.CreatedAt.ToString(ApplicationDateTimeFormat),
            Posts = posts ?? new HashSet<PostForBoardDetailsViewModel>(),
            Categories = categories ?? new HashSet<CategoryViewModel>(),
        };
    }
    public async Task<BoardDetailsAdminViewModel?> GetBoardDetailsAdminAsync(Guid boardId)
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

        IEnumerable<PostForBoardDetailsViewModel>? posts =
            await postService.GetPostsForBoardDetailsAsync(boardId);

        IEnumerable<CategoryViewModel>? categories =
            await categoryService.GetCategoriesAsyncByBoardId(boardId);

        BoardDetailsAdminViewModel model = new BoardDetailsAdminViewModel
        {
            Id = board.Id,
            Name = board.Name,
            ImageUrl = board.ImageUrl,
            Description = board.Description,
            CreatedAt = board.CreatedAt.ToString(ApplicationDateTimeFormat),
            Posts = posts ?? new HashSet<PostForBoardDetailsViewModel>(),
            Categories = categories ?? new HashSet<CategoryViewModel>(),
            Moderators = board.BoardManagers
                .Where(bm => !bm.IsDeleted)
                .Select(bm => new BoardModeratorViewModel
                {
                    Id = bm.ApplicationUserId,
                    DisplayName = bm.ApplicationUser.DisplayName,
                    Handle = bm.ApplicationUser.UserName ?? "Unknown",
                })
                .ToArray()
        };

        return model;
    }
    public async Task<BoardDeleteViewModel?> GetBoardForDeletionAsync(Guid boardId)
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
    public async Task<bool> RestoreBoardAsync(Guid boardId)
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

        board.IsDeleted = false;
        await boardRepository.SaveChangesAsync();

        return true;
    }
    public async Task<bool> ApproveBoardAsync(Guid boardId)
    {
        if (boardId == Guid.Empty)
        {
            throw new ArgumentException("BoardId is null",nameof(boardId));
        }

        Board? board = await boardRepository
            .GetByIdAsync(boardId,
                          asNoTracking: false,
                          ignoreQueryFilters: true);

        if (board == null)
        {
            throw new ArgumentException("Board not found", nameof(boardId));
        }

        board.IsApproved = true;

        await boardRepository.SaveChangesAsync();

        return true;
    }
    public async Task<bool> SoftDeleteBoardAsync(BoardDeleteViewModel model)
    {
        Board? board = await boardRepository
            .GetByIdAsync(model.Id,
                          asNoTracking: false);

        if (board == null)
        {
            throw new ArgumentException("Board not found", nameof(model.Id));
        }

        board.IsDeleted = true;
        await boardRepository.SaveChangesAsync();

        return true;
    }
    public async Task<bool> CreateBoardAsync(Guid userId, BoardCreateInputModel model)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("User ID cannot be empty", nameof(userId));

        if (model == null)
            throw new ArgumentNullException(nameof(model));

        if (string.IsNullOrWhiteSpace(model.Name))
            throw new ArgumentException("Board name is required", nameof(model));

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
            return true;
        }
        catch (Exception e)
        {
            throw new Exception("Failed to create board in database", e);
        }
    }
    public async Task<string?> GetBoardNameByIdAsync(Guid boardId)
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
    public async Task<bool> AddModeratorAsync(Guid userId, Guid boardId)
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

        ApplicationUser? user = await userManager
            .FindByIdAsync(userId.ToString());

        if (board == null)
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
                return false;
            }

            boardManager.IsDeleted = false;
        }

        await boardManagerRepository.SaveChangesAsync();

        return true;
    }
    public async Task<bool> RemoveModeratorAsync(Guid userId, Guid boardId)
    {
        if (boardId == Guid.Empty || userId == Guid.Empty)
        {
            throw new ArgumentException("User ID and Board ID must be provided.");
        }

        BoardManager? boardManager = await boardManagerRepository
            .SingleOrDefaultAsync(bm => bm.BoardId == boardId
                                  && bm.ApplicationUserId == userId,
                                  asNoTracking: false);

        if (boardManager == null)
        {
            throw new InvalidOperationException("Moderator relationship not found.");
        }

        boardManager.IsDeleted = true;

        await boardManagerRepository.SaveChangesAsync();

        return true;
    }
}
