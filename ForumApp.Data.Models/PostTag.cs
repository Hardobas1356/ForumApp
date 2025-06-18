using Microsoft.EntityFrameworkCore;

namespace ForumApp.Data.Models;

public class PostTag
{
    [Comment("Id of the post")]
    public Guid PostId { get; set; }
    public virtual Post Post { get; set; } = null!;


    [Comment("Id of the tag which applied to the post")]
    public Guid TagId { get; set; }
    public virtual Tag Tag { get; set; } = null!;
}