using ForumApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ForumApp.Data.Configurations;

public class BoardTagConfiguration : IEntityTypeConfiguration<BoardTag>
{
    public void Configure(EntityTypeBuilder<BoardTag> builder)
    {
        builder
            .HasKey(bt => bt.Id);

        builder
            .Property(bt => bt.Name)
            .IsRequired();

        builder.HasData(
            new BoardTag { Id = 1, Name = "Sticky" },
            new BoardTag { Id = 2, Name = "Question" },
            new BoardTag { Id = 3, Name = "Resolved" });
    }
}
