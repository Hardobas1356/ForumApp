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
    }
}
