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
            .HasDefaultValueSql("getutcdate()");

        builder
            .HasOne(r=>r.Post)
            .WithMany(p=>p.Replies)
            .HasForeignKey(r=>r.PostId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(r=>r.ApplicationUser)
            .WithMany(au=>au.Replies)
            .HasForeignKey(r=>r.ApplicationUserId)
            .OnDelete(DeleteBehavior.SetNull);

        builder
            .HasQueryFilter(r => !r.IsDeleted);

        builder
            .HasData(Replies);
    }

    public static List<Reply> Replies => new()
    {
        new Reply
        {
            Id = Guid.Parse("7bded954-6e81-4e44-a7e3-19234f568f0c"),
            Content = "Thanks! Happy to be here.",
            CreatedAt = DateTime.UtcNow,
            IsDeleted = false,
            PostId = Guid.Parse("71d465ed-bd31-4c2c-9700-e1274685ca5d"),
            ApplicationUserId = Guid.Parse("e43bb3f7-884a-437b-9a0c-b0d181f07634")
        },
        new Reply
        {
            Id = Guid.Parse("9669f2a1-b62d-4a18-8e49-3edabb18d418"),
            Content = "Try cleaning the fan and applying new thermal paste.",
            CreatedAt = DateTime.UtcNow,
            IsDeleted = false,
            PostId = Guid.Parse("6523ec54-87f8-4114-b42b-4e6cb75c802a"),
            ApplicationUserId = Guid.Parse("7d926fd2-1b4e-4ea7-a019-2bcb179db8f9")
        },
        new Reply
        {
            Id = Guid.Parse("43d1fe5d-3d3e-4972-b94c-85b758afed08"),
            Content = "Nice to meet everyone! Looking forward to great discussions.",
            CreatedAt = DateTime.UtcNow,
            IsDeleted = false,
            PostId = Guid.Parse("ae12507f-cde2-42c3-94ae-3f7d012d7a7d"),
            ApplicationUserId = Guid.Parse("e43bb3f7-884a-437b-9a0c-b0d181f07634")
        },
        new Reply
        {
            Id = Guid.Parse("f4c0cb0f-95b3-41cd-9480-4a1042bb50b5"),
            Content = "Please make sure to follow the rules to keep things respectful.",
            CreatedAt = DateTime.UtcNow,
            IsDeleted = false,
            PostId = Guid.Parse("9829f11a-3d2e-4cb7-b2d6-44c35b3b7ae6"),
            ApplicationUserId = Guid.Parse("e43bb3f7-884a-437b-9a0c-b0d181f07634")
        },
        new Reply
        {
            Id = Guid.Parse("157c45f9-7a91-4038-8e75-f3c47bced2a3"),
            Content = "I’m using the Klim Cool+. It helped lower temps by around 10°C.",
            CreatedAt = DateTime.UtcNow,
            IsDeleted = false,
            PostId = Guid.Parse("ba6c3283-2f6a-4954-a02d-ecb19dd6e82e"),
            ApplicationUserId = Guid.Parse("7d926fd2-1b4e-4ea7-a019-2bcb179db8f9")
        },
        new Reply
        {
            Id = Guid.Parse("6f99b8e3-9e69-4de8-94ec-ff44c315d383"),
            Content = "Glad to hear the thermal paste worked for you!",
            CreatedAt = DateTime.UtcNow,
            IsDeleted = false,
            PostId = Guid.Parse("cc06d511-8b7f-49e4-bab0-3787e54a2a97"),
            ApplicationUserId = Guid.Parse("e43bb3f7-884a-437b-9a0c-b0d181f07634")
        },
        new Reply
        {
            Id = Guid.Parse("a6d17b43-09ad-4462-809d-1e7076bd319c"),
            Content = "You might also want to undervolt your CPU for lower heat.",
            CreatedAt = DateTime.UtcNow,
            IsDeleted = false,
            PostId = Guid.Parse("6523ec54-87f8-4114-b42b-4e6cb75c802a"),
            ApplicationUserId = Guid.Parse("e43bb3f7-884a-437b-9a0c-b0d181f07634")
        }
    };
}
