using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ForumApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class MoreDataSeeded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PostTags",
                keyColumns: new[] { "PostId", "TagId" },
                keyValues: new object[] { new Guid("6523ec54-87f8-4114-b42b-4e6cb75c802a"), new Guid("1c326eb8-947a-41e9-a3a9-03a630af7151") });

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Replies",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                comment: "Comment of reply",
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150,
                oldComment: "Comment of reply");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("7d926fd2-1b4e-4ea7-a019-2bcb179db8f9"),
                columns: new[] { "ConcurrencyStamp", "JoinDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1abdc80b-4eef-4f81-91a3-7142f93256f4", new DateTime(2025, 7, 30, 10, 3, 55, 73, DateTimeKind.Utc).AddTicks(4902), "AQAAAAIAAYagAAAAELI15kmFwvomcR/m7OSsm6WTA7kiMX9Ls/C+WNyXBIgHMNNylSLekYXdtP4QdUbOgw==", "52d7d254-dd1e-4ece-8670-e36bba06db41" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e43bb3f7-884a-437b-9a0c-b0d181f07634"),
                columns: new[] { "ConcurrencyStamp", "JoinDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "70f35a00-c313-4f0c-8f2b-f2fffde24b83", new DateTime(2025, 7, 30, 10, 3, 55, 73, DateTimeKind.Utc).AddTicks(4930), "AQAAAAIAAYagAAAAELI15kmFwvomcR/m7OSsm6WTA7kiMX9Ls/C+WNyXBIgHMNNylSLekYXdtP4QdUbOgw==", "540ebf24-76ca-4d7c-8f3b-664ffd5df909" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DisplayName", "Email", "EmailConfirmed", "ImageUrl", "IsDeleted", "JoinDate", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("b1e423d5-32fc-4e18-a2c4-d2a38b1b9a93"), 0, "786052ec-e4c4-4152-938e-d3e1b773b9e5", "Diana", "diana@example.com", true, null, false, new DateTime(2025, 7, 30, 10, 3, 55, 73, DateTimeKind.Utc).AddTicks(4943), false, null, "DIANA@EXAMPLE.COM", "DIANA@EXAMPLE.COM", "AQAAAAIAAYagAAAAELI15kmFwvomcR/m7OSsm6WTA7kiMX9Ls/C+WNyXBIgHMNNylSLekYXdtP4QdUbOgw==", null, false, "1d60b57e-0f69-4157-b3ab-8392490593a3", false, "diana@example.com" },
                    { new Guid("f05ffb49-07c4-4b87-81a3-3ea2b479ed75"), 0, "2b0f526c-5ee6-4623-a63d-c7202041f495", "Charlie", "charlie@example.com", true, null, false, new DateTime(2025, 7, 30, 10, 3, 55, 73, DateTimeKind.Utc).AddTicks(4937), false, null, "CHARLIE@EXAMPLE.COM", "CHARLIE@EXAMPLE.COM", "AQAAAAIAAYagAAAAELI15kmFwvomcR/m7OSsm6WTA7kiMX9Ls/C+WNyXBIgHMNNylSLekYXdtP4QdUbOgw==", null, false, "81cd9f9d-2638-475d-95a2-878c7a459260", false, "charlie@example.com" },
                    { new Guid("fe0fc445-3087-4a4e-8a7f-ff6d251f8a56"), 0, "b4c582c5-0f62-40ab-b842-d440fc0b643c", "Eve", "eve@example.com", true, null, false, new DateTime(2025, 7, 30, 10, 3, 55, 73, DateTimeKind.Utc).AddTicks(4954), false, null, "EVE@EXAMPLE.COM", "EVE@EXAMPLE.COM", "AQAAAAIAAYagAAAAELI15kmFwvomcR/m7OSsm6WTA7kiMX9Ls/C+WNyXBIgHMNNylSLekYXdtP4QdUbOgw==", null, false, "ca9e76d7-4aea-4108-90b1-0446c9d56e55", false, "eve@example.com" }
                });

            migrationBuilder.InsertData(
                table: "BoardManagers",
                columns: new[] { "ApplicationUserId", "BoardId", "IsDeleted" },
                values: new object[,]
                {
                    { new Guid("7d926fd2-1b4e-4ea7-a019-2bcb179db8f9"), new Guid("c5578431-7ae6-4ed9-a402-f1c3401c7100"), false },
                    { new Guid("e43bb3f7-884a-437b-9a0c-b0d181f07634"), new Guid("f8385f75-481b-4b70-be0e-c975265e98ba"), false }
                });

            migrationBuilder.UpdateData(
                table: "Boards",
                keyColumn: "Id",
                keyValue: new Guid("c5578431-7ae6-4ed9-a402-f1c3401c7100"),
                column: "CreatedAt",
                value: new DateTime(2025, 7, 30, 10, 3, 55, 74, DateTimeKind.Utc).AddTicks(2800));

            migrationBuilder.UpdateData(
                table: "Boards",
                keyColumn: "Id",
                keyValue: new Guid("f8385f75-481b-4b70-be0e-c975265e98ba"),
                column: "CreatedAt",
                value: new DateTime(2025, 7, 30, 10, 3, 55, 74, DateTimeKind.Utc).AddTicks(2808));

            migrationBuilder.InsertData(
                table: "Boards",
                columns: new[] { "Id", "CreatedAt", "Description", "ImageUrl", "IsApproved", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { new Guid("aa0a3c1e-1b6d-4a7c-a4d9-eee80b91b1a1"), new DateTime(2025, 7, 30, 10, 3, 55, 74, DateTimeKind.Utc).AddTicks(2811), "Discuss the latest games, platforms, and news.", null, true, false, "Gaming" },
                    { new Guid("bb2c4e8f-3f3d-49b3-8417-07de67b4b1b2"), new DateTime(2025, 7, 30, 10, 3, 55, 74, DateTimeKind.Utc).AddTicks(2814), "Share code, ask questions, and learn programming.", null, true, false, "Programming" },
                    { new Guid("cc3e7fa0-61f6-4a7a-bb4b-1fcda248c1c3"), new DateTime(2025, 7, 30, 10, 3, 55, 74, DateTimeKind.Utc).AddTicks(2822), "Talk about science, tech breakthroughs, and innovations.", null, true, false, "Science & Technology" },
                    { new Guid("dd4f9cb1-9a7e-4c5a-9c6b-3bba9e1d41d4"), new DateTime(2025, 7, 30, 10, 3, 55, 74, DateTimeKind.Utc).AddTicks(2825), "Showcase your art, get feedback, and explore creativity.", null, true, false, "Art & Design" },
                    { new Guid("ee510dc2-afbf-4a38-b97f-e6f3c4eb51e5"), new DateTime(2025, 7, 30, 10, 3, 55, 74, DateTimeKind.Utc).AddTicks(2858), "Anything that doesn't fit elsewhere goes here.", null, true, false, "Off Topic" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "ColorHex", "Name" },
                values: new object[,]
                {
                    { new Guid("a1f8f839-28b3-4e3c-9b2f-4ffae1d23456"), "#8E44AD", "Programming" },
                    { new Guid("b2d7f930-4e0f-4a5f-a45f-d84b1e78b789"), "#17A2B8", "Science" },
                    { new Guid("c3a6d021-9a6b-43f3-b13b-13db6b9e5432"), "#E83E8C", "Art" },
                    { new Guid("d487c912-c1fe-46cc-b872-78a5d96c3f21"), "#FFC107", "Design" },
                    { new Guid("e561a103-d567-42b9-a512-5de903abc321"), "#6C757D", "Off Topic" }
                });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("6523ec54-87f8-4114-b42b-4e6cb75c802a"),
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 7, 30, 10, 3, 55, 75, DateTimeKind.Utc).AddTicks(4075), new DateTime(2025, 7, 30, 10, 3, 55, 75, DateTimeKind.Utc).AddTicks(4076) });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("71d465ed-bd31-4c2c-9700-e1274685ca5d"),
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 7, 30, 10, 3, 55, 75, DateTimeKind.Utc).AddTicks(4067), new DateTime(2025, 7, 30, 10, 3, 55, 75, DateTimeKind.Utc).AddTicks(4068) });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "ApplicationUserId", "BoardId", "Content", "CreatedAt", "ImageUrl", "IsDeleted", "IsPinned", "ModifiedAt", "Title" },
                values: new object[,]
                {
                    { new Guid("9829f11a-3d2e-4cb7-b2d6-44c35b3b7ae6"), new Guid("7d926fd2-1b4e-4ea7-a019-2bcb179db8f9"), new Guid("c5578431-7ae6-4ed9-a402-f1c3401c7100"), "Please read the rules before posting. Be respectful!", new DateTime(2025, 7, 30, 10, 3, 55, 75, DateTimeKind.Utc).AddTicks(4132), null, false, true, new DateTime(2025, 7, 30, 10, 3, 55, 75, DateTimeKind.Utc).AddTicks(4132), "Forum Rules and Guidelines" },
                    { new Guid("ae12507f-cde2-42c3-94ae-3f7d012d7a7d"), new Guid("7d926fd2-1b4e-4ea7-a019-2bcb179db8f9"), new Guid("c5578431-7ae6-4ed9-a402-f1c3401c7100"), "Say hello and tell us a bit about yourself.", new DateTime(2025, 7, 30, 10, 3, 55, 75, DateTimeKind.Utc).AddTicks(4081), null, false, false, new DateTime(2025, 7, 30, 10, 3, 55, 75, DateTimeKind.Utc).AddTicks(4082), "Introduce Yourself!" },
                    { new Guid("ba6c3283-2f6a-4954-a02d-ecb19dd6e82e"), new Guid("e43bb3f7-884a-437b-9a0c-b0d181f07634"), new Guid("f8385f75-481b-4b70-be0e-c975265e98ba"), "Looking for a reliable cooling pad. Any recommendations?", new DateTime(2025, 7, 30, 10, 3, 55, 75, DateTimeKind.Utc).AddTicks(4137), null, false, false, new DateTime(2025, 7, 30, 10, 3, 55, 75, DateTimeKind.Utc).AddTicks(4137), "Best cooling pads for laptops?" },
                    { new Guid("cc06d511-8b7f-49e4-bab0-3787e54a2a97"), new Guid("e43bb3f7-884a-437b-9a0c-b0d181f07634"), new Guid("f8385f75-481b-4b70-be0e-c975265e98ba"), "I applied new thermal paste and it helped a lot. Thanks all!", new DateTime(2025, 7, 30, 10, 3, 55, 75, DateTimeKind.Utc).AddTicks(4149), null, false, false, new DateTime(2025, 7, 30, 10, 3, 55, 75, DateTimeKind.Utc).AddTicks(4149), "Update: Thermal paste worked!" }
                });

            migrationBuilder.UpdateData(
                table: "Replies",
                keyColumn: "Id",
                keyValue: new Guid("7bded954-6e81-4e44-a7e3-19234f568f0c"),
                column: "CreatedAt",
                value: new DateTime(2025, 7, 30, 10, 3, 55, 76, DateTimeKind.Utc).AddTicks(3153));

            migrationBuilder.UpdateData(
                table: "Replies",
                keyColumn: "Id",
                keyValue: new Guid("9669f2a1-b62d-4a18-8e49-3edabb18d418"),
                column: "CreatedAt",
                value: new DateTime(2025, 7, 30, 10, 3, 55, 76, DateTimeKind.Utc).AddTicks(3160));

            migrationBuilder.InsertData(
                table: "Replies",
                columns: new[] { "Id", "ApplicationUserId", "Content", "CreatedAt", "PostId" },
                values: new object[] { new Guid("a6d17b43-09ad-4462-809d-1e7076bd319c"), new Guid("e43bb3f7-884a-437b-9a0c-b0d181f07634"), "You might also want to undervolt your CPU for lower heat.", new DateTime(2025, 7, 30, 10, 3, 55, 76, DateTimeKind.Utc).AddTicks(3180), new Guid("6523ec54-87f8-4114-b42b-4e6cb75c802a") });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "ColorHex", "Name" },
                values: new object[,]
                {
                    { new Guid("6fd77bb8-b44d-42a6-b4b5-d217b99b1d7d"), "#6610f2", "Showcase" },
                    { new Guid("c7f3c2e1-5bc4-4c1e-8d88-a9ed542be401"), "#17A2B8", "Help" },
                    { new Guid("e8b93d7d-d267-44fb-8914-ff08b7ffbd90"), "#FFC107", "Question" },
                    { new Guid("f1a2be0e-1cd2-4e6d-87b4-1e5039db92c5"), "#6C757D", "Feedback" }
                });

            migrationBuilder.InsertData(
                table: "BoardCategories",
                columns: new[] { "BoardId", "CategoryId" },
                values: new object[,]
                {
                    { new Guid("aa0a3c1e-1b6d-4a7c-a4d9-eee80b91b1a1"), new Guid("60f51770-93bc-42b4-a27c-8a280abda112") },
                    { new Guid("bb2c4e8f-3f3d-49b3-8417-07de67b4b1b2"), new Guid("a1f8f839-28b3-4e3c-9b2f-4ffae1d23456") },
                    { new Guid("cc3e7fa0-61f6-4a7a-bb4b-1fcda248c1c3"), new Guid("b2d7f930-4e0f-4a5f-a45f-d84b1e78b789") },
                    { new Guid("dd4f9cb1-9a7e-4c5a-9c6b-3bba9e1d41d4"), new Guid("c3a6d021-9a6b-43f3-b13b-13db6b9e5432") },
                    { new Guid("dd4f9cb1-9a7e-4c5a-9c6b-3bba9e1d41d4"), new Guid("d487c912-c1fe-46cc-b872-78a5d96c3f21") },
                    { new Guid("ee510dc2-afbf-4a38-b97f-e6f3c4eb51e5"), new Guid("e561a103-d567-42b9-a512-5de903abc321") }
                });

            migrationBuilder.InsertData(
                table: "PostTags",
                columns: new[] { "PostId", "TagId" },
                values: new object[,]
                {
                    { new Guid("ba6c3283-2f6a-4954-a02d-ecb19dd6e82e"), new Guid("1c326eb8-947a-41e9-a3a9-03a630af7151") },
                    { new Guid("cc06d511-8b7f-49e4-bab0-3787e54a2a97"), new Guid("1c326eb8-947a-41e9-a3a9-03a630af7151") },
                    { new Guid("9829f11a-3d2e-4cb7-b2d6-44c35b3b7ae6"), new Guid("3b169889-2b30-47f5-81fc-4f68fb3369ba") },
                    { new Guid("cc06d511-8b7f-49e4-bab0-3787e54a2a97"), new Guid("b53a915c-c138-4567-9718-d04f7080297d") },
                    { new Guid("ba6c3283-2f6a-4954-a02d-ecb19dd6e82e"), new Guid("c7f3c2e1-5bc4-4c1e-8d88-a9ed542be401") },
                    { new Guid("6523ec54-87f8-4114-b42b-4e6cb75c802a"), new Guid("e8b93d7d-d267-44fb-8914-ff08b7ffbd90") },
                    { new Guid("ae12507f-cde2-42c3-94ae-3f7d012d7a7d"), new Guid("f1a2be0e-1cd2-4e6d-87b4-1e5039db92c5") }
                });

            migrationBuilder.InsertData(
                table: "Replies",
                columns: new[] { "Id", "ApplicationUserId", "Content", "CreatedAt", "PostId" },
                values: new object[,]
                {
                    { new Guid("157c45f9-7a91-4038-8e75-f3c47bced2a3"), new Guid("7d926fd2-1b4e-4ea7-a019-2bcb179db8f9"), "I’m using the Klim Cool+. It helped lower temps by around 10°C.", new DateTime(2025, 7, 30, 10, 3, 55, 76, DateTimeKind.Utc).AddTicks(3172), new Guid("ba6c3283-2f6a-4954-a02d-ecb19dd6e82e") },
                    { new Guid("43d1fe5d-3d3e-4972-b94c-85b758afed08"), new Guid("e43bb3f7-884a-437b-9a0c-b0d181f07634"), "Nice to meet everyone! Looking forward to great discussions.", new DateTime(2025, 7, 30, 10, 3, 55, 76, DateTimeKind.Utc).AddTicks(3164), new Guid("ae12507f-cde2-42c3-94ae-3f7d012d7a7d") },
                    { new Guid("6f99b8e3-9e69-4de8-94ec-ff44c315d383"), new Guid("e43bb3f7-884a-437b-9a0c-b0d181f07634"), "Glad to hear the thermal paste worked for you!", new DateTime(2025, 7, 30, 10, 3, 55, 76, DateTimeKind.Utc).AddTicks(3177), new Guid("cc06d511-8b7f-49e4-bab0-3787e54a2a97") },
                    { new Guid("f4c0cb0f-95b3-41cd-9480-4a1042bb50b5"), new Guid("e43bb3f7-884a-437b-9a0c-b0d181f07634"), "Please make sure to follow the rules to keep things respectful.", new DateTime(2025, 7, 30, 10, 3, 55, 76, DateTimeKind.Utc).AddTicks(3168), new Guid("9829f11a-3d2e-4cb7-b2d6-44c35b3b7ae6") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("b1e423d5-32fc-4e18-a2c4-d2a38b1b9a93"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f05ffb49-07c4-4b87-81a3-3ea2b479ed75"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fe0fc445-3087-4a4e-8a7f-ff6d251f8a56"));

            migrationBuilder.DeleteData(
                table: "BoardCategories",
                keyColumns: new[] { "BoardId", "CategoryId" },
                keyValues: new object[] { new Guid("aa0a3c1e-1b6d-4a7c-a4d9-eee80b91b1a1"), new Guid("60f51770-93bc-42b4-a27c-8a280abda112") });

            migrationBuilder.DeleteData(
                table: "BoardCategories",
                keyColumns: new[] { "BoardId", "CategoryId" },
                keyValues: new object[] { new Guid("bb2c4e8f-3f3d-49b3-8417-07de67b4b1b2"), new Guid("a1f8f839-28b3-4e3c-9b2f-4ffae1d23456") });

            migrationBuilder.DeleteData(
                table: "BoardCategories",
                keyColumns: new[] { "BoardId", "CategoryId" },
                keyValues: new object[] { new Guid("cc3e7fa0-61f6-4a7a-bb4b-1fcda248c1c3"), new Guid("b2d7f930-4e0f-4a5f-a45f-d84b1e78b789") });

            migrationBuilder.DeleteData(
                table: "BoardCategories",
                keyColumns: new[] { "BoardId", "CategoryId" },
                keyValues: new object[] { new Guid("dd4f9cb1-9a7e-4c5a-9c6b-3bba9e1d41d4"), new Guid("c3a6d021-9a6b-43f3-b13b-13db6b9e5432") });

            migrationBuilder.DeleteData(
                table: "BoardCategories",
                keyColumns: new[] { "BoardId", "CategoryId" },
                keyValues: new object[] { new Guid("dd4f9cb1-9a7e-4c5a-9c6b-3bba9e1d41d4"), new Guid("d487c912-c1fe-46cc-b872-78a5d96c3f21") });

            migrationBuilder.DeleteData(
                table: "BoardCategories",
                keyColumns: new[] { "BoardId", "CategoryId" },
                keyValues: new object[] { new Guid("ee510dc2-afbf-4a38-b97f-e6f3c4eb51e5"), new Guid("e561a103-d567-42b9-a512-5de903abc321") });

            migrationBuilder.DeleteData(
                table: "BoardManagers",
                keyColumns: new[] { "ApplicationUserId", "BoardId" },
                keyValues: new object[] { new Guid("7d926fd2-1b4e-4ea7-a019-2bcb179db8f9"), new Guid("c5578431-7ae6-4ed9-a402-f1c3401c7100") });

            migrationBuilder.DeleteData(
                table: "BoardManagers",
                keyColumns: new[] { "ApplicationUserId", "BoardId" },
                keyValues: new object[] { new Guid("e43bb3f7-884a-437b-9a0c-b0d181f07634"), new Guid("f8385f75-481b-4b70-be0e-c975265e98ba") });

            migrationBuilder.DeleteData(
                table: "PostTags",
                keyColumns: new[] { "PostId", "TagId" },
                keyValues: new object[] { new Guid("ba6c3283-2f6a-4954-a02d-ecb19dd6e82e"), new Guid("1c326eb8-947a-41e9-a3a9-03a630af7151") });

            migrationBuilder.DeleteData(
                table: "PostTags",
                keyColumns: new[] { "PostId", "TagId" },
                keyValues: new object[] { new Guid("cc06d511-8b7f-49e4-bab0-3787e54a2a97"), new Guid("1c326eb8-947a-41e9-a3a9-03a630af7151") });

            migrationBuilder.DeleteData(
                table: "PostTags",
                keyColumns: new[] { "PostId", "TagId" },
                keyValues: new object[] { new Guid("9829f11a-3d2e-4cb7-b2d6-44c35b3b7ae6"), new Guid("3b169889-2b30-47f5-81fc-4f68fb3369ba") });

            migrationBuilder.DeleteData(
                table: "PostTags",
                keyColumns: new[] { "PostId", "TagId" },
                keyValues: new object[] { new Guid("cc06d511-8b7f-49e4-bab0-3787e54a2a97"), new Guid("b53a915c-c138-4567-9718-d04f7080297d") });

            migrationBuilder.DeleteData(
                table: "PostTags",
                keyColumns: new[] { "PostId", "TagId" },
                keyValues: new object[] { new Guid("ba6c3283-2f6a-4954-a02d-ecb19dd6e82e"), new Guid("c7f3c2e1-5bc4-4c1e-8d88-a9ed542be401") });

            migrationBuilder.DeleteData(
                table: "PostTags",
                keyColumns: new[] { "PostId", "TagId" },
                keyValues: new object[] { new Guid("6523ec54-87f8-4114-b42b-4e6cb75c802a"), new Guid("e8b93d7d-d267-44fb-8914-ff08b7ffbd90") });

            migrationBuilder.DeleteData(
                table: "PostTags",
                keyColumns: new[] { "PostId", "TagId" },
                keyValues: new object[] { new Guid("ae12507f-cde2-42c3-94ae-3f7d012d7a7d"), new Guid("f1a2be0e-1cd2-4e6d-87b4-1e5039db92c5") });

            migrationBuilder.DeleteData(
                table: "Replies",
                keyColumn: "Id",
                keyValue: new Guid("157c45f9-7a91-4038-8e75-f3c47bced2a3"));

            migrationBuilder.DeleteData(
                table: "Replies",
                keyColumn: "Id",
                keyValue: new Guid("43d1fe5d-3d3e-4972-b94c-85b758afed08"));

            migrationBuilder.DeleteData(
                table: "Replies",
                keyColumn: "Id",
                keyValue: new Guid("6f99b8e3-9e69-4de8-94ec-ff44c315d383"));

            migrationBuilder.DeleteData(
                table: "Replies",
                keyColumn: "Id",
                keyValue: new Guid("a6d17b43-09ad-4462-809d-1e7076bd319c"));

            migrationBuilder.DeleteData(
                table: "Replies",
                keyColumn: "Id",
                keyValue: new Guid("f4c0cb0f-95b3-41cd-9480-4a1042bb50b5"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("6fd77bb8-b44d-42a6-b4b5-d217b99b1d7d"));

            migrationBuilder.DeleteData(
                table: "Boards",
                keyColumn: "Id",
                keyValue: new Guid("aa0a3c1e-1b6d-4a7c-a4d9-eee80b91b1a1"));

            migrationBuilder.DeleteData(
                table: "Boards",
                keyColumn: "Id",
                keyValue: new Guid("bb2c4e8f-3f3d-49b3-8417-07de67b4b1b2"));

            migrationBuilder.DeleteData(
                table: "Boards",
                keyColumn: "Id",
                keyValue: new Guid("cc3e7fa0-61f6-4a7a-bb4b-1fcda248c1c3"));

            migrationBuilder.DeleteData(
                table: "Boards",
                keyColumn: "Id",
                keyValue: new Guid("dd4f9cb1-9a7e-4c5a-9c6b-3bba9e1d41d4"));

            migrationBuilder.DeleteData(
                table: "Boards",
                keyColumn: "Id",
                keyValue: new Guid("ee510dc2-afbf-4a38-b97f-e6f3c4eb51e5"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a1f8f839-28b3-4e3c-9b2f-4ffae1d23456"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b2d7f930-4e0f-4a5f-a45f-d84b1e78b789"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("c3a6d021-9a6b-43f3-b13b-13db6b9e5432"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("d487c912-c1fe-46cc-b872-78a5d96c3f21"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("e561a103-d567-42b9-a512-5de903abc321"));

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("9829f11a-3d2e-4cb7-b2d6-44c35b3b7ae6"));

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("ae12507f-cde2-42c3-94ae-3f7d012d7a7d"));

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("ba6c3283-2f6a-4954-a02d-ecb19dd6e82e"));

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("cc06d511-8b7f-49e4-bab0-3787e54a2a97"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("c7f3c2e1-5bc4-4c1e-8d88-a9ed542be401"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("e8b93d7d-d267-44fb-8914-ff08b7ffbd90"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("f1a2be0e-1cd2-4e6d-87b4-1e5039db92c5"));

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Replies",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                comment: "Comment of reply",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldComment: "Comment of reply");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("7d926fd2-1b4e-4ea7-a019-2bcb179db8f9"),
                columns: new[] { "ConcurrencyStamp", "JoinDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "29967006-8855-400f-acae-0b4a0d88c10f", new DateTime(2025, 7, 24, 14, 13, 49, 545, DateTimeKind.Utc).AddTicks(4190), "AQAAAAIAAYagAAAAEC9bG6Y4LAGgT2Ih3qsFwL2zHcLv4RYK0zPWYtrsi0P6bq31sMQzmxkAghrUYZ9AIQ==", "44417fc7-a0cc-4043-a510-2f54e25cfeef" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e43bb3f7-884a-437b-9a0c-b0d181f07634"),
                columns: new[] { "ConcurrencyStamp", "JoinDate", "PasswordHash", "SecurityStamp" },
                values: new object[] { "27a25b66-c9af-456b-85de-1e84277d07fb", new DateTime(2025, 7, 24, 14, 13, 49, 545, DateTimeKind.Utc).AddTicks(4215), "AQAAAAIAAYagAAAAEC9bG6Y4LAGgT2Ih3qsFwL2zHcLv4RYK0zPWYtrsi0P6bq31sMQzmxkAghrUYZ9AIQ==", "66d6ddb2-d491-48e1-a740-b48b95dd14b1" });

            migrationBuilder.UpdateData(
                table: "Boards",
                keyColumn: "Id",
                keyValue: new Guid("c5578431-7ae6-4ed9-a402-f1c3401c7100"),
                column: "CreatedAt",
                value: new DateTime(2025, 7, 24, 14, 13, 49, 546, DateTimeKind.Utc).AddTicks(1164));

            migrationBuilder.UpdateData(
                table: "Boards",
                keyColumn: "Id",
                keyValue: new Guid("f8385f75-481b-4b70-be0e-c975265e98ba"),
                column: "CreatedAt",
                value: new DateTime(2025, 7, 24, 14, 13, 49, 546, DateTimeKind.Utc).AddTicks(1198));

            migrationBuilder.InsertData(
                table: "PostTags",
                columns: new[] { "PostId", "TagId" },
                values: new object[] { new Guid("6523ec54-87f8-4114-b42b-4e6cb75c802a"), new Guid("1c326eb8-947a-41e9-a3a9-03a630af7151") });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("6523ec54-87f8-4114-b42b-4e6cb75c802a"),
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 7, 24, 14, 13, 49, 547, DateTimeKind.Utc).AddTicks(1162), new DateTime(2025, 7, 24, 14, 13, 49, 547, DateTimeKind.Utc).AddTicks(1162) });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("71d465ed-bd31-4c2c-9700-e1274685ca5d"),
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 7, 24, 14, 13, 49, 547, DateTimeKind.Utc).AddTicks(1153), new DateTime(2025, 7, 24, 14, 13, 49, 547, DateTimeKind.Utc).AddTicks(1153) });

            migrationBuilder.UpdateData(
                table: "Replies",
                keyColumn: "Id",
                keyValue: new Guid("7bded954-6e81-4e44-a7e3-19234f568f0c"),
                column: "CreatedAt",
                value: new DateTime(2025, 7, 24, 14, 13, 49, 547, DateTimeKind.Utc).AddTicks(9302));

            migrationBuilder.UpdateData(
                table: "Replies",
                keyColumn: "Id",
                keyValue: new Guid("9669f2a1-b62d-4a18-8e49-3edabb18d418"),
                column: "CreatedAt",
                value: new DateTime(2025, 7, 24, 14, 13, 49, 547, DateTimeKind.Utc).AddTicks(9309));
        }
    }
}
