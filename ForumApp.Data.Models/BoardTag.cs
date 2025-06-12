using Microsoft.EntityFrameworkCore;

namespace ForumApp.Data.Models;

public class BoardTag
{
    [Comment("Id of tag which can be used in posts on a board")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Comment("Name of tag")]
    public string Name { get; set; } = null!;

    public virtual ICollection<PostBoardTag> PostBoardTags { get; set; }
        = new HashSet<PostBoardTag>();
}
