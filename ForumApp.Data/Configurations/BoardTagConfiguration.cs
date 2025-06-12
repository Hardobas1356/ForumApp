using ForumApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static ForumApp.Data.Common.BoardTagConstants;

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
    }
}
