using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ForumApp.Data;

public class ForumAppDbContext : IdentityDbContext
{
    public ForumAppDbContext(DbContextOptions<ForumAppDbContext> options)
        : base(options)
    {
    }
}
