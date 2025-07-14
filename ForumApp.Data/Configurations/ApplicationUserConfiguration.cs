using ForumApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ForumApp.Data.Configurations;

using static ForumApp.GCommon.ValidationConstants.ApplicationUserConstants;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder
            .Property(au => au.DisplayName)
            .HasMaxLength(DisplayNameMaxLength)
            .IsRequired();

        builder
            .Property(au => au.JoinDate)
            .HasDefaultValueSql("getutcdate()");

        builder
            .HasData(Users);
    }

    public static List<ApplicationUser> Users => new()
    {
        new ApplicationUser
        {
            Id = Guid.Parse("7d926fd2-1b4e-4ea7-a019-2bcb179db8f9"),
            UserName = "alice@example.com",
            NormalizedUserName = "ALICE@EXAMPLE.COM",
            Email = "alice@example.com",
            NormalizedEmail = "ALICE@EXAMPLE.COM",
            EmailConfirmed = true,
            DisplayName = "Alice",
            JoinDate = DateTime.UtcNow,
            SecurityStamp = Guid.NewGuid().ToString(),
            ConcurrencyStamp = Guid.NewGuid().ToString(),
            PasswordHash = "AQAAAAIAAYagAAAAEC9bG6Y4LAGgT2Ih3qsFwL2zHcLv4RYK0zPWYtrsi0P6bq31sMQzmxkAghrUYZ9AIQ==",
            IsDeleted = false,
        },
        new ApplicationUser
        {
            Id = Guid.Parse("e43bb3f7-884a-437b-9a0c-b0d181f07634"),
            UserName = "bob@example.com",
            NormalizedUserName = "BOB@EXAMPLE.COM",
            Email = "bob@example.com",
            NormalizedEmail = "BOB@EXAMPLE.COM",
            EmailConfirmed = true,
            DisplayName = "Bob",
            JoinDate = DateTime.UtcNow,
            SecurityStamp = Guid.NewGuid().ToString(),
            ConcurrencyStamp = Guid.NewGuid().ToString(),
            PasswordHash = "AQAAAAIAAYagAAAAEC9bG6Y4LAGgT2Ih3qsFwL2zHcLv4RYK0zPWYtrsi0P6bq31sMQzmxkAghrUYZ9AIQ==",
            IsDeleted = false,
        }
    };
}
