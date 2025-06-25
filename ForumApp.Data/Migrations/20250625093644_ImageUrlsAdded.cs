using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForumApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class ImageUrlsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true,
                comment: "Url for optional picture");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Boards",
                type: "nvarchar(max)",
                nullable: true,
                comment: "Url for board icon");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("7d926fd2-1b4e-4ea7-a019-2bcb179db8f9"),
                columns: new[] { "ConcurrencyStamp", "JoinDate", "SecurityStamp" },
                values: new object[] { "d264b700-e512-4d10-8cc5-05553553c398", new DateTime(2025, 6, 25, 9, 36, 43, 469, DateTimeKind.Utc).AddTicks(1336), "ca2b9db6-ac9f-4605-b0ed-1a703714e374" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e43bb3f7-884a-437b-9a0c-b0d181f07634"),
                columns: new[] { "ConcurrencyStamp", "JoinDate", "SecurityStamp" },
                values: new object[] { "d2cac92e-55f0-4cc4-9b10-3067a4e37b40", new DateTime(2025, 6, 25, 9, 36, 43, 469, DateTimeKind.Utc).AddTicks(1358), "3b98cb18-3bc3-4d0a-97ba-a67e05d03abc" });

            migrationBuilder.UpdateData(
                table: "Boards",
                keyColumn: "Id",
                keyValue: new Guid("c5578431-7ae6-4ed9-a402-f1c3401c7100"),
                columns: new[] { "CreatedAt", "ImageUrl" },
                values: new object[] { new DateTime(2025, 6, 25, 9, 36, 43, 469, DateTimeKind.Utc).AddTicks(8377), null });

            migrationBuilder.UpdateData(
                table: "Boards",
                keyColumn: "Id",
                keyValue: new Guid("f8385f75-481b-4b70-be0e-c975265e98ba"),
                columns: new[] { "CreatedAt", "ImageUrl" },
                values: new object[] { new DateTime(2025, 6, 25, 9, 36, 43, 469, DateTimeKind.Utc).AddTicks(8384), "data:image/jpeg;base64,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" });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("6523ec54-87f8-4114-b42b-4e6cb75c802a"),
                columns: new[] { "CreatedAt", "ImageUrl", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 6, 25, 9, 36, 43, 470, DateTimeKind.Utc).AddTicks(3671), null, new DateTime(2025, 6, 25, 9, 36, 43, 470, DateTimeKind.Utc).AddTicks(3672) });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("71d465ed-bd31-4c2c-9700-e1274685ca5d"),
                columns: new[] { "CreatedAt", "ImageUrl", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 6, 25, 9, 36, 43, 470, DateTimeKind.Utc).AddTicks(3634), null, new DateTime(2025, 6, 25, 9, 36, 43, 470, DateTimeKind.Utc).AddTicks(3635) });

            migrationBuilder.UpdateData(
                table: "Replies",
                keyColumn: "Id",
                keyValue: new Guid("7bded954-6e81-4e44-a7e3-19234f568f0c"),
                column: "CreatedAt",
                value: new DateTime(2025, 6, 25, 9, 36, 43, 471, DateTimeKind.Utc).AddTicks(1880));

            migrationBuilder.UpdateData(
                table: "Replies",
                keyColumn: "Id",
                keyValue: new Guid("9669f2a1-b62d-4a18-8e49-3edabb18d418"),
                column: "CreatedAt",
                value: new DateTime(2025, 6, 25, 9, 36, 43, 471, DateTimeKind.Utc).AddTicks(1889));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Boards");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("7d926fd2-1b4e-4ea7-a019-2bcb179db8f9"),
                columns: new[] { "ConcurrencyStamp", "JoinDate", "SecurityStamp" },
                values: new object[] { "cbfd31de-b86d-4d76-8976-7d3980d6052d", new DateTime(2025, 6, 19, 12, 53, 1, 43, DateTimeKind.Utc).AddTicks(140), "21b906ae-588a-41c6-bf71-b83192c3f78a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e43bb3f7-884a-437b-9a0c-b0d181f07634"),
                columns: new[] { "ConcurrencyStamp", "JoinDate", "SecurityStamp" },
                values: new object[] { "a387644c-63dc-4e25-bb29-1d3dee8cd462", new DateTime(2025, 6, 19, 12, 53, 1, 43, DateTimeKind.Utc).AddTicks(152), "6db1d12e-cbf3-401d-93c8-0e434e3c085b" });

            migrationBuilder.UpdateData(
                table: "Boards",
                keyColumn: "Id",
                keyValue: new Guid("c5578431-7ae6-4ed9-a402-f1c3401c7100"),
                column: "CreatedAt",
                value: new DateTime(2025, 6, 19, 12, 53, 1, 43, DateTimeKind.Utc).AddTicks(8541));

            migrationBuilder.UpdateData(
                table: "Boards",
                keyColumn: "Id",
                keyValue: new Guid("f8385f75-481b-4b70-be0e-c975265e98ba"),
                column: "CreatedAt",
                value: new DateTime(2025, 6, 19, 12, 53, 1, 43, DateTimeKind.Utc).AddTicks(8547));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("6523ec54-87f8-4114-b42b-4e6cb75c802a"),
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 6, 19, 12, 53, 1, 44, DateTimeKind.Utc).AddTicks(4899), new DateTime(2025, 6, 19, 12, 53, 1, 44, DateTimeKind.Utc).AddTicks(4899) });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("71d465ed-bd31-4c2c-9700-e1274685ca5d"),
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 6, 19, 12, 53, 1, 44, DateTimeKind.Utc).AddTicks(4885), new DateTime(2025, 6, 19, 12, 53, 1, 44, DateTimeKind.Utc).AddTicks(4886) });

            migrationBuilder.UpdateData(
                table: "Replies",
                keyColumn: "Id",
                keyValue: new Guid("7bded954-6e81-4e44-a7e3-19234f568f0c"),
                column: "CreatedAt",
                value: new DateTime(2025, 6, 19, 12, 53, 1, 45, DateTimeKind.Utc).AddTicks(5250));

            migrationBuilder.UpdateData(
                table: "Replies",
                keyColumn: "Id",
                keyValue: new Guid("9669f2a1-b62d-4a18-8e49-3edabb18d418"),
                column: "CreatedAt",
                value: new DateTime(2025, 6, 19, 12, 53, 1, 45, DateTimeKind.Utc).AddTicks(5258));
        }
    }
}
