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
            .HasData(Categories);
    }

    public static List<Category> Categories => new()
        {
            new() { Id = Guid.Parse("67e8a9f8-29d7-444f-bd9b-86225ae41daf"), Name = "Technology" },
            new() { Id = Guid.Parse("60f51770-93bc-42b4-a27c-8a280abda112"), Name = "Gaming" },
            new() { Id = Guid.Parse("5fbd4e2e-a6f9-4d0f-ad91-fa2794d20317"), Name = "News" }
        };
}
