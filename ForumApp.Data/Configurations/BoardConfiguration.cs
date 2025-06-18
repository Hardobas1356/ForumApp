using ForumApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static ForumApp.GCommon.ValidationConstants.BoardConstants;

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

        builder.HasData(Boards);
    }

    public static List<Board> Boards => new()
        {
            new()
            {
                Id = Guid.Parse("c5578431-7ae6-4ed9-a402-f1c3401c7100"),
                Name = "General Discussion",
                Description = "Talk about anything here.",
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            },
            new()
            {
                Id = Guid.Parse("f8385f75-481b-4b70-be0e-c975265e98ba"),
                Name = "Tech Support",
                Description = "Get help with your tech problems.",
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            }
        };
}
