using ForumApp.Web.ViewModels.Reply;
using System.Collections;

namespace ForumApp.Web.ViewModels.Post;

public class PostDetailsViewModel
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public string CreatedAt { get; set; } = null!;
    public string BoardId { get; set; } = null!;
    public string BoardName { get; set; } = null!;
    public bool IsPublisher { get; set; } = false;
    public ICollection<ReplyDetailForPostDetailViewModel> Replies { get; set; }
        = new HashSet<ReplyDetailForPostDetailViewModel>();
}
