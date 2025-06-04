namespace ForumApp.Data.Models;

public class BoardTag
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public virtual ICollection<PostBoardTag> PostTags { get; set; } 
        = new HashSet<PostBoardTag>();
}
