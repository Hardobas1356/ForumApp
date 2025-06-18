using ForumApp.Web.ViewModels.Post;

namespace ForumApp.Web.ViewModels.Board;

public class BoardDetailsViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public ICollection<PostBoardDetailsViewModel>? Posts { get; set; }
        = new HashSet<PostBoardDetailsViewModel>();
}
