using ForumApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ForumApp.Data.Configurations;

public class PostBoardTagConfiguration : IEntityTypeConfiguration<PostBoardTag>
{
    public void Configure(EntityTypeBuilder<PostBoardTag> builder)
    {
        builder
            .HasKey(pbt => new { pbt.BoardTagId, pbt.PostId });

        builder
            .HasOne(pbt => pbt.BoardTag)
            .WithMany(bt => bt.PostBoardTags)
            .HasForeignKey(pbt => pbt.BoardTagId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(pbt=>pbt.Post)
            .WithMany(p=>p.PostBoardTags)
            .HasForeignKey(pbt => pbt.PostId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasData(PostBoardTags);
    }

    public static List<PostBoardTag> PostBoardTags => new()
        {
            new()
            {
                PostId = Guid.Parse("71d465ed-bd31-4c2c-9700-e1274685ca5d"),
                BoardTagId = Guid.Parse("3b169889-2b30-47f5-81fc-4f68fb3369ba")
            },
            new()
            {
                PostId = Guid.Parse("6523ec54-87f8-4114-b42b-4e6cb75c802a"),
                BoardTagId = Guid.Parse("1c326eb8-947a-41e9-a3a9-03a630af7151")
            }
        };
}
