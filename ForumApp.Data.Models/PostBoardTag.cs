namespace ForumApp.Data.Models;

public class PostBoardTag
{
    public int PostId { get; set; }
    public virtual Post Post { get; set; } = null!;

    public int BoardTagId { get; set; }
    public virtual BoardTag BoardTag { get; set; } = null!;
}

//-PostBoardTag
//	-PostId
//	-BoardTagId