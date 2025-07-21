using ForumApp.GCommon;
using ForumApp.Web.ViewModels.Category;
using ForumApp.Web.ViewModels.Post;

namespace ForumApp.Web.ViewModels.Board;

public class BoardDetailsViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? ImageUrl { get; set; }
    public string CreatedAt { get; set; } = null!;
    public string Description { get; set; } = null!;
    public PaginatedResult<PostForBoardDetailsViewModel>? Posts { get; set; }
    public IEnumerable<CategoryViewModel>? Categories { get; set; }
        = new HashSet<CategoryViewModel>();
}
