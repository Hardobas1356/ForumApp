namespace ForumApp.Web.ViewModels.Post;

public class PostForBoardDetailsViewModel
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string CreatedAt { get; set; } = null!;
}
