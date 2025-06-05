using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ForumApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "BoardTags",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Sticky" },
                    { 2, "Question" },
                    { 3, "Resolved" }
                });

            migrationBuilder.InsertData(
                table: "Boards",
                columns: new[] { "Id", "CreatedAt", "Description", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 6, 5, 14, 48, 47, 297, DateTimeKind.Local).AddTicks(1278), "General discussion board", false, "General" },
                    { 2, new DateTime(2025, 6, 5, 14, 48, 47, 297, DateTimeKind.Local).AddTicks(1336), "Official announcements", false, "Announcements" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Community" },
                    { 2, "Support" }
                });

            migrationBuilder.InsertData(
                table: "BoardCategories",
                columns: new[] { "BoardId", "CategoryId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "BoardId", "Content", "CreatedAt", "IsDeleted", "IsPinned", "ModifiedAt", "Title" },
                values: new object[,]
                {
                    { 1, 1, "Introduce yourself here.", new DateTime(2025, 6, 5, 11, 48, 47, 298, DateTimeKind.Utc).AddTicks(2056), false, false, new DateTime(2025, 6, 5, 11, 48, 47, 298, DateTimeKind.Utc).AddTicks(2056), "Welcome to the Forum!" },
                    { 2, 2, "Please read before posting.", new DateTime(2025, 6, 5, 11, 48, 47, 298, DateTimeKind.Utc).AddTicks(2061), false, true, new DateTime(2025, 6, 5, 11, 48, 47, 298, DateTimeKind.Utc).AddTicks(2061), "Site Rules" }
                });

            migrationBuilder.InsertData(
                table: "PostBoardTags",
                columns: new[] { "BoardTagId", "PostId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 2, 1 },
                    { 3, 2 }
                });

            migrationBuilder.InsertData(
                table: "Replies",
                columns: new[] { "Id", "Content", "CreatedAt", "PostId" },
                values: new object[,]
                {
                    { 1, "Hello everyone!", new DateTime(2025, 6, 5, 11, 48, 47, 298, DateTimeKind.Utc).AddTicks(3760), 1 },
                    { 2, "Thanks for the heads-up.", new DateTime(2025, 6, 5, 11, 48, 47, 298, DateTimeKind.Utc).AddTicks(3761), 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BoardCategories",
                keyColumns: new[] { "BoardId", "CategoryId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "BoardCategories",
                keyColumns: new[] { "BoardId", "CategoryId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "PostBoardTags",
                keyColumns: new[] { "BoardTagId", "PostId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "PostBoardTags",
                keyColumns: new[] { "BoardTagId", "PostId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "PostBoardTags",
                keyColumns: new[] { "BoardTagId", "PostId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "PostBoardTags",
                keyColumns: new[] { "BoardTagId", "PostId" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "Replies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Replies",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "BoardTags",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BoardTags",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "BoardTags",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Boards",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Boards",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
