using ForumApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static ForumApp.Data.Common.PostConstants;

namespace ForumApp.Data.Configurations;

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder
            .HasKey(p => p.Id);

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

        builder.HasData(
            new Post
            {
                Id = 1,
                Title = "Welcome to the Forum!",
                Content = "Introduce yourself here.",
                BoardId = 1,
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            },
            new Post
            {
                Id = 2,
                Title = "Site Rules",
                Content = "Please read before posting.",
                BoardId = 2,
                IsPinned = true,
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            }
        );
    }
}
