using ForumApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static ForumApp.GCommon.ValidationConstants.ReplyConstants;

namespace ForumApp.Data.Configurations;

public class ReplyConfiguration : IEntityTypeConfiguration<Reply>
{
    public void Configure(EntityTypeBuilder<Reply> builder)
    {
        builder
            .HasKey(r => r.Id);

        builder
            .Property(r => r.Id)
            .ValueGeneratedNever();

        builder
            .Property(r => r.Content)
            .IsRequired()
            .HasMaxLength(ContentMaxLength);

        builder
            .Property(r => r.IsDeleted)
            .HasDefaultValue(false);

        builder
            .Property(r => r.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
    }
}
