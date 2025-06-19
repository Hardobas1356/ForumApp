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
            .HasDefaultValueSql("getutcdate()");

        builder
            .Property(p => p.ModifiedAt)
            .HasDefaultValueSql("getutcdate()");

        builder
            .HasOne(p => p.Board)
            .WithMany(b => b.Posts)
            .HasForeignKey(p => p.BoardId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(p=>p.ApplicationUser)
            .WithMany(au=>au.Posts)
            .HasForeignKey(p => p.ApplicationUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasQueryFilter(p => !p.IsDeleted);

        builder
            .HasData(Posts);
    }

    public static List<Post> Posts => new()
    {
        new Post
        {
            Id = Guid.Parse("71d465ed-bd31-4c2c-9700-e1274685ca5d"),
            Title = "Welcome to the forums!",
            Content = "We're glad to have you here.",
            CreatedAt = DateTime.UtcNow,
            ModifiedAt = DateTime.UtcNow,
            IsPinned = true,
            IsDeleted = false,
            BoardId = Guid.Parse("c5578431-7ae6-4ed9-a402-f1c3401c7100"),
            ApplicationUserId = Guid.Parse("7d926fd2-1b4e-4ea7-a019-2bcb179db8f9"),
        },
        new Post
        {
            Id = Guid.Parse("6523ec54-87f8-4114-b42b-4e6cb75c802a"),
            Title = "Laptop overheating issue",
            Content = "My laptop gets very hot when gaming. Any tips?",
            CreatedAt = DateTime.UtcNow,
            ModifiedAt = DateTime.UtcNow,
            IsPinned = false,
            IsDeleted = false,
            BoardId = Guid.Parse("f8385f75-481b-4b70-be0e-c975265e98ba"),
            ApplicationUserId = Guid.Parse("e43bb3f7-884a-437b-9a0c-b0d181f07634")
        }
    };
}
