using ForumApp.Data;
using ForumApp.Services.Core.Interfaces;
using ForumApp.Web.ViewModels.Category;
using Microsoft.EntityFrameworkCore;

namespace ForumApp.Services.Core;

public class CategoryService : ICategoryService
{
    private readonly ForumAppDbContext dbContext;

    public CategoryService(ForumAppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<ICollection<CategoryViewModel>> GetCategoriesAsync()
    {
        ICollection<CategoryViewModel> categories = await dbContext
            .Categories
            .AsNoTracking()
            .Select(c => new CategoryViewModel
            {
                Name = c.Name,
                ColorHex = c.ColorHex,
            })
            .ToArrayAsync();

        return categories;
    }

    public async Task<ICollection<CategoryViewModel>> GetCategoriesAsyncByBoardId(Guid boardId)
    {
        ICollection<CategoryViewModel> categories = await dbContext
            .BoardCategories
            .AsNoTracking()
            .Where(bc=>bc.BoardId==boardId)
            .Select(bc => new CategoryViewModel
            {
                Name = bc.Category.Name,
                ColorHex = bc.Category.ColorHex,
            })
            .ToArrayAsync();

        return categories;
    }
}
