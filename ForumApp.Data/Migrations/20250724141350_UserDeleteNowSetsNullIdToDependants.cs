using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForumApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class UserDeleteNowSetsNullIdToDependants : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_AspNetUsers_ApplicationUserId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Replies_AspNetUsers_ApplicationUserId",
                table: "Replies");

            migrationBuilder.AlterColumn<Guid>(
                name: "ApplicationUserId",
                table: "Replies",
                type: "uniqueidentifier",
                nullable: true,
                comment: "Id of user which posted this reply",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "Id of user which posted this reply");

            migrationBuilder.AlterColumn<Guid>(
                name: "ApplicationUserId",
                table: "Posts",
                type: "uniqueidentifier",
                nullable: true,
                comment: "Id of user which posted this post",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldComment: "Id of user which posted this post");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("7d926fd2-1b4e-4ea7-a019-2bcb179db8f9"),
                columns: new[] { "ConcurrencyStamp", "JoinDate", "SecurityStamp" },
                values: new object[] { "29967006-8855-400f-acae-0b4a0d88c10f", new DateTime(2025, 7, 24, 14, 13, 49, 545, DateTimeKind.Utc).AddTicks(4190), "44417fc7-a0cc-4043-a510-2f54e25cfeef" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e43bb3f7-884a-437b-9a0c-b0d181f07634"),
                columns: new[] { "ConcurrencyStamp", "JoinDate", "SecurityStamp" },
                values: new object[] { "27a25b66-c9af-456b-85de-1e84277d07fb", new DateTime(2025, 7, 24, 14, 13, 49, 545, DateTimeKind.Utc).AddTicks(4215), "66d6ddb2-d491-48e1-a740-b48b95dd14b1" });

            migrationBuilder.UpdateData(
                table: "Boards",
                keyColumn: "Id",
                keyValue: new Guid("c5578431-7ae6-4ed9-a402-f1c3401c7100"),
                column: "CreatedAt",
                value: new DateTime(2025, 7, 24, 14, 13, 49, 546, DateTimeKind.Utc).AddTicks(1164));

            migrationBuilder.UpdateData(
                table: "Boards",
                keyColumn: "Id",
                keyValue: new Guid("f8385f75-481b-4b70-be0e-c975265e98ba"),
                column: "CreatedAt",
                value: new DateTime(2025, 7, 24, 14, 13, 49, 546, DateTimeKind.Utc).AddTicks(1198));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("6523ec54-87f8-4114-b42b-4e6cb75c802a"),
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 7, 24, 14, 13, 49, 547, DateTimeKind.Utc).AddTicks(1162), new DateTime(2025, 7, 24, 14, 13, 49, 547, DateTimeKind.Utc).AddTicks(1162) });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: new Guid("71d465ed-bd31-4c2c-9700-e1274685ca5d"),
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2025, 7, 24, 14, 13, 49, 547, DateTimeKind.Utc).AddTicks(1153), new DateTime(2025, 7, 24, 14, 13, 49, 547, DateTimeKind.Utc).AddTicks(1153) });

            migrationBuilder.UpdateData(
                table: "Replies",
                keyColumn: "Id",
                keyValue: new Guid("7bded954-6e81-4e44-a7e3-19234f568f0c"),
                column: "CreatedAt",
                value: new DateTime(2025, 7, 24, 14, 13, 49, 547, DateTimeKind.Utc).AddTicks(9302));

            migrationBuilder.UpdateData(
                table: "Replies",
                keyColumn: "Id",
                keyValue: new Guid("9669f2a1-b62d-4a18-8e49-3edabb18d418"),
                column: "CreatedAt",
                value: new DateTime(2025, 7, 24, 14, 13, 49, 547, DateTimeKind.Utc).AddTicks(9309));

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_AspNetUsers_ApplicationUserId",
                table: "Posts",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Replies_AspNetUsers_ApplicationUserId",
                table: "Replies",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_AspNetUsers_ApplicationUserId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Replies_AspNetUsers_ApplicationUserId",
                table: "Replies");

            migrationBuilder.AlterColumn<Guid>(
                name: "ApplicationUserId",
                table: "Replies",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                comment: "Id of user which posted this reply",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true,
                oldComment: "Id of user which posted this reply");

            migrationBuilder.AlterColumn<Guid>(
                name: "ApplicationUserId",
                table: "Posts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                comment: "Id of user which posted this post",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true,
                oldComment: "Id of user which posted this post");

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
                name: "FK_Posts_AspNetUsers_ApplicationUserId",
                table: "Posts",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Replies_AspNetUsers_ApplicationUserId",
                table: "Replies",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
