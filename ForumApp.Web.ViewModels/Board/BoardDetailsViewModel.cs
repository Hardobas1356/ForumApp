namespace ForumApp.Web.ViewModels.Board;

public class BoardDetailsViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public HashSet<BoardPostViewModel> Posts { get; set; } = new HashSet<BoardPostViewModel>();
}
