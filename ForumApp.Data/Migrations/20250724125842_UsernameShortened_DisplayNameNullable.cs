using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForumApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class UsernameShortened_DisplayNameNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DisplayName",
                table: "AspNetUsers",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: true,
                comment: "Name with which user is displayed in app",
                oldClrType: typeof(string),
                oldType: "nvarchar(80)",
                oldMaxLength: 80,
                oldComment: "Name with which user is displayed in app");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("7d926fd2-1b4e-4ea7-a019-2bcb179db8f9"),
                columns: new[] { "ConcurrencyStamp", "JoinDate", "SecurityStamp" },
                values: new object[] { "4db441a1-154c-4073-b4c0-0d36ed9fbd93", new DateTime(2025, 7, 24, 12, 58, 42, 227, DateTimeKind.Utc).AddTicks(3533), "88784ed5-4b3a-496a-be7e-46749c32b057" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e43bb3f7-884a-437b-9a0c-b0d181f07634"),
                columns: new[] { "ConcurrencyStamp", "JoinDate", "SecurityStamp" },
                values: new object[] { "08201b6c-d394-49f9-b41e-541221213201", new DateTime(2025, 7, 24, 12, 58, 42, 227, DateTimeKind.Utc).AddTicks(3557), "cc181ab5-ac81-4cf3-ae1f-ebea63e0261b" });

            migrationBuilder.UpdateData(
                table: "Boards",
                keyColumn: "Id",
                keyValue: new Guid("c5578431-7ae6-4ed9-a402-f1c3401c7100"),
                column: "CreatedAt",
                value: new DateTime(2025, 7, 24, 12, 58, 42, 228, DateTimeKind.Utc).AddTicks(586));

            migrationBuilder.UpdateData(
                table: "Boards",
                keyColumn: "Id",
                keyValue: new Guid("f8385f75-481b-4b70-be0e-c975265e98ba"),
                column: "CreatedAt",
                value: new DateTime(2025, 7, 24, 12, 58, 42, 228, DateTimeKind.Utc).AddTicks(593));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("6523ec54-87f8-4114-b42b-4e6cb75c802a"),
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 7, 24, 12, 58, 42, 229, DateTimeKind.Utc).AddTicks(1057), new DateTime(2025, 7, 24, 12, 58, 42, 229, DateTimeKind.Utc).AddTicks(1057) });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("71d465ed-bd31-4c2c-9700-e1274685ca5d"),
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 7, 24, 12, 58, 42, 229, DateTimeKind.Utc).AddTicks(1049), new DateTime(2025, 7, 24, 12, 58, 42, 229, DateTimeKind.Utc).AddTicks(1050) });

            migrationBuilder.UpdateData(
                table: "Replies",
                keyColumn: "Id",
                keyValue: new Guid("7bded954-6e81-4e44-a7e3-19234f568f0c"),
                column: "CreatedAt",
                value: new DateTime(2025, 7, 24, 12, 58, 42, 229, DateTimeKind.Utc).AddTicks(9207));

            migrationBuilder.UpdateData(
                table: "Replies",
                keyColumn: "Id",
                keyValue: new Guid("9669f2a1-b62d-4a18-8e49-3edabb18d418"),
                column: "CreatedAt",
                value: new DateTime(2025, 7, 24, 12, 58, 42, 229, DateTimeKind.Utc).AddTicks(9214));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DisplayName",
                table: "AspNetUsers",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: false,
                defaultValue: "",
                comment: "Name with which user is displayed in app",
                oldClrType: typeof(string),
                oldType: "nvarchar(80)",
                oldMaxLength: 80,
                oldNullable: true,
                oldComment: "Name with which user is displayed in app");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("7d926fd2-1b4e-4ea7-a019-2bcb179db8f9"),
                columns: new[] { "ConcurrencyStamp", "JoinDate", "SecurityStamp" },
                values: new object[] { "ceeb66e7-dd42-4b57-94f5-efab74194ff3", new DateTime(2025, 7, 10, 19, 23, 29, 227, DateTimeKind.Utc).AddTicks(5724), "41392ea8-d335-4ab3-a1a6-f3dc429c2fc3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e43bb3f7-884a-437b-9a0c-b0d181f07634"),
                columns: new[] { "ConcurrencyStamp", "JoinDate", "SecurityStamp" },
                values: new object[] { "7bc237bb-e504-4b67-9de8-a6904173a6e0", new DateTime(2025, 7, 10, 19, 23, 29, 227, DateTimeKind.Utc).AddTicks(5750), "db5108e4-85f4-4c21-8d0a-c802e43d5b2b" });

            migrationBuilder.UpdateData(
                table: "Boards",
                keyColumn: "Id",
                keyValue: new Guid("c5578431-7ae6-4ed9-a402-f1c3401c7100"),
                column: "CreatedAt",
                value: new DateTime(2025, 7, 10, 19, 23, 29, 228, DateTimeKind.Utc).AddTicks(5363));

            migrationBuilder.UpdateData(
                table: "Boards",
                keyColumn: "Id",
                keyValue: new Guid("f8385f75-481b-4b70-be0e-c975265e98ba"),
                column: "CreatedAt",
                value: new DateTime(2025, 7, 10, 19, 23, 29, 228, DateTimeKind.Utc).AddTicks(5369));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("6523ec54-87f8-4114-b42b-4e6cb75c802a"),
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 7, 10, 19, 23, 29, 229, DateTimeKind.Utc).AddTicks(6935), new DateTime(2025, 7, 10, 19, 23, 29, 229, DateTimeKind.Utc).AddTicks(6935) });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("71d465ed-bd31-4c2c-9700-e1274685ca5d"),
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 7, 10, 19, 23, 29, 229, DateTimeKind.Utc).AddTicks(6927), new DateTime(2025, 7, 10, 19, 23, 29, 229, DateTimeKind.Utc).AddTicks(6928) });

            migrationBuilder.UpdateData(
                table: "Replies",
                keyColumn: "Id",
                keyValue: new Guid("7bded954-6e81-4e44-a7e3-19234f568f0c"),
                column: "CreatedAt",
                value: new DateTime(2025, 7, 10, 19, 23, 29, 231, DateTimeKind.Utc).AddTicks(1585));

            migrationBuilder.UpdateData(
                table: "Replies",
                keyColumn: "Id",
                keyValue: new Guid("9669f2a1-b62d-4a18-8e49-3edabb18d418"),
                column: "CreatedAt",
                value: new DateTime(2025, 7, 10, 19, 23, 29, 231, DateTimeKind.Utc).AddTicks(1602));
        }
    }
}
