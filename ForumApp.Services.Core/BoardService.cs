using ForumApp.Data;
using ForumApp.Data.Models;
using ForumApp.Services.Core.Interfaces;
using ForumApp.Web.ViewModels.Board;
using ForumApp.Web.ViewModels.Category;
using ForumApp.Web.ViewModels.Post;
using Microsoft.EntityFrameworkCore;

using static ForumApp.GCommon.GlobalConstants;

namespace ForumApp.Services.Core;

public class BoardService : IBoardService
{
    private readonly IGenericRepository<Board> repository;
    private readonly IPostService postService;
    private readonly ICategoryService categoryService;

    public BoardService(IGenericRepository<Board> repository, IPostService postService, ICategoryService categoryService)
    {
        this.repository = repository;
        this.postService = postService;
        this.categoryService = categoryService;
    }

    public async Task<IEnumerable<BoardAllIndexViewModel>> GetAllBoardsAsync()
    {
        IEnumerable<Board> boards = await repository
            .GetAllAsync(true);

        return boards
            .Select(b => new BoardAllIndexViewModel
            {
                Id = b.Id,
                Name = b.Name,
                ImageUrl = b.ImageUrl,
                Description = b.Description,
                Categories = b.BoardCategories
                    .Select(bc => new CategoryViewModel
                    {
                        Id = bc.CategoryId,
                        Name = bc.Category.Name,
                        ColorHex = bc.Category.ColorHex,
                    })
                    .ToArray()
            })
            .ToArray();
    }

    public async Task<BoardDetailsViewModel?> GetBoardDetailsAsync(Guid boardId)
    {
        IEnumerable<Board> board = await repository
            .GetWhereAsync(b => b.Id == boardId,true);
        Board? boardEntity = board
            .FirstOrDefault();

        if (boardEntity == null)
        {
            return null;
        }

        IEnumerable<PostForBoardDetailsViewModel>? posts =
            await postService.GetPostsForBoardDetailsAsync(boardId);

        ICollection<CategoryViewModel>? categories =
            await categoryService.GetCategoriesAsyncByBoardId(boardId);

        return new BoardDetailsViewModel
        {
            Id = boardEntity.Id,
            Name = boardEntity.Name,
            ImageUrl = boardEntity.ImageUrl,
            Description = boardEntity.Description,
            Posts = posts?.ToHashSet(),
            Categories = categories?.ToHashSet(),
        };
    }
}
