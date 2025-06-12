using Microsoft.EntityFrameworkCore;

namespace ForumApp.Data.Models;

public class Category
{
    [Comment("Id of category")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Comment("Name of category")]
    public string Name { get; set; } = null!;

    public virtual ICollection<BoardCategory> BoardCategories { get; set; }
    = new HashSet<BoardCategory>();
}

//-Category
//	-ID
//	-Name

//	-BoardCollection
