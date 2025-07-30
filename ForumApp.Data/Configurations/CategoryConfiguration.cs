using ForumApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static ForumApp.GCommon.ValidationConstants.CategoryConstants;

namespace ForumApp.Data.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder
            .HasKey(c => c.Id);

        builder
            .Property(c => c.Id)
            .ValueGeneratedNever();

        builder
            .Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(NameMaxLength);

        builder
            .Property(c => c.ColorHex)
            .IsRequired()
            .HasMaxLength(ColorHexLength)
            .IsFixedLength()
            .HasDefaultValue(ColorHexDefaultValue);

        builder
            .HasData(Categories);
    }

    public static List<Category> Categories => new()
    {
        new()
        {
            Id = Guid.Parse("67e8a9f8-29d7-444f-bd9b-86225ae41daf"),
            Name = "Technology",
            ColorHex = "#FF5733"
        },
        new()
        {
            Id = Guid.Parse("60f51770-93bc-42b4-a27c-8a280abda112"),
            Name = "Gaming",
            ColorHex = "#33C1FF"
        },
        new()
        {
            Id = Guid.Parse("5fbd4e2e-a6f9-4d0f-ad91-fa2794d20317"),
            Name = "News",
            ColorHex = "#28A745"
        },
        new()
        {
            Id = Guid.Parse("a1f8f839-28b3-4e3c-9b2f-4ffae1d23456"),
            Name = "Programming",
            ColorHex = "#8E44AD"
        },
        new()
        {
            Id = Guid.Parse("b2d7f930-4e0f-4a5f-a45f-d84b1e78b789"),
            Name = "Science",
            ColorHex = "#17A2B8"
        },
        new()
        {
            Id = Guid.Parse("c3a6d021-9a6b-43f3-b13b-13db6b9e5432"),
            Name = "Art",
            ColorHex = "#E83E8C"
        },
        new()
        {
            Id = Guid.Parse("d487c912-c1fe-46cc-b872-78a5d96c3f21"),
            Name = "Design",
            ColorHex = "#FFC107"
        },
        new()
        {
            Id = Guid.Parse("e561a103-d567-42b9-a512-5de903abc321"),
            Name = "Off Topic",
            ColorHex = "#6C757D"
        }
    };
}
