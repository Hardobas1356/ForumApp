using ForumApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ForumApp.Data.Configurations;

public class BoardManagerConfiguration : IEntityTypeConfiguration<BoardManager>
{
    public void Configure(EntityTypeBuilder<BoardManager> builder)
    {
        builder
            .HasKey(bm => new { bm.ApplicationUserId, bm.BoardId });

        builder
            .HasOne(bm => bm.Board)
            .WithMany(b => b.BoardManagers)
            .HasForeignKey(bm => bm.BoardId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(bm => bm.ApplicationUser)
            .WithMany(au => au.BoardManagers)
            .HasForeignKey(bm => bm.ApplicationUserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasQueryFilter(bm => !bm.IsDeleted
                            && !bm.ApplicationUser.IsDeleted
                            && !bm.Board.IsDeleted);
    }
}
