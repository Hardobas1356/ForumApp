using ForumApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ForumApp.Data.Configurations;

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder
            .HasKey(p => p.Id);

        builder
            .Property(p => p.Content)
            .IsRequired();

        builder
            .Property(p => p.Title)
            .IsRequired();

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
    }
}
