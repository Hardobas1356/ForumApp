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
    }
}
