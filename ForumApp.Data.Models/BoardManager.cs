using Microsoft.EntityFrameworkCore;

namespace ForumApp.Data.Models;

public class BoardManager
{
    [Comment("Id of Board to which the user is a manager. Part of key")]
    public Guid BoardId { get; set; }
    public Board Board { get; set; } = null!;
    [Comment("Id of user manager. Part of key")]
    public Guid ApplicationUserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; } = null!;
    [Comment("Shows whether this manager position is deleted")]
    public bool IsDeleted { get; set; } = false;
}
