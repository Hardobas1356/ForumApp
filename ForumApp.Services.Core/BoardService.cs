using ForumApp.Data.Models;
using ForumApp.Services.Core.Interfaces;
using ForumApp.Web.ViewModels.Admin.Board;
using ForumApp.Web.ViewModels.Board;
using ForumApp.Web.ViewModels.Category;
using ForumApp.Web.ViewModels.Post;
using Microsoft.EntityFrameworkCore;
using static ForumApp.GCommon.FilterEnums;

using static ForumApp.GCommon.GlobalConstants;

namespace ForumApp.Services.Core;

public class BoardService : IBoardService
{
    private readonly IGenericRepository<Board> boardRepository;
    private readonly IPostService postService;
    private readonly ICategoryService categoryService;

    public BoardService(IGenericRepository<Board> boardRepository, IPostService postService,
        ICategoryService categoryService)
    {
        this.boardRepository = boardRepository;
        this.postService = postService;
        this.categoryService = categoryService;
    }

    public async Task<IEnumerable<BoardAllIndexViewModel>> GetAllBoardsAsync()
    {
        IEnumerable<Board> boards = await boardRepository
            .GetAllWithInludeAsync(q => q
                                    .Include(b => b.BoardCategories)
                                    .ThenInclude(bc => bc.Category));

        return boards
            .Select(b => new BoardAllIndexViewModel
            {
                Id = b.Id,
                Name = b.Name,
                ImageUrl = b.ImageUrl,
                Description = b.Description,
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
        IEnumerable<Board>? boards = null;

        switch (filter)
        {
            case BoardAdminFilter.All:
                boards = await boardRepository
                    .GetAllAsync(ignoreQueryFilters: true);
                break;
            case BoardAdminFilter.Pending:
                boards = await boardRepository
                    .GetWhereAsync(b => !b.IsApproved,
                                   ignoreQueryFilters: true);
                break;
            case BoardAdminFilter.Approved:
                boards = await boardRepository
                    .GetWhereAsync(b => b.IsApproved,
                                   ignoreQueryFilters: true);
                break;
            case BoardAdminFilter.Deleted:
                boards = await boardRepository
                    .GetWhereAsync(b => b.IsDeleted,
                                   ignoreQueryFilters: true);
                break;
        }

        IEnumerable<BoardAdminViewModel> result = boards!
            .Select(b => new BoardAdminViewModel
            {
                Id = b.Id,
                Name = b.Name,
                Description = b.Description,
                IsApproved = b.IsApproved,
                IsDeleted = b.IsDeleted,
            });

        return result;
    }

    public async Task<BoardDetailsViewModel?> GetBoardDetailsAsync(Guid boardId)
    {
        Board? board = await boardRepository
            .SingleOrDefaultAsync(b => b.Id == boardId);

        if (board == null)
        {
            return null;
        }

        IEnumerable<PostForBoardDetailsViewModel>? posts =
            await postService.GetPostsForBoardDetailsAsync(boardId);

        ICollection<CategoryViewModel>? categories =
            await categoryService.GetCategoriesAsyncByBoardId(boardId);

        return new BoardDetailsViewModel
        {
            Id = board.Id,
            Name = board.Name,
            ImageUrl = board.ImageUrl,
            Description = board.Description,
            CreatedAt = board.CreatedAt.ToString(ApplicationDateTimeFormat),
            Posts = posts?.ToHashSet(),
            Categories = categories?.ToHashSet(),
        };
    }

    public async Task<BoardDeleteViewModel?> GetBoardForDeletionAsync(Guid id)
    {
        Board? board = await boardRepository
            .GetByIdAsync(id);

        if (board == null)
        {
            return null;
        }

        BoardDeleteViewModel model = new BoardDeleteViewModel()
        {
            Id = id,
            Name = board.Name,
            ImageUrl = board.ImageUrl,

        };

        return model;
    }

    public async Task<bool> RestoreBoardAsync(Guid id)
    {
        Board? board = await boardRepository
            .GetByIdAsync(id,
                          asNoTracking: false,
                          ignoreQueryFilters: true);

        if (board == null)
        {
            return false;
        }

        board.IsDeleted = false;
        await boardRepository.SaveChangesAsync();

        return true;
    }
    public async Task<bool> ApproveBoardAsync(Guid id)
    {
        Board? board = await boardRepository
            .GetByIdAsync(id,
                          asNoTracking: false,
                          ignoreQueryFilters: true);

        if (board == null)
        {
            return false;
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
            return false;
        }

        board.IsDeleted = true;
        await boardRepository.SaveChangesAsync();

        return true;
    }
    public async Task<bool> CreateBoardAsync(Guid userId, BoardCreateInputModel model)
    {
        Board board = new Board()
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

        ICollection<CategoryViewModel> categories = await categoryService.GetCategoriesAsync();

        foreach (Guid categoryId in model.SelectedCategoryIds)
        {
            if (!categories.Any(c => c.Id == categoryId))
            {
                continue;
            }

            BoardCategory boardCategory = new BoardCategory()
            {
                BoardId = board.Id,
                CategoryId = categoryId
            };
            board.BoardCategories.Add(boardCategory);
        }
        try
        {
            await boardRepository.AddAsync(board);
            await boardRepository.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException e)
        {
            Console.WriteLine("Error creating board: " + e.Message);
            return false;
        }
    }

    public async Task<string?> GetBoardNameByIdAsync(Guid id)
    {
        Board? board = await boardRepository.GetByIdAsync(id, ignoreQueryFilters: true);

        if (board == null)
            return null;

        return board.Name;
    }
}
