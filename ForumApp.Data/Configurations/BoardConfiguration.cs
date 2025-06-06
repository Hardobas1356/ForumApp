using ForumApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static ForumApp.Data.Common.BoardConstants;

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
            .IsRequired()
            .HasMaxLength(NameMaxLength);

        builder
            .Property(b => b.Description)
            .IsRequired()
            .HasMaxLength(DescriptionMaxLength);

        builder
            .Property(b => b.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.HasData(
            new Board { Id = 1, Name = "General", Description = "General discussion board" },
            new Board { Id = 2, Name = "Announcements", Description = "Official announcements" }
        );

    }
}
