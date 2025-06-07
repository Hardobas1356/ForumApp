using Microsoft.EntityFrameworkCore;

namespace ForumApp.Data.Models;

public class Post
{
    [Comment("Id of post")]
    public int Id { get; set; }
    [Comment("Title pf the post")]
    public string Title { get; set; } = null!;
    [Comment("Content of the post")]
    public string Content { get; set; } = null!;
    [Comment("Date when the post was created in UTC time")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    [Comment("Last date the post was modified in UTC time")]
    public DateTime ModifiedAt { get; set; } = DateTime.UtcNow;
    [Comment("Shows whether the post is pinned moderator")]
    public bool IsPinned { get; set; } = false;
    [Comment("Shows whether the post was deleted by moderator")]
    public bool IsDeleted { get; set; } = false;

    [Comment("Id of board to which the post belongs to")]
    public int BoardId { get; set; }
    public virtual Board Board { get; set; } = null!;

    public virtual ICollection<Reply> Replies { get; set; } = new HashSet<Reply>();
    public virtual ICollection<PostBoardTag> PostBoardTags { get; set; } = new HashSet<PostBoardTag>();

}

//-Post
//		-ID
//		-Title
//		-Content
//		-CreatedAt
//		-IsPinned = false
//		-IsDeleted = false

//		-BoardID
//		-UserID
//		-RepliesCollection
//		-PostBoardTagCollection