using Microsoft.EntityFrameworkCore;

namespace ForumApp.Data.Models;

public class Category
{
    [Comment("Id of category")]
    public int Id { get; set; }
    [Comment("Name of category")]
    public string Name { get; set; } = null!;

    public virtual ICollection<BoardCategory> BoardCategories { get; set; }
    = new HashSet<BoardCategory>();
}

//-Category
//	-ID
//	-Name

//	-BoardCollection
