using Microsoft.EntityFrameworkCore;

namespace ForumApp.Data.Models;

public class Post
{
    [Comment("Id of post")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Comment("Title of the post")]
    public string Title { get; set; } = null!;

    [Comment("Content of the post")]
    public string Content { get; set; } = null!;

    [Comment("Url for optional picture")]
    public string? ImageUrl { get; set; }

    [Comment("Date when the post was created in UTC time")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Comment("Last date the post was modified in UTC time")]
    public DateTime ModifiedAt { get; set; } = DateTime.UtcNow;

    [Comment("Shows whether the post is pinned by moderator")]
    public bool IsPinned { get; set; } = false;

    [Comment("Shows whether the post was deleted by moderator")]
    public bool IsDeleted { get; set; } = false;

    [Comment("Id of board to which the post belongs to")]
    public Guid BoardId { get; set; }
    public virtual Board Board { get; set; } = null!;

    [Comment("Id of user which posted this post")]
    public Guid ApplicationUserId { get; set; }
    public virtual ApplicationUser ApplicationUser { get; set; } = null!;

    public virtual ICollection<Reply> Replies { get; set; } = new HashSet<Reply>();
    public virtual ICollection<PostTag> PostTags { get; set; } = new HashSet<PostTag>();

}