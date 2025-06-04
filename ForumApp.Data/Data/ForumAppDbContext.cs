using ForumApp.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ForumApp.Data;

public class ForumAppDbContext : IdentityDbContext
{
    public ForumAppDbContext(DbContextOptions<ForumAppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Board> Boards { get; set; } = null!;
    public virtual DbSet<Post> Posts { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(ForumAppDbContext).Assembly);
    }
}
