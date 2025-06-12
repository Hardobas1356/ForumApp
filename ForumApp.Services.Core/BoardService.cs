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

    public async Task<BoardDetailsViewModel?> GetBoardDetailsAsync(string boardId)
    {
        if (Guid.TryParse(boardId, out Guid id))
        {
            return null;
        }

        var boardDetails = await dbContext
            .Boards
            .AsNoTracking()
            .Where(b => !b.IsDeleted && b.Id == id)
            .Select(b => new BoardDetailsViewModel
            {
                Id = b.Id.ToString(),
                Name = b.Name,
                Description = b.Description,
                Posts = b.Posts
                            .Where(p => !p.IsDeleted)
                            .OrderByDescending(p => p.IsPinned)
                            .Select(p => new BoardPostViewModel
                            {
                                Id = p.Id.ToString(),
                                Title = p.Title,
                                CreatedAt = p.CreatedAt.ToString(DateTimeFormat),
                            })
                            .ToHashSet()
            })
            .FirstOrDefaultAsync();

        return boardDetails;
    }
}
