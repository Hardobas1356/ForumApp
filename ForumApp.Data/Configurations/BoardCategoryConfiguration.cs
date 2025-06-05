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
            .WithMany(b => b.BoardCategoryCollection)
            .HasForeignKey(bc => bc.BoardId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(bc => bc.Category)
            .WithMany(c => c.BoardCategoryCollection)
            .HasForeignKey(bc => bc.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasData(
            new BoardCategory { BoardId = 1, CategoryId = 1 },
            new BoardCategory { BoardId = 2, CategoryId = 2 });
    }
}
