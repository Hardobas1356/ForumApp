namespace ForumApp.Data.Models;

public class Reply
{
    public int Id { get; set; }
    public string Content { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int PostId { get; set; }
    public virtual Post Post { get; set; } = null!;
}

//-Reply 
//		-ID
//		-Content
//		-CreatedAt

//		-PostID
//		-UserID