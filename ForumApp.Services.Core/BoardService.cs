using ForumApp.Data;
using ForumApp.Services.Core.Interfaces;
using ForumApp.Web.ViewModels.Board;
using Microsoft.EntityFrameworkCore;

using static ForumApp.GCommon.GlobalConstants;

namespace ForumApp.Services.Core;

public class BoardService : IBoardService
{
    private readonly ForumAppDbContext dbContext;

    public BoardService(ForumAppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<IEnumerable<AllBoardsIndexViewModel>> GetAllBoardsAsync()
    {
        var boards = await dbContext
            .Boards
            .AsNoTracking()
            .Where(b => !b.IsDeleted)
            .Select(b => new AllBoardsIndexViewModel
            {
                Id = b.Id.ToString(),
                Name = b.Name,
                Description = b.Description,
            })
            .ToListAsync();

        return boards;
    }

    public async Task<BoardDetailsViewModel?> GetBoardDetailsAsync(int boardId)
    {
        var boardDetails = await dbContext
            .Boards
            .AsNoTracking()
            .Where(b => !b.IsDeleted && b.Id == boardId)
            .Select(b => new BoardDetailsViewModel
            {
                Id = (int)b.Id,
                Name = b.Name,
                Description = b.Description,
                Posts = b.Posts
                            .Where(p => !p.IsDeleted)
                            .OrderByDescending(p => p.IsPinned)
                            .Select(p => new BoardPostViewModel
                            {
                                Id = (int)p.Id,
                                Title = p.Title,
                                CreatedAt = p.CreatedAt.ToString(DateTimeFormat),
                            })
                            .ToHashSet()
            })
            .FirstOrDefaultAsync();

        return boardDetails;
    }
}
