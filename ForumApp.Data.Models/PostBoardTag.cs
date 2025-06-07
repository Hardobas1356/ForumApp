using Microsoft.EntityFrameworkCore;

namespace ForumApp.Data.Models;

public class PostBoardTag
{
    [Comment("Id of the post")]
    public int PostId { get; set; }
    public virtual Post Post { get; set; } = null!;

    [Comment("Id of the tag which applied to the post")]
    public int BoardTagId { get; set; }
    public virtual BoardTag BoardTag { get; set; } = null!;
}

//-PostBoardTag
//	-PostId
//	-BoardTagId