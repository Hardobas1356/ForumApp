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
            },
            new()
            {
                BoardId = Guid.Parse("aa0a3c1e-1b6d-4a7c-a4d9-eee80b91b1a1"),
                CategoryId = Guid.Parse("60f51770-93bc-42b4-a27c-8a280abda112")
            },
            new()
            {
                BoardId = Guid.Parse("bb2c4e8f-3f3d-49b3-8417-07de67b4b1b2"),
                CategoryId = Guid.Parse("a1f8f839-28b3-4e3c-9b2f-4ffae1d23456")
            },
            new()
            {
                BoardId = Guid.Parse("cc3e7fa0-61f6-4a7a-bb4b-1fcda248c1c3"),
                CategoryId = Guid.Parse("b2d7f930-4e0f-4a5f-a45f-d84b1e78b789")
            },
            new()
            {
                BoardId = Guid.Parse("dd4f9cb1-9a7e-4c5a-9c6b-3bba9e1d41d4"),
                CategoryId = Guid.Parse("c3a6d021-9a6b-43f3-b13b-13db6b9e5432")
            },
            new()
            {
                BoardId = Guid.Parse("dd4f9cb1-9a7e-4c5a-9c6b-3bba9e1d41d4"),
                CategoryId = Guid.Parse("d487c912-c1fe-46cc-b872-78a5d96c3f21")
            },
            new()
            {
                BoardId = Guid.Parse("ee510dc2-afbf-4a38-b97f-e6f3c4eb51e5"),
                CategoryId = Guid.Parse("e561a103-d567-42b9-a512-5de903abc321")
            }
        };
}
