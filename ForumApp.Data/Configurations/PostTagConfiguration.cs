using ForumApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ForumApp.Data.Configurations;

public class PostTagConfiguration : IEntityTypeConfiguration<PostTag>
{
    public void Configure(EntityTypeBuilder<PostTag> builder)
    {
        builder
            .HasKey(pt => new { pt.TagId, pt.PostId });

        builder
            .HasOne(pt => pt.Tag)
            .WithMany(bt => bt.PostTags)
            .HasForeignKey(pt => pt.TagId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(pt => pt.Post)
            .WithMany(p=>p.PostTags)
            .HasForeignKey(pt => pt.PostId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasQueryFilter(pt => !pt.Post.IsDeleted);

        builder
            .HasData(PostTags);
    }

    public static List<PostTag> PostTags => new()
        {
            new()
            {
                PostId = Guid.Parse("71d465ed-bd31-4c2c-9700-e1274685ca5d"),
                TagId = Guid.Parse("3b169889-2b30-47f5-81fc-4f68fb3369ba")
            },
            new()
            {
                PostId = Guid.Parse("6523ec54-87f8-4114-b42b-4e6cb75c802a"),
                TagId = Guid.Parse("1c326eb8-947a-41e9-a3a9-03a630af7151")
            }
        };
}
