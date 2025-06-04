namespace ForumApp.Data.Models;

public class Board
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateOnly CreatedAt { get; set; }
    public bool IsDeleted { get; set; } = false;

    public virtual ICollection<Post> Posts { get; set; } = new HashSet<Post>();
}

//-Board
//		-ID
//		-Name
//		-CreatedAt
//		-IsDeleted = false

//		-BoardCategoryCollection
//		-PostsCollection
//		-BoardTags