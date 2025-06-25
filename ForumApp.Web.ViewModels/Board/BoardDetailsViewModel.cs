using ForumApp.Web.ViewModels.Post;

namespace ForumApp.Web.ViewModels.Board;

public class BoardDetailsViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? ImageUrl { get; set; }
    public string Description { get; set; } = null!;
    public ICollection<PostForBoardDetailsViewModel>? Posts { get; set; }
        = new HashSet<PostForBoardDetailsViewModel>();
}
