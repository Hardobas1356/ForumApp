namespace ForumApp.Data.Models;

public class BoardCategory
{
    public int BoardId { get; set; }
    public virtual Board Board { get; set; } = null!;
    public int CategoryId { get; set; }
    public virtual Category Category { get; set; } = null!;
}
