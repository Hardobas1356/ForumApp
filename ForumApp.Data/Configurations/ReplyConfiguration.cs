using ForumApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static ForumApp.GCommon.ValidationConstants.ReplyConstants;

namespace ForumApp.Data.Configurations;

public class ReplyConfiguration : IEntityTypeConfiguration<Reply>
{
    public void Configure(EntityTypeBuilder<Reply> builder)
    {
        builder
            .HasKey(r => r.Id);

        builder
            .Property(r => r.Id)
            .ValueGeneratedNever();

        builder
            .Property(r => r.Content)
            .IsRequired()
            .HasMaxLength(ContentMaxLength);

        builder
            .Property(r => r.IsDeleted)
            .HasDefaultValue(false);

        builder
            .Property(r => r.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder
            .HasQueryFilter(r => !r.IsDeleted);

        builder
            .HasData(Replies);
    }

    public static List<Reply> Replies => new()
        {
            new()
            {
                Id = Guid.Parse("7bded954-6e81-4e44-a7e3-19234f568f0c"),
                Content = "Thanks! Happy to be here.",
                CreatedAt = DateTime.UtcNow,
                PostId = Guid.Parse("71d465ed-bd31-4c2c-9700-e1274685ca5d")
            },
            new()
            {
                Id = Guid.Parse("9669f2a1-b62d-4a18-8e49-3edabb18d418"),
                Content = "Try cleaning the fan and applying new thermal paste.",
                CreatedAt = DateTime.UtcNow,
                PostId = Guid.Parse("6523ec54-87f8-4114-b42b-4e6cb75c802a")
            }
        };
}
