using ForumApp.Web.ViewModels.Reply;
using System.Collections;

namespace ForumApp.Web.ViewModels.Post;

public class PostDetailsViewModel
{
    public string Id { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public string CreatedAt { get; set; } = null!;
    public string BoardId { get; set; } = null!;
    public string BoardName { get; set; } = null!;
    public ICollection<ReplyDetailForPostDetailViewModel> Replies { get; set; }
        = new HashSet<ReplyDetailForPostDetailViewModel>();
}
