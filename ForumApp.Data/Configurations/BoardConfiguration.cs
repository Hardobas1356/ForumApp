using ForumApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ForumApp.Data.Configurations;

public class BoardConfiguration : IEntityTypeConfiguration<Board>
{
    public void Configure(EntityTypeBuilder<Board> builder)
    {
        builder
            .HasKey(b => b.Id);

        builder
            .HasIndex(b => b.Name)
            .IsUnique();

        builder
            .Property(b => b.Name)
            .IsRequired();

        builder
            .Property(b => b.Description)
            .IsRequired();

        builder
            .Property(b => b.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.HasData(
            new Board { Id = 1, Name = "General", Description = "General discussion board" },
            new Board { Id = 2, Name = "Announcements", Description = "Official announcements" }
        );

    }
}
