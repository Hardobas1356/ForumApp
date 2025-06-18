using ForumApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static ForumApp.GCommon.ValidationConstants.BoardTagConstants;


namespace ForumApp.Data.Configurations;

public class BoardTagConfiguration : IEntityTypeConfiguration<BoardTag>
{
    public void Configure(EntityTypeBuilder<BoardTag> builder)
    {
        builder
            .HasKey(bt => bt.Id);

        builder
            .Property(bt => bt.Id)
            .ValueGeneratedNever();

        builder
            .Property(bt => bt.Name)
            .IsRequired()
            .HasMaxLength(NameMaxLength);

        builder
            .HasData(BoardTags);
    }

    public static List<BoardTag> BoardTags => new()
        {
            new() { Id = Guid.Parse("b53a915c-c138-4567-9718-d04f7080297d"), Name = "Hot" },
            new() { Id = Guid.Parse("1c326eb8-947a-41e9-a3a9-03a630af7151"), Name = "Discussion" },
            new() { Id = Guid.Parse("3b169889-2b30-47f5-81fc-4f68fb3369ba"), Name = "Announcement" }
        };
}
