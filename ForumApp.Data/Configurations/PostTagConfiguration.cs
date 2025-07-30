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
            new PostTag
            {
                PostId = Guid.Parse("71d465ed-bd31-4c2c-9700-e1274685ca5d"),
                TagId = Guid.Parse("3b169889-2b30-47f5-81fc-4f68fb3369ba") 
            },
            new PostTag
            {
                PostId = Guid.Parse("6523ec54-87f8-4114-b42b-4e6cb75c802a"),
                TagId = Guid.Parse("e8b93d7d-d267-44fb-8914-ff08b7ffbd90")
            },
            new PostTag
            {
                PostId = Guid.Parse("ae12507f-cde2-42c3-94ae-3f7d012d7a7d"), 
                TagId = Guid.Parse("f1a2be0e-1cd2-4e6d-87b4-1e5039db92c5")
            },
            new PostTag
            {
                PostId = Guid.Parse("9829f11a-3d2e-4cb7-b2d6-44c35b3b7ae6"),
                TagId = Guid.Parse("3b169889-2b30-47f5-81fc-4f68fb3369ba")
            },
            new PostTag
            {
                PostId = Guid.Parse("ba6c3283-2f6a-4954-a02d-ecb19dd6e82e"),
                TagId = Guid.Parse("1c326eb8-947a-41e9-a3a9-03a630af7151")
            },
            new PostTag
            {
                PostId = Guid.Parse("ba6c3283-2f6a-4954-a02d-ecb19dd6e82e"),
                TagId = Guid.Parse("c7f3c2e1-5bc4-4c1e-8d88-a9ed542be401")   
            },
            new PostTag
            {
                PostId = Guid.Parse("cc06d511-8b7f-49e4-bab0-3787e54a2a97"),
                TagId = Guid.Parse("b53a915c-c138-4567-9718-d04f7080297d")
            },
            new PostTag
            {
                PostId = Guid.Parse("cc06d511-8b7f-49e4-bab0-3787e54a2a97"),
                TagId = Guid.Parse("1c326eb8-947a-41e9-a3a9-03a630af7151")   // Discussion
            }
        };
}
