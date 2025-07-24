using Microsoft.EntityFrameworkCore;

namespace ForumApp.Data.Models;

public class Reply
{
    [Comment("Id of reply")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Comment("Comment of reply")]
    public string Content { get; set; } = null!;

    [Comment("Date when the reply was created in UTC time")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Comment("Shows whether the reply was deleted by moderator")]
    public bool IsDeleted { get; set; } = false;


    [Comment("Id of the post to which the reply belongs to")]
    public Guid PostId { get; set; }
    public virtual Post Post { get; set; } = null!;

    [Comment("Id of user which posted this reply")]
    public Guid? ApplicationUserId { get; set; }
    public virtual ApplicationUser? ApplicationUser { get; set; }
}