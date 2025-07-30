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
            .HasMaxLength(DisplayNameMaxLength);

        builder
            .Property(au=>au.UserName)
            .HasMaxLength(UserNameMaxLength);

        builder
            .Property(au => au.JoinDate)
            .HasDefaultValueSql("getutcdate()");

        builder
            .HasData(Users);
    }

    // All seeded users use the password: "Pass123!"
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
            PasswordHash = "AQAAAAIAAYagAAAAELI15kmFwvomcR/m7OSsm6WTA7kiMX9Ls/C+WNyXBIgHMNNylSLekYXdtP4QdUbOgw==",
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
            PasswordHash = "AQAAAAIAAYagAAAAELI15kmFwvomcR/m7OSsm6WTA7kiMX9Ls/C+WNyXBIgHMNNylSLekYXdtP4QdUbOgw==",
            IsDeleted = false,
        },
        new ApplicationUser
        {
            Id = Guid.Parse("f05ffb49-07c4-4b87-81a3-3ea2b479ed75"),
            UserName = "charlie@example.com",
            NormalizedUserName = "CHARLIE@EXAMPLE.COM",
            Email = "charlie@example.com",
            NormalizedEmail = "CHARLIE@EXAMPLE.COM",
            EmailConfirmed = true,
            DisplayName = "Charlie",
            JoinDate = DateTime.UtcNow,
            SecurityStamp = Guid.NewGuid().ToString(),
            ConcurrencyStamp = Guid.NewGuid().ToString(),
            PasswordHash = "AQAAAAIAAYagAAAAELI15kmFwvomcR/m7OSsm6WTA7kiMX9Ls/C+WNyXBIgHMNNylSLekYXdtP4QdUbOgw==",
            IsDeleted = false,
        },
        new ApplicationUser
        {
            Id = Guid.Parse("b1e423d5-32fc-4e18-a2c4-d2a38b1b9a93"),
            UserName = "diana@example.com",
            NormalizedUserName = "DIANA@EXAMPLE.COM",
            Email = "diana@example.com",
            NormalizedEmail = "DIANA@EXAMPLE.COM",
            EmailConfirmed = true,
            DisplayName = "Diana",
            JoinDate = DateTime.UtcNow,
            SecurityStamp = Guid.NewGuid().ToString(),
            ConcurrencyStamp = Guid.NewGuid().ToString(),
            PasswordHash = "AQAAAAIAAYagAAAAELI15kmFwvomcR/m7OSsm6WTA7kiMX9Ls/C+WNyXBIgHMNNylSLekYXdtP4QdUbOgw==",
            IsDeleted = false,
        },
        new ApplicationUser
        {
            Id = Guid.Parse("fe0fc445-3087-4a4e-8a7f-ff6d251f8a56"),
            UserName = "eve@example.com",
            NormalizedUserName = "EVE@EXAMPLE.COM",
            Email = "eve@example.com",
            NormalizedEmail = "EVE@EXAMPLE.COM",
            EmailConfirmed = true,
            DisplayName = "Eve",
            JoinDate = DateTime.UtcNow,
            SecurityStamp = Guid.NewGuid().ToString(),
            ConcurrencyStamp = Guid.NewGuid().ToString(),
            PasswordHash = "AQAAAAIAAYagAAAAELI15kmFwvomcR/m7OSsm6WTA7kiMX9Ls/C+WNyXBIgHMNNylSLekYXdtP4QdUbOgw==",
            IsDeleted = false,
        },
    };
}
