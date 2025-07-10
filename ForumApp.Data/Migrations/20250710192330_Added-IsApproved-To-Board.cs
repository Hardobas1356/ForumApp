using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForumApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedIsApprovedToBoard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Boards",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "Represents whether the board is approved to be created by admin");

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
                columns: new[] { "CreatedAt", "IsApproved" },
                values: new object[] { new DateTime(2025, 7, 10, 19, 23, 29, 228, DateTimeKind.Utc).AddTicks(5363), true });

            migrationBuilder.UpdateData(
                table: "Boards",
                keyColumn: "Id",
                keyValue: new Guid("f8385f75-481b-4b70-be0e-c975265e98ba"),
                columns: new[] { "CreatedAt", "IsApproved" },
                values: new object[] { new DateTime(2025, 7, 10, 19, 23, 29, 228, DateTimeKind.Utc).AddTicks(5369), true });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Boards");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("7d926fd2-1b4e-4ea7-a019-2bcb179db8f9"),
                columns: new[] { "ConcurrencyStamp", "JoinDate", "SecurityStamp" },
                values: new object[] { "fb92898f-c319-4687-9593-2bf9eff00817", new DateTime(2025, 7, 10, 11, 34, 30, 295, DateTimeKind.Utc).AddTicks(7455), "e097a3b9-cb19-4216-8b6b-6c3b65e1a001" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e43bb3f7-884a-437b-9a0c-b0d181f07634"),
                columns: new[] { "ConcurrencyStamp", "JoinDate", "SecurityStamp" },
                values: new object[] { "17ee11ef-9270-4bf1-b48a-2f8679388551", new DateTime(2025, 7, 10, 11, 34, 30, 295, DateTimeKind.Utc).AddTicks(7469), "d683f315-769f-4919-8fac-1b87b1ac9381" });

            migrationBuilder.UpdateData(
                table: "Boards",
                keyColumn: "Id",
                keyValue: new Guid("c5578431-7ae6-4ed9-a402-f1c3401c7100"),
                column: "CreatedAt",
                value: new DateTime(2025, 7, 10, 11, 34, 30, 296, DateTimeKind.Utc).AddTicks(4388));

            migrationBuilder.UpdateData(
                table: "Boards",
                keyColumn: "Id",
                keyValue: new Guid("f8385f75-481b-4b70-be0e-c975265e98ba"),
                column: "CreatedAt",
                value: new DateTime(2025, 7, 10, 11, 34, 30, 296, DateTimeKind.Utc).AddTicks(4394));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("6523ec54-87f8-4114-b42b-4e6cb75c802a"),
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 7, 10, 11, 34, 30, 297, DateTimeKind.Utc).AddTicks(4402), new DateTime(2025, 7, 10, 11, 34, 30, 297, DateTimeKind.Utc).AddTicks(4402) });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("71d465ed-bd31-4c2c-9700-e1274685ca5d"),
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 7, 10, 11, 34, 30, 297, DateTimeKind.Utc).AddTicks(4393), new DateTime(2025, 7, 10, 11, 34, 30, 297, DateTimeKind.Utc).AddTicks(4394) });

            migrationBuilder.UpdateData(
                table: "Replies",
                keyColumn: "Id",
                keyValue: new Guid("7bded954-6e81-4e44-a7e3-19234f568f0c"),
                column: "CreatedAt",
                value: new DateTime(2025, 7, 10, 11, 34, 30, 298, DateTimeKind.Utc).AddTicks(2583));

            migrationBuilder.UpdateData(
                table: "Replies",
                keyColumn: "Id",
                keyValue: new Guid("9669f2a1-b62d-4a18-8e49-3edabb18d418"),
                column: "CreatedAt",
                value: new DateTime(2025, 7, 10, 11, 34, 30, 298, DateTimeKind.Utc).AddTicks(2589));
        }
    }
}
