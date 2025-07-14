using ForumApp.Data;
using ForumApp.Data.Models;
using ForumApp.Services.Core.Interfaces;
using ForumApp.Web.ViewModels.Category;
using Microsoft.EntityFrameworkCore;

namespace ForumApp.Services.Core;

public class CategoryService : ICategoryService
{
    private readonly IGenericRepository<Category> categoryRepository;

    public CategoryService(IGenericRepository<Category> repository)
    {
        this.categoryRepository = repository;
    }

    public async Task<IEnumerable<CategoryViewModel>> GetCategoriesAsync()
    {
        IEnumerable<Category> categories = await categoryRepository
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

    public async Task<IEnumerable<CategoryViewModel>> GetCategoriesAsyncByBoardId(Guid boardId)
    {
        IEnumerable<Category> categories = await categoryRepository
            .GetWhereWithIncludeAsync(c => c.BoardCategories.Any(bc => bc.BoardId == boardId),
                                      q => q.Include(c => c.BoardCategories));

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
