using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ForumApp.Data.Migrations;

/// <inheritdoc />
public partial class SeededNewData : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.InsertData(
            table: "BoardTags",
            columns: new[] { "Id", "Name" },
            values: new object[,]
            {
                { new Guid("1c326eb8-947a-41e9-a3a9-03a630af7151"), "Discussion" },
                { new Guid("3b169889-2b30-47f5-81fc-4f68fb3369ba"), "Announcement" },
                { new Guid("b53a915c-c138-4567-9718-d04f7080297d"), "Hot" }
            });

        migrationBuilder.InsertData(
            table: "Boards",
            columns: new[] { "Id", "CreatedAt", "Description", "IsDeleted", "Name" },
            values: new object[,]
            {
                { new Guid("c5578431-7ae6-4ed9-a402-f1c3401c7100"), new DateTime(2025, 6, 18, 9, 46, 22, 917, DateTimeKind.Utc).AddTicks(9858), "Talk about anything here.", false, "General Discussion" },
                { new Guid("f8385f75-481b-4b70-be0e-c975265e98ba"), new DateTime(2025, 6, 18, 9, 46, 22, 917, DateTimeKind.Utc).AddTicks(9868), "Get help with your tech problems.", false, "Tech Support" }
            });

        migrationBuilder.InsertData(
            table: "Categories",
            columns: new[] { "Id", "Name" },
            values: new object[,]
            {
                { new Guid("5fbd4e2e-a6f9-4d0f-ad91-fa2794d20317"), "News" },
                { new Guid("60f51770-93bc-42b4-a27c-8a280abda112"), "Gaming" },
                { new Guid("67e8a9f8-29d7-444f-bd9b-86225ae41daf"), "Technology" }
            });

        migrationBuilder.InsertData(
            table: "BoardCategories",
            columns: new[] { "BoardId", "CategoryId" },
            values: new object[,]
            {
                { new Guid("c5578431-7ae6-4ed9-a402-f1c3401c7100"), new Guid("67e8a9f8-29d7-444f-bd9b-86225ae41daf") },
                { new Guid("f8385f75-481b-4b70-be0e-c975265e98ba"), new Guid("67e8a9f8-29d7-444f-bd9b-86225ae41daf") }
            });

        migrationBuilder.InsertData(
            table: "Posts",
            columns: new[] { "Id", "BoardId", "Content", "CreatedAt", "IsDeleted", "IsPinned", "ModifiedAt", "Title" },
            values: new object[,]
            {
                { new Guid("6523ec54-87f8-4114-b42b-4e6cb75c802a"), new Guid("f8385f75-481b-4b70-be0e-c975265e98ba"), "My laptop gets very hot when gaming. Any tips?", new DateTime(2025, 6, 18, 9, 46, 22, 919, DateTimeKind.Utc).AddTicks(3935), false, false, new DateTime(2025, 6, 18, 9, 46, 22, 919, DateTimeKind.Utc).AddTicks(3935), "Laptop overheating issue" },
                { new Guid("71d465ed-bd31-4c2c-9700-e1274685ca5d"), new Guid("c5578431-7ae6-4ed9-a402-f1c3401c7100"), "We're glad to have you here.", new DateTime(2025, 6, 18, 9, 46, 22, 919, DateTimeKind.Utc).AddTicks(3927), false, true, new DateTime(2025, 6, 18, 9, 46, 22, 919, DateTimeKind.Utc).AddTicks(3928), "Welcome to the forums!" }
            });

        migrationBuilder.InsertData(
            table: "PostBoardTags",
            columns: new[] { "BoardTagId", "PostId" },
            values: new object[,]
            {
                { new Guid("1c326eb8-947a-41e9-a3a9-03a630af7151"), new Guid("6523ec54-87f8-4114-b42b-4e6cb75c802a") },
                { new Guid("3b169889-2b30-47f5-81fc-4f68fb3369ba"), new Guid("71d465ed-bd31-4c2c-9700-e1274685ca5d") }
            });

        migrationBuilder.InsertData(
            table: "Replies",
            columns: new[] { "Id", "Content", "CreatedAt", "PostId" },
            values: new object[,]
            {
                { new Guid("7bded954-6e81-4e44-a7e3-19234f568f0c"), "Thanks! Happy to be here.", new DateTime(2025, 6, 18, 9, 46, 22, 919, DateTimeKind.Utc).AddTicks(6471), new Guid("71d465ed-bd31-4c2c-9700-e1274685ca5d") },
                { new Guid("9669f2a1-b62d-4a18-8e49-3edabb18d418"), "Try cleaning the fan and applying new thermal paste.", new DateTime(2025, 6, 18, 9, 46, 22, 919, DateTimeKind.Utc).AddTicks(6479), new Guid("6523ec54-87f8-4114-b42b-4e6cb75c802a") }
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData(
            table: "BoardCategories",
            keyColumns: new[] { "BoardId", "CategoryId" },
            keyValues: new object[] { new Guid("c5578431-7ae6-4ed9-a402-f1c3401c7100"), new Guid("67e8a9f8-29d7-444f-bd9b-86225ae41daf") });

        migrationBuilder.DeleteData(
            table: "BoardCategories",
            keyColumns: new[] { "BoardId", "CategoryId" },
            keyValues: new object[] { new Guid("f8385f75-481b-4b70-be0e-c975265e98ba"), new Guid("67e8a9f8-29d7-444f-bd9b-86225ae41daf") });

        migrationBuilder.DeleteData(
            table: "BoardTags",
            keyColumn: "Id",
            keyValue: new Guid("b53a915c-c138-4567-9718-d04f7080297d"));

        migrationBuilder.DeleteData(
            table: "Categories",
            keyColumn: "Id",
            keyValue: new Guid("5fbd4e2e-a6f9-4d0f-ad91-fa2794d20317"));

        migrationBuilder.DeleteData(
            table: "Categories",
            keyColumn: "Id",
            keyValue: new Guid("60f51770-93bc-42b4-a27c-8a280abda112"));

        migrationBuilder.DeleteData(
            table: "PostBoardTags",
            keyColumns: new[] { "BoardTagId", "PostId" },
            keyValues: new object[] { new Guid("1c326eb8-947a-41e9-a3a9-03a630af7151"), new Guid("6523ec54-87f8-4114-b42b-4e6cb75c802a") });

        migrationBuilder.DeleteData(
            table: "PostBoardTags",
            keyColumns: new[] { "BoardTagId", "PostId" },
            keyValues: new object[] { new Guid("3b169889-2b30-47f5-81fc-4f68fb3369ba"), new Guid("71d465ed-bd31-4c2c-9700-e1274685ca5d") });

        migrationBuilder.DeleteData(
            table: "Replies",
            keyColumn: "Id",
            keyValue: new Guid("7bded954-6e81-4e44-a7e3-19234f568f0c"));

        migrationBuilder.DeleteData(
            table: "Replies",
            keyColumn: "Id",
            keyValue: new Guid("9669f2a1-b62d-4a18-8e49-3edabb18d418"));

        migrationBuilder.DeleteData(
            table: "BoardTags",
            keyColumn: "Id",
            keyValue: new Guid("1c326eb8-947a-41e9-a3a9-03a630af7151"));

        migrationBuilder.DeleteData(
            table: "BoardTags",
            keyColumn: "Id",
            keyValue: new Guid("3b169889-2b30-47f5-81fc-4f68fb3369ba"));

        migrationBuilder.DeleteData(
            table: "Categories",
            keyColumn: "Id",
            keyValue: new Guid("67e8a9f8-29d7-444f-bd9b-86225ae41daf"));

        migrationBuilder.DeleteData(
            table: "Posts",
            keyColumn: "Id",
            keyValue: new Guid("6523ec54-87f8-4114-b42b-4e6cb75c802a"));

        migrationBuilder.DeleteData(
            table: "Posts",
            keyColumn: "Id",
            keyValue: new Guid("71d465ed-bd31-4c2c-9700-e1274685ca5d"));

        migrationBuilder.DeleteData(
            table: "Boards",
            keyColumn: "Id",
            keyValue: new Guid("c5578431-7ae6-4ed9-a402-f1c3401c7100"));

        migrationBuilder.DeleteData(
            table: "Boards",
            keyColumn: "Id",
            keyValue: new Guid("f8385f75-481b-4b70-be0e-c975265e98ba"));
    }
}
