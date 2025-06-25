using ForumApp.Data;
using ForumApp.Services.Core.Interfaces;
using ForumApp.Web.ViewModels.Board;
using ForumApp.Web.ViewModels.Category;
using ForumApp.Web.ViewModels.Post;
using Microsoft.EntityFrameworkCore;

using static ForumApp.GCommon.GlobalConstants;

namespace ForumApp.Services.Core;

public class BoardService : IBoardService
{
    private readonly ForumAppDbContext dbContext;
    private readonly IPostService postService;
    private readonly ICategoryService categoryService;

    public BoardService(ForumAppDbContext dbContext, IPostService postService, ICategoryService categoryService)
    {
        this.dbContext = dbContext;
        this.postService = postService;
        this.categoryService = categoryService;
    }

    public async Task<IEnumerable<BoardAllIndexViewModel>> GetAllBoardsAsync()
    {
        var boards = await dbContext
            .Boards
            .AsNoTracking()
            .Where(b => !b.IsDeleted)
            .Select(b => new BoardAllIndexViewModel
            {
                Id = b.Id,
                Name = b.Name,
                ImageUrl = b.ImageUrl,
                Description = b.Description,
            })
            .ToListAsync();

        return boards;
    }

    public async Task<BoardDetailsViewModel?> GetBoardDetailsAsync(Guid boardId)
    {
        var board = await dbContext
               .Boards
               .AsNoTracking()
               .Where(b => b.Id == boardId)
               .FirstOrDefaultAsync();

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
            Posts = posts?.ToHashSet(),
            Categories = categories?.ToHashSet(),
        };
    }
}
