using ForumApp.Web.ViewModels.Admin.BoardModerators;
using ForumApp.Web.ViewModels.Category;
using ForumApp.Web.ViewModels.Post;

namespace ForumApp.Web.ViewModels.Admin.Board;

public class BoardDetailsAdminViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string? ImageUrl { get; set; }
    public string CreatedAt { get; set; } = null!;
    public IEnumerable<BoardModeratorViewModel>? Moderators { get; set; }
        = new HashSet<BoardModeratorViewModel>();
    public IEnumerable<CategoryViewModel>? Categories { get; set; }
    = new HashSet<CategoryViewModel>();
    public IEnumerable<PostForBoardDetailsViewModel>? Posts { get; set; }
        = new HashSet<PostForBoardDetailsViewModel>();
}
