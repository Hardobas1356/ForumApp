using ForumApp.Data;
using ForumApp.Services.Core.Interfaces;
using ForumApp.Web.ViewModels.Board;
using Microsoft.EntityFrameworkCore;

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
                Name = b.Name,
                Description = b.Description,
            })
            .ToListAsync();

        return boards;
    }
}
