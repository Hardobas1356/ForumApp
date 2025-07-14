namespace ForumApp.Web.ViewModels.Reply;

public class ReplyForPostDetailViewModel
{
    public Guid Id { get; set; }
    public string Content { get; set; } = null!;
    public string CreatedAt { get; set; } = null!;
    public string Author { get; set; } = null!;
    public bool IsPublisher { get; set; }
    public bool CanModerate { get; set; } = false;
}
