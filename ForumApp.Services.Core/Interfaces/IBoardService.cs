using ForumApp.Web.ViewModels.Board;

namespace ForumApp.Services.Core.Interfaces;

public interface IBoardService
{
    Task<IEnumerable<AllBoardsIndexViewModel>> GetAllBoardsAsync();
    Task<BoardDetailsViewModel?> GetBoardDetailsAsync(string boardId);
}
