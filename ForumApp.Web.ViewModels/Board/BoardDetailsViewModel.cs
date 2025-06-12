namespace ForumApp.Web.ViewModels.Board;

public class BoardDetailsViewModel
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public HashSet<BoardPostViewModel> Posts { get; set; } = new HashSet<BoardPostViewModel>();
}
