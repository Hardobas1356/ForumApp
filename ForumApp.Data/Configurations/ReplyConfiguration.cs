using ForumApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ForumApp.Data.Configurations;

public class ReplyConfiguration : IEntityTypeConfiguration<Reply>
{
    public void Configure(EntityTypeBuilder<Reply> builder)
    {
        builder
            .HasKey(r => r.Id);

        builder
            .Property(r => r.Content)
            .IsRequired();

        builder
            .Property(r => r.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.HasData(
            new Reply
            {
                Id = 1,
                Content = "Hello everyone!",
                PostId = 1,
                CreatedAt = DateTime.UtcNow
            },
            new Reply
            {
                Id = 2,
                Content = "Thanks for the heads-up.",
                PostId = 2,
                CreatedAt = DateTime.UtcNow
            }
        );
    }
}
