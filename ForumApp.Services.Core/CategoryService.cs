using ForumApp.Data;
using ForumApp.Data.Models;
using ForumApp.Services.Core.Interfaces;
using ForumApp.Web.ViewModels.Category;
using Microsoft.EntityFrameworkCore;

namespace ForumApp.Services.Core;

public class CategoryService : ICategoryService
{
    private readonly IGenericRepository<Category> repository;

    public CategoryService(IGenericRepository<Category> repository)
    {
        this.repository = repository;
    }

    public async Task<ICollection<CategoryViewModel>> GetCategoriesAsync()
    {
        IEnumerable<Category> categories = await repository
            .GetAllAsync(true);

        ICollection<CategoryViewModel> entities = categories
            .Select(c => new CategoryViewModel
            {
                Id = c.Id,
                Name = c.Name,
                ColorHex = c.ColorHex,
            })
            .ToArray();

        return entities;
    }

    public async Task<ICollection<CategoryViewModel>> GetCategoriesAsyncByBoardId(Guid boardId)
    {
        IEnumerable<Category> categories = await repository
            .GetWhereAsync(c => c.BoardCategories.Any(bc => bc.BoardId == boardId), true);

        ICollection<CategoryViewModel> result = categories
                .Select(c => new CategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    ColorHex = c.ColorHex,
                })
                .ToArray();

        return result;
    }
}
