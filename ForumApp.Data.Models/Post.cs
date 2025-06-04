namespace ForumApp.Data.Models;

public class Post
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime ModifiedAt { get; set; } = DateTime.Now;
    public bool IsPinned { get; set; } = false;
    public bool IsDeleted { get; set; } = false;

    public int BoardId { get; set; }
    public virtual Board Board { get; set; } = null!;

    public virtual ICollection<Reply> Replies { get; set; } = new HashSet<Reply>();
    public virtual ICollection<PostBoardTag> PostTags { get; set; } = new HashSet<PostBoardTag>();

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