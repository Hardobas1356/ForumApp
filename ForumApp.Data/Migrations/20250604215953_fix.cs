using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForumApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoardCategory_Boards_BoardId",
                table: "BoardCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_BoardCategory_Category_CategoryId",
                table: "BoardCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_PostBoardTag_BoardTag_BoardTagId",
                table: "PostBoardTag");

            migrationBuilder.DropForeignKey(
                name: "FK_PostBoardTag_Posts_PostId",
                table: "PostBoardTag");

            migrationBuilder.DropForeignKey(
                name: "FK_Reply_Posts_PostId",
                table: "Reply");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reply",
                table: "Reply");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostBoardTag",
                table: "PostBoardTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BoardTag",
                table: "BoardTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BoardCategory",
                table: "BoardCategory");

            migrationBuilder.RenameTable(
                name: "Reply",
                newName: "Replies");

            migrationBuilder.RenameTable(
                name: "PostBoardTag",
                newName: "PostBoardTags");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "Categories");

            migrationBuilder.RenameTable(
                name: "BoardTag",
                newName: "BoardTags");

            migrationBuilder.RenameTable(
                name: "BoardCategory",
                newName: "BoardCategories");

            migrationBuilder.RenameIndex(
                name: "IX_Reply_PostId",
                table: "Replies",
                newName: "IX_Replies_PostId");

            migrationBuilder.RenameIndex(
                name: "IX_PostBoardTag_PostId",
                table: "PostBoardTags",
                newName: "IX_PostBoardTags_PostId");

            migrationBuilder.RenameIndex(
                name: "IX_BoardCategory_CategoryId",
                table: "BoardCategories",
                newName: "IX_BoardCategories_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Replies",
                table: "Replies",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostBoardTags",
                table: "PostBoardTags",
                columns: new[] { "BoardTagId", "PostId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BoardTags",
                table: "BoardTags",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BoardCategories",
                table: "BoardCategories",
                columns: new[] { "BoardId", "CategoryId" });

            migrationBuilder.AddForeignKey(
                name: "FK_BoardCategories_Boards_BoardId",
                table: "BoardCategories",
                column: "BoardId",
                principalTable: "Boards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BoardCategories_Categories_CategoryId",
                table: "BoardCategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostBoardTags_BoardTags_BoardTagId",
                table: "PostBoardTags",
                column: "BoardTagId",
                principalTable: "BoardTags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostBoardTags_Posts_PostId",
                table: "PostBoardTags",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Replies_Posts_PostId",
                table: "Replies",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoardCategories_Boards_BoardId",
                table: "BoardCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_BoardCategories_Categories_CategoryId",
                table: "BoardCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_PostBoardTags_BoardTags_BoardTagId",
                table: "PostBoardTags");

            migrationBuilder.DropForeignKey(
                name: "FK_PostBoardTags_Posts_PostId",
                table: "PostBoardTags");

            migrationBuilder.DropForeignKey(
                name: "FK_Replies_Posts_PostId",
                table: "Replies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Replies",
                table: "Replies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostBoardTags",
                table: "PostBoardTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BoardTags",
                table: "BoardTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BoardCategories",
                table: "BoardCategories");

            migrationBuilder.RenameTable(
                name: "Replies",
                newName: "Reply");

            migrationBuilder.RenameTable(
                name: "PostBoardTags",
                newName: "PostBoardTag");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Category");

            migrationBuilder.RenameTable(
                name: "BoardTags",
                newName: "BoardTag");

            migrationBuilder.RenameTable(
                name: "BoardCategories",
                newName: "BoardCategory");

            migrationBuilder.RenameIndex(
                name: "IX_Replies_PostId",
                table: "Reply",
                newName: "IX_Reply_PostId");

            migrationBuilder.RenameIndex(
                name: "IX_PostBoardTags_PostId",
                table: "PostBoardTag",
                newName: "IX_PostBoardTag_PostId");

            migrationBuilder.RenameIndex(
                name: "IX_BoardCategories_CategoryId",
                table: "BoardCategory",
                newName: "IX_BoardCategory_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reply",
                table: "Reply",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostBoardTag",
                table: "PostBoardTag",
                columns: new[] { "BoardTagId", "PostId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BoardTag",
                table: "BoardTag",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BoardCategory",
                table: "BoardCategory",
                columns: new[] { "BoardId", "CategoryId" });

            migrationBuilder.AddForeignKey(
                name: "FK_BoardCategory_Boards_BoardId",
                table: "BoardCategory",
                column: "BoardId",
                principalTable: "Boards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BoardCategory_Category_CategoryId",
                table: "BoardCategory",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostBoardTag_BoardTag_BoardTagId",
                table: "PostBoardTag",
                column: "BoardTagId",
                principalTable: "BoardTag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostBoardTag_Posts_PostId",
                table: "PostBoardTag",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reply_Posts_PostId",
                table: "Reply",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
