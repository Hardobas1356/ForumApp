using ForumApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static ForumApp.GCommon.ValidationConstants.TagConstants;


namespace ForumApp.Data.Configurations;

public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder
            .HasKey(t => t.Id);

        builder
            .Property(t => t.Id)
            .ValueGeneratedNever();

        builder
            .Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(NameMaxLength);

        builder
            .Property(t=>t.ColorHex)
            .IsRequired()
            .IsFixedLength()
            .HasMaxLength(ColorHexLength);

        builder
            .HasData(Tags);
    }

    public static List<Tag> Tags => new()
    {
        new() { Id = Guid.Parse("b53a915c-c138-4567-9718-d04f7080297d"), Name = "Hot", ColorHex = "#ff0000" },
        new() { Id = Guid.Parse("1c326eb8-947a-41e9-a3a9-03a630af7151"), Name = "Discussion", ColorHex = "#0000ff" },
        new() { Id = Guid.Parse("3b169889-2b30-47f5-81fc-4f68fb3369ba"), Name = "Announcement", ColorHex = "#00ff00" }
    };
}
