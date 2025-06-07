using Microsoft.EntityFrameworkCore;

namespace ForumApp.Data.Models;

public class BoardCategory
{
    [Comment("Id of board")]
    public int BoardId { get; set; }
    public virtual Board Board { get; set; } = null!;

    [Comment("Id of category to apply to board")]
    public int CategoryId { get; set; }
    public virtual Category Category { get; set; } = null!;
}
