using ForumApp.GCommon;
using ForumApp.Web.ViewModels.Reply;
using ForumApp.Web.ViewModels.Tag;

namespace ForumApp.Web.ViewModels.Post;

public class PostDetailsViewModel
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public string CreatedAt { get; set; } = null!;
    public string ModifiedAt { get; set; } = null!;
    public string Author { get; set; } = null!;
    public bool IsDeleted { get; set; } = false;
    public string? ImageUrl { get; set; }
    public Guid BoardId { get; set; }
    public string BoardName { get; set; } = null!;
    public bool IsPublisher { get; set; } = false;
    public bool CanModerate { get; set; }
    public PaginatedResult<ReplyForPostDetailViewModel>? Replies { get; set; }
    public ICollection<TagViewModel>? Tags { get; set; }
        = new HashSet<TagViewModel>();
}
