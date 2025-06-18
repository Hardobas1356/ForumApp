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
    }
}
