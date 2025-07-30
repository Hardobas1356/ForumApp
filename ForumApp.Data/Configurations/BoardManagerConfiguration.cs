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
        builder
                  .HasData(BoardManagers);
    }
    public static List<BoardManager> BoardManagers => new()
    {
        new()
        {
            ApplicationUserId = Guid.Parse("7d926fd2-1b4e-4ea7-a019-2bcb179db8f9"),
            BoardId = Guid.Parse("c5578431-7ae6-4ed9-a402-f1c3401c7100"),
            IsDeleted = false
        },
        new()
        {
            ApplicationUserId = Guid.Parse("e43bb3f7-884a-437b-9a0c-b0d181f07634"),
            BoardId = Guid.Parse("f8385f75-481b-4b70-be0e-c975265e98ba"),
            IsDeleted = false
        }
    };
}

