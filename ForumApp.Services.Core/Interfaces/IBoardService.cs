using ForumApp.Web.ViewModels.Board;

namespace ForumApp.Services.Core.Interfaces;

public interface IBoardService
{
    Task<IEnumerable<BoardAllIndexViewModel>> GetAllBoardsAsync();
    Task<BoardDetailsViewModel?> GetBoardDetailsAsync(Guid boardId);
}
