using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForumApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedColorHexCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ColorHex",
                table: "Categories",
                type: "nchar(7)",
                fixedLength: true,
                maxLength: 7,
                nullable: false,
                defaultValue: "#FFFFFF",
                comment: "Hex color code for the category");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("7d926fd2-1b4e-4ea7-a019-2bcb179db8f9"),
                columns: new[] { "ConcurrencyStamp", "JoinDate", "SecurityStamp" },
                values: new object[] { "2d47730d-998c-466c-8aff-08ee46923e89", new DateTime(2025, 6, 25, 10, 17, 15, 963, DateTimeKind.Utc).AddTicks(7515), "906cba38-cab0-4b69-84da-47fb35da8ec4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e43bb3f7-884a-437b-9a0c-b0d181f07634"),
                columns: new[] { "ConcurrencyStamp", "JoinDate", "SecurityStamp" },
                values: new object[] { "107d804e-eee1-4a7e-9cad-669ad0db476a", new DateTime(2025, 6, 25, 10, 17, 15, 963, DateTimeKind.Utc).AddTicks(7526), "82e5587b-5f2b-4f29-a565-d0c07678c09e" });

            migrationBuilder.UpdateData(
                table: "Boards",
                keyColumn: "Id",
                keyValue: new Guid("c5578431-7ae6-4ed9-a402-f1c3401c7100"),
                column: "CreatedAt",
                value: new DateTime(2025, 6, 25, 10, 17, 15, 964, DateTimeKind.Utc).AddTicks(5775));

            migrationBuilder.UpdateData(
                table: "Boards",
                keyColumn: "Id",
                keyValue: new Guid("f8385f75-481b-4b70-be0e-c975265e98ba"),
                column: "CreatedAt",
                value: new DateTime(2025, 6, 25, 10, 17, 15, 964, DateTimeKind.Utc).AddTicks(5783));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("5fbd4e2e-a6f9-4d0f-ad91-fa2794d20317"),
                column: "ColorHex",
                value: "#28A745");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("60f51770-93bc-42b4-a27c-8a280abda112"),
                column: "ColorHex",
                value: "#33C1FF");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("67e8a9f8-29d7-444f-bd9b-86225ae41daf"),
                column: "ColorHex",
                value: "#FF5733");

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("6523ec54-87f8-4114-b42b-4e6cb75c802a"),
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 6, 25, 10, 17, 15, 965, DateTimeKind.Utc).AddTicks(1750), new DateTime(2025, 6, 25, 10, 17, 15, 965, DateTimeKind.Utc).AddTicks(1750) });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("71d465ed-bd31-4c2c-9700-e1274685ca5d"),
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 6, 25, 10, 17, 15, 965, DateTimeKind.Utc).AddTicks(1732), new DateTime(2025, 6, 25, 10, 17, 15, 965, DateTimeKind.Utc).AddTicks(1733) });

            migrationBuilder.UpdateData(
                table: "Replies",
                keyColumn: "Id",
                keyValue: new Guid("7bded954-6e81-4e44-a7e3-19234f568f0c"),
                column: "CreatedAt",
                value: new DateTime(2025, 6, 25, 10, 17, 15, 966, DateTimeKind.Utc).AddTicks(812));

            migrationBuilder.UpdateData(
                table: "Replies",
                keyColumn: "Id",
                keyValue: new Guid("9669f2a1-b62d-4a18-8e49-3edabb18d418"),
                column: "CreatedAt",
                value: new DateTime(2025, 6, 25, 10, 17, 15, 966, DateTimeKind.Utc).AddTicks(819));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ColorHex",
                table: "Categories");

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
                column: "CreatedAt",
                value: new DateTime(2025, 6, 25, 9, 36, 43, 469, DateTimeKind.Utc).AddTicks(8377));

            migrationBuilder.UpdateData(
                table: "Boards",
                keyColumn: "Id",
                keyValue: new Guid("f8385f75-481b-4b70-be0e-c975265e98ba"),
                column: "CreatedAt",
                value: new DateTime(2025, 6, 25, 9, 36, 43, 469, DateTimeKind.Utc).AddTicks(8384));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("6523ec54-87f8-4114-b42b-4e6cb75c802a"),
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 6, 25, 9, 36, 43, 470, DateTimeKind.Utc).AddTicks(3671), new DateTime(2025, 6, 25, 9, 36, 43, 470, DateTimeKind.Utc).AddTicks(3672) });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("71d465ed-bd31-4c2c-9700-e1274685ca5d"),
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 6, 25, 9, 36, 43, 470, DateTimeKind.Utc).AddTicks(3634), new DateTime(2025, 6, 25, 9, 36, 43, 470, DateTimeKind.Utc).AddTicks(3635) });

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
    }
}
