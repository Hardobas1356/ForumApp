using ForumApp.Web.ViewModels.Reply;
using System.Collections;

namespace ForumApp.Web.ViewModels.Post;

public class PostDetailsViewModel
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public string CreatedAt { get; set; } = null!;
    public int BoardId { get; set; }
    public string BoardName { get; set; } = null!;
    public ICollection<ReplyDetailForPostDetailViewModel> Replies { get; set; }
        = new HashSet<ReplyDetailForPostDetailViewModel>();
}
