using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ForumApp.Data.Models;

public class ApplicationUser : IdentityUser<Guid>
{
    [Comment("Non-mandatory url of profile picture for user")]
    public string? ImageUrl { get; set; }

    [Comment("Join date for user in utc time")]
    public DateTime JoinDate { get; set; } = DateTime.UtcNow;

    [Comment("Name with which user is displayed in app")]
    public string DisplayName { get; set; } = null!;
    [Comment("Shows whether the user is deleted")]
    public bool IsDeleted { get; set; } = false;

    public virtual ICollection<Post> Posts { get; set; } 
        = new HashSet<Post>();
    public virtual ICollection<Reply> Replies { get; set; }
        =new HashSet<Reply>();
    public virtual ICollection<BoardManager> BoardManagers { get; set; }
    = new HashSet<BoardManager>();
}
