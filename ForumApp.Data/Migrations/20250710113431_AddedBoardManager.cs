using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForumApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedBoardManager : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "Shows whether the user is deleted");

            migrationBuilder.CreateTable(
                name: "BoardManagers",
                columns: table => new
                {
                    BoardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id of Board to which the user is a manager. Part of key"),
                    ApplicationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id of user manager. Part of key"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "Shows whether this manager position is deleted")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardManagers", x => new { x.ApplicationUserId, x.BoardId });
                    table.ForeignKey(
                        name: "FK_BoardManagers_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BoardManagers_Boards_BoardId",
                        column: x => x.BoardId,
                        principalTable: "Boards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("7d926fd2-1b4e-4ea7-a019-2bcb179db8f9"),
                columns: new[] { "ConcurrencyStamp", "IsDeleted", "JoinDate", "SecurityStamp" },
                values: new object[] { "fb92898f-c319-4687-9593-2bf9eff00817", false, new DateTime(2025, 7, 10, 11, 34, 30, 295, DateTimeKind.Utc).AddTicks(7455), "e097a3b9-cb19-4216-8b6b-6c3b65e1a001" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e43bb3f7-884a-437b-9a0c-b0d181f07634"),
                columns: new[] { "ConcurrencyStamp", "IsDeleted", "JoinDate", "SecurityStamp" },
                values: new object[] { "17ee11ef-9270-4bf1-b48a-2f8679388551", false, new DateTime(2025, 7, 10, 11, 34, 30, 295, DateTimeKind.Utc).AddTicks(7469), "d683f315-769f-4919-8fac-1b87b1ac9381" });

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

            migrationBuilder.CreateIndex(
                name: "IX_BoardManagers_BoardId",
                table: "BoardManagers",
                column: "BoardId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoardManagers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AspNetUsers");

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
        }
    }
}
