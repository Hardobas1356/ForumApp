using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForumApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedColorHexTag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ColorHex",
                table: "Tags",
                type: "nchar(7)",
                fixedLength: true,
                maxLength: 7,
                nullable: false,
                defaultValue: "",
                comment: "Hex color code for the tag");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("7d926fd2-1b4e-4ea7-a019-2bcb179db8f9"),
                columns: new[] { "ConcurrencyStamp", "JoinDate", "SecurityStamp" },
                values: new object[] { "cf6025f2-4da7-4767-9b64-7ef970f24f5c", new DateTime(2025, 6, 26, 8, 21, 6, 269, DateTimeKind.Utc).AddTicks(9896), "9485088e-ffe7-4490-9fe9-5336d4834237" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e43bb3f7-884a-437b-9a0c-b0d181f07634"),
                columns: new[] { "ConcurrencyStamp", "JoinDate", "SecurityStamp" },
                values: new object[] { "22050526-1372-487b-a23a-df180334b654", new DateTime(2025, 6, 26, 8, 21, 6, 269, DateTimeKind.Utc).AddTicks(9911), "b1bbc262-6dc4-45eb-aff8-9c5672a1fb54" });

            migrationBuilder.UpdateData(
                table: "Boards",
                keyColumn: "Id",
                keyValue: new Guid("c5578431-7ae6-4ed9-a402-f1c3401c7100"),
                column: "CreatedAt",
                value: new DateTime(2025, 6, 26, 8, 21, 6, 271, DateTimeKind.Utc).AddTicks(2878));

            migrationBuilder.UpdateData(
                table: "Boards",
                keyColumn: "Id",
                keyValue: new Guid("f8385f75-481b-4b70-be0e-c975265e98ba"),
                column: "CreatedAt",
                value: new DateTime(2025, 6, 26, 8, 21, 6, 271, DateTimeKind.Utc).AddTicks(2896));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("6523ec54-87f8-4114-b42b-4e6cb75c802a"),
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 6, 26, 8, 21, 6, 272, DateTimeKind.Utc).AddTicks(1738), new DateTime(2025, 6, 26, 8, 21, 6, 272, DateTimeKind.Utc).AddTicks(1738) });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("71d465ed-bd31-4c2c-9700-e1274685ca5d"),
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 6, 26, 8, 21, 6, 272, DateTimeKind.Utc).AddTicks(1725), new DateTime(2025, 6, 26, 8, 21, 6, 272, DateTimeKind.Utc).AddTicks(1726) });

            migrationBuilder.UpdateData(
                table: "Replies",
                keyColumn: "Id",
                keyValue: new Guid("7bded954-6e81-4e44-a7e3-19234f568f0c"),
                column: "CreatedAt",
                value: new DateTime(2025, 6, 26, 8, 21, 6, 273, DateTimeKind.Utc).AddTicks(4110));

            migrationBuilder.UpdateData(
                table: "Replies",
                keyColumn: "Id",
                keyValue: new Guid("9669f2a1-b62d-4a18-8e49-3edabb18d418"),
                column: "CreatedAt",
                value: new DateTime(2025, 6, 26, 8, 21, 6, 273, DateTimeKind.Utc).AddTicks(4119));

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("1c326eb8-947a-41e9-a3a9-03a630af7151"),
                column: "ColorHex",
                value: "#0000ff");

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("3b169889-2b30-47f5-81fc-4f68fb3369ba"),
                column: "ColorHex",
                value: "#00ff00");

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: new Guid("b53a915c-c138-4567-9718-d04f7080297d"),
                column: "ColorHex",
                value: "#ff0000");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ColorHex",
                table: "Tags");

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
    }
}
