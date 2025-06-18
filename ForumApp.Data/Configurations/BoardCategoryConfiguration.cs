using ForumApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ForumApp.Data.Configurations;

public class BoardCategoryConfiguration : IEntityTypeConfiguration<BoardCategory>
{
    public void Configure(EntityTypeBuilder<BoardCategory> builder)
    {
        builder
            .HasKey(bc => new { bc.BoardId, bc.CategoryId });

        builder
            .HasOne(bc => bc.Board)
            .WithMany(b => b.BoardCategories)
            .HasForeignKey(bc => bc.BoardId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(bc => bc.Category)
            .WithMany(c => c.BoardCategories)
            .HasForeignKey(bc => bc.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasQueryFilter(bc => !bc.Board.IsDeleted);

        builder
            .HasData(BoardCategories);
    }

    public static List<BoardCategory> BoardCategories => new()
        {
            new()
            {
                BoardId = Guid.Parse("c5578431-7ae6-4ed9-a402-f1c3401c7100"),
                CategoryId = Guid.Parse("67e8a9f8-29d7-444f-bd9b-86225ae41daf")
            },
            new()
            {
                BoardId = Guid.Parse("f8385f75-481b-4b70-be0e-c975265e98ba"),
                CategoryId = Guid.Parse("67e8a9f8-29d7-444f-bd9b-86225ae41daf")
            }
        };
}
