using Microsoft.EntityFrameworkCore;

namespace ForumApp.Data.Models;

public class Board
{
    [Comment("Board Id")]
    public int Id { get; set; }

    [Comment("Name of board")]
    public string Name { get; set; } = null!;

    [Comment("Short description of the board")]
    public string Description { get; set; } = null!;

    [Comment("Board creation date")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [Comment("Represents whether the board is deleted or not")]
    public bool IsDeleted { get; set; } = false;

    public virtual ICollection<Post> Posts { get; set; }
        = new HashSet<Post>();
    public virtual ICollection<BoardCategory> BoardCategories { get; set; }
        = new HashSet<BoardCategory>();
}

//-Board
//		-ID
//		-Name
//		-CreatedAt
//		-IsDeleted = false

//		-BoardCategoryCollection
//		-PostsCollection
//		-BoardTags