using ForumApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static ForumApp.GCommon.ValidationConstants.PostConstants;

namespace ForumApp.Data.Configurations;

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder
            .HasKey(p => p.Id);

        builder
            .Property(p => p.Id)
            .ValueGeneratedNever();

        builder
            .Property(p => p.Content)
            .IsRequired()
            .HasMaxLength(ContentMaximumLength);

        builder
            .Property(p => p.Title)
            .IsRequired()
            .HasMaxLength(TitleMaximumLength);

        builder
            .Property(p => p.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder
            .Property(p => p.ModifiedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder
            .HasOne(p => p.Board)
            .WithMany(b => b.Posts)
            .HasForeignKey(p => p.BoardId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasMany(p => p.Replies)
            .WithOne(r => r.Post)
            .HasForeignKey(r => r.PostId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasQueryFilter(p => !p.IsDeleted);

        builder
            .HasData(Posts);
    }

    public static List<Post> Posts => new()
        {
            new()
            {
                Id = Guid.Parse("71d465ed-bd31-4c2c-9700-e1274685ca5d"),
                Title = "Welcome to the forums!",
                Content = "We're glad to have you here.",
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow,
                IsPinned = true,
                IsDeleted = false,
                BoardId = Guid.Parse("c5578431-7ae6-4ed9-a402-f1c3401c7100")
            },
            new()
            {
                Id = Guid.Parse("6523ec54-87f8-4114-b42b-4e6cb75c802a"),
                Title = "Laptop overheating issue",
                Content = "My laptop gets very hot when gaming. Any tips?",
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow,
                IsPinned = false,
                IsDeleted = false,
                BoardId = Guid.Parse("f8385f75-481b-4b70-be0e-c975265e98ba")
            }
        };
}
