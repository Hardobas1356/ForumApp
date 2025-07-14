using ForumApp.Web.ViewModels.Category;

namespace ForumApp.Services.Core.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<CategoryViewModel>> GetCategoriesAsync();
    Task<IEnumerable<CategoryViewModel>> GetCategoriesAsyncByBoardId(Guid boardId); 
}
