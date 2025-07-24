using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForumApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class ApplicationUserDeletionCascadesIntoManager : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoardManagers_AspNetUsers_ApplicationUserId",
                table: "BoardManagers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("7d926fd2-1b4e-4ea7-a019-2bcb179db8f9"),
                columns: new[] { "ConcurrencyStamp", "JoinDate", "SecurityStamp" },
                values: new object[] { "fb67a70c-da0f-411a-90db-b211a480e825", new DateTime(2025, 7, 24, 14, 2, 7, 711, DateTimeKind.Utc).AddTicks(6906), "245150f4-5bb3-4d73-90f9-e12ba179f8da" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e43bb3f7-884a-437b-9a0c-b0d181f07634"),
                columns: new[] { "ConcurrencyStamp", "JoinDate", "SecurityStamp" },
                values: new object[] { "e354859d-9bb0-4a98-82e7-830462590f05", new DateTime(2025, 7, 24, 14, 2, 7, 711, DateTimeKind.Utc).AddTicks(6918), "f7921922-cf68-4edb-ad78-02b98d32fbff" });

            migrationBuilder.UpdateData(
                table: "Boards",
                keyColumn: "Id",
                keyValue: new Guid("c5578431-7ae6-4ed9-a402-f1c3401c7100"),
                column: "CreatedAt",
                value: new DateTime(2025, 7, 24, 14, 2, 7, 712, DateTimeKind.Utc).AddTicks(3854));

            migrationBuilder.UpdateData(
                table: "Boards",
                keyColumn: "Id",
                keyValue: new Guid("f8385f75-481b-4b70-be0e-c975265e98ba"),
                column: "CreatedAt",
                value: new DateTime(2025, 7, 24, 14, 2, 7, 712, DateTimeKind.Utc).AddTicks(3861));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("6523ec54-87f8-4114-b42b-4e6cb75c802a"),
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 7, 24, 14, 2, 7, 713, DateTimeKind.Utc).AddTicks(4005), new DateTime(2025, 7, 24, 14, 2, 7, 713, DateTimeKind.Utc).AddTicks(4005) });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("71d465ed-bd31-4c2c-9700-e1274685ca5d"),
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 7, 24, 14, 2, 7, 713, DateTimeKind.Utc).AddTicks(3997), new DateTime(2025, 7, 24, 14, 2, 7, 713, DateTimeKind.Utc).AddTicks(3997) });

            migrationBuilder.UpdateData(
                table: "Replies",
                keyColumn: "Id",
                keyValue: new Guid("7bded954-6e81-4e44-a7e3-19234f568f0c"),
                column: "CreatedAt",
                value: new DateTime(2025, 7, 24, 14, 2, 7, 714, DateTimeKind.Utc).AddTicks(1997));

            migrationBuilder.UpdateData(
                table: "Replies",
                keyColumn: "Id",
                keyValue: new Guid("9669f2a1-b62d-4a18-8e49-3edabb18d418"),
                column: "CreatedAt",
                value: new DateTime(2025, 7, 24, 14, 2, 7, 714, DateTimeKind.Utc).AddTicks(2003));

            migrationBuilder.AddForeignKey(
                name: "FK_BoardManagers_AspNetUsers_ApplicationUserId",
                table: "BoardManagers",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoardManagers_AspNetUsers_ApplicationUserId",
                table: "BoardManagers");

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

            migrationBuilder.AddForeignKey(
                name: "FK_BoardManagers_AspNetUsers_ApplicationUserId",
                table: "BoardManagers",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
