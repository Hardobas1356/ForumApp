using ForumApp.Web.ViewModels.Category;

namespace ForumApp.Services.Core.Interfaces;

public interface ICategoryService
{
    Task<ICollection<CategoryViewModel>> GetCategoriesAsync();
    Task<ICollection<CategoryViewModel>> GetCategoriesAsyncByBoardId(Guid boardId); 
}
