namespace ForumApp.Web.ViewModels.Post;

public class PostDeleteViewModel
{
    public Guid Id { get; set; }
    public Guid BoardId { get; set; }   
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public string? ImageUrl { get; set; }
    public DateTime CreatedAt { get; set; }
}
