namespace ForumApp.Web.ViewModels.Post;

public class PostBoardDetailsViewModel
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string CreatedAt { get; set; } = null!;
}
