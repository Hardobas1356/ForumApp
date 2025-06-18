using ForumApp.Data;
using ForumApp.Services.Core.Interfaces;
using ForumApp.Web.ViewModels.Board;
using ForumApp.Web.ViewModels.Post;
using Microsoft.EntityFrameworkCore;

using static ForumApp.GCommon.GlobalConstants;

namespace ForumApp.Services.Core;

public class BoardService : IBoardService
{
    private readonly ForumAppDbContext dbContext;
    private readonly IPostService postService;

    public BoardService(ForumAppDbContext dbContext, IPostService postService)
    {
        this.dbContext = dbContext;
        this.postService = postService;
    }

    public async Task<IEnumerable<BoardAllIndexViewModel>> GetAllBoardsAsync()
    {
        var boards = await dbContext
            .Boards
            .AsNoTracking()
            .Where(b => !b.IsDeleted)
            .Select(b => new BoardAllIndexViewModel
            {
                Id = b.Id.ToString(),
                Name = b.Name,
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
               .Where(b => !b.IsDeleted && b.Id == boardId)
               .FirstOrDefaultAsync();

        if (board == null)
        {
            return null;
        }

        IEnumerable<PostBoardDetailsViewModel>? posts = 
            await postService.GetPostsForBoardDetailsAsync(boardId);

        return new BoardDetailsViewModel
        {
            Id = board.Id,
            Name = board.Name,
            Description = board.Description,
            Posts = posts?.ToHashSet()
        };
    }
}
