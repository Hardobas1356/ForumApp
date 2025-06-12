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
            .Property(b => b.Id)
            .ValueGeneratedNever();

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

    }
}
