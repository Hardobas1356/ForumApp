namespace ForumApp.Web.ViewModels.Board;

public class BoardPostViewModel
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string CreatedAt { get; set; } = null!;
}
