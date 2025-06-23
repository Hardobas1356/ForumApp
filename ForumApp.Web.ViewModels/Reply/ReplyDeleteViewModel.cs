namespace ForumApp.Web.ViewModels.Reply;

public class ReplyDeleteViewModel
{
    public Guid Id { get; set; }
    public Guid PostId { get; set; }
    public string Content { get; set; } = null!;
    public string CreatedAt { get; set; } = null!;
}
