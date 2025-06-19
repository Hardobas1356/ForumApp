using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForumApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("7d926fd2-1b4e-4ea7-a019-2bcb179db8f9"),
                columns: new[] { "ConcurrencyStamp", "JoinDate", "SecurityStamp" },
                values: new object[] { "ff3a60e2-6fce-40ca-8ba4-a72c0d2df7c9", new DateTime(2025, 6, 19, 10, 57, 14, 145, DateTimeKind.Utc).AddTicks(8937), "f13a7afb-c6a6-4033-895d-0805ca0cef43" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e43bb3f7-884a-437b-9a0c-b0d181f07634"),
                columns: new[] { "ConcurrencyStamp", "JoinDate", "SecurityStamp" },
                values: new object[] { "3d12c855-effd-4827-bc5b-3ec1355c8765", new DateTime(2025, 6, 19, 10, 57, 14, 145, DateTimeKind.Utc).AddTicks(8960), "e890cdaa-ec70-457c-9678-9025d1931b90" });

            migrationBuilder.UpdateData(
                table: "Boards",
                keyColumn: "Id",
                keyValue: new Guid("c5578431-7ae6-4ed9-a402-f1c3401c7100"),
                column: "CreatedAt",
                value: new DateTime(2025, 6, 19, 10, 57, 14, 146, DateTimeKind.Utc).AddTicks(7200));

            migrationBuilder.UpdateData(
                table: "Boards",
                keyColumn: "Id",
                keyValue: new Guid("f8385f75-481b-4b70-be0e-c975265e98ba"),
                column: "CreatedAt",
                value: new DateTime(2025, 6, 19, 10, 57, 14, 146, DateTimeKind.Utc).AddTicks(7206));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("6523ec54-87f8-4114-b42b-4e6cb75c802a"),
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 6, 19, 10, 57, 14, 147, DateTimeKind.Utc).AddTicks(3569), new DateTime(2025, 6, 19, 10, 57, 14, 147, DateTimeKind.Utc).AddTicks(3570) });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("71d465ed-bd31-4c2c-9700-e1274685ca5d"),
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 6, 19, 10, 57, 14, 147, DateTimeKind.Utc).AddTicks(3561), new DateTime(2025, 6, 19, 10, 57, 14, 147, DateTimeKind.Utc).AddTicks(3562) });

            migrationBuilder.UpdateData(
                table: "Replies",
                keyColumn: "Id",
                keyValue: new Guid("7bded954-6e81-4e44-a7e3-19234f568f0c"),
                column: "CreatedAt",
                value: new DateTime(2025, 6, 19, 10, 57, 14, 148, DateTimeKind.Utc).AddTicks(4091));

            migrationBuilder.UpdateData(
                table: "Replies",
                keyColumn: "Id",
                keyValue: new Guid("9669f2a1-b62d-4a18-8e49-3edabb18d418"),
                column: "CreatedAt",
                value: new DateTime(2025, 6, 19, 10, 57, 14, 148, DateTimeKind.Utc).AddTicks(4097));
        }
    }
}
