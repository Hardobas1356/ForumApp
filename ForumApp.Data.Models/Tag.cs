using Microsoft.EntityFrameworkCore;

namespace ForumApp.Data.Models;

public class Tag
{
    [Comment("Id of tag which can be used in posts on a board")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Comment("Name of tag")]
    public string Name { get; set; } = null!;

    public virtual ICollection<PostTag> PostTags { get; set; }
        = new HashSet<PostTag>();
}
