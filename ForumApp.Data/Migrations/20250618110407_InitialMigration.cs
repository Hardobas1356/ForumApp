using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ForumApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Boards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Board Id"),
                    Name = table.Column<string>(type: "nvarchar(130)", maxLength: 130, nullable: false, comment: "Name of board"),
                    Description = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: false, comment: "Short description of the board"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP", comment: "Board creation date"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "Represents whether the board is deleted or not")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id of category"),
                    Name = table.Column<string>(type: "nvarchar(130)", maxLength: 130, nullable: false, comment: "Name of category")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id of tag which can be used in posts on a board"),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false, comment: "Name of tag")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id of post"),
                    Title = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false, comment: "Title pf the post"),
                    Content = table.Column<string>(type: "nvarchar(max)", maxLength: 20000, nullable: false, comment: "Content of the post"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP", comment: "Date when the post was created in UTC time"),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP", comment: "Last date the post was modified in UTC time"),
                    IsPinned = table.Column<bool>(type: "bit", nullable: false, comment: "Shows whether the post is pinned moderator"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "Shows whether the post was deleted by moderator"),
                    BoardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id of board to which the post belongs to")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_Boards_BoardId",
                        column: x => x.BoardId,
                        principalTable: "Boards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BoardCategories",
                columns: table => new
                {
                    BoardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id of board"),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id of category to apply to board")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardCategories", x => new { x.BoardId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_BoardCategories_Boards_BoardId",
                        column: x => x.BoardId,
                        principalTable: "Boards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BoardCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostTags",
                columns: table => new
                {
                    PostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id of the post"),
                    TagId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id of the tag which applied to the post")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostTags", x => new { x.TagId, x.PostId });
                    table.ForeignKey(
                        name: "FK_PostTags_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Replies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id of reply"),
                    Content = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false, comment: "Comment of reply"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP", comment: "Date when the reply was created in UTC time"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "Shows whether the reply was deleted by moderator"),
                    PostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id of the post to which the reply belongs to")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Replies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Replies_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Boards",
                columns: new[] { "Id", "CreatedAt", "Description", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { new Guid("c5578431-7ae6-4ed9-a402-f1c3401c7100"), new DateTime(2025, 6, 18, 11, 4, 7, 106, DateTimeKind.Utc).AddTicks(7431), "Talk about anything here.", false, "General Discussion" },
                    { new Guid("f8385f75-481b-4b70-be0e-c975265e98ba"), new DateTime(2025, 6, 18, 11, 4, 7, 106, DateTimeKind.Utc).AddTicks(7440), "Get help with your tech problems.", false, "Tech Support" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("5fbd4e2e-a6f9-4d0f-ad91-fa2794d20317"), "News" },
                    { new Guid("60f51770-93bc-42b4-a27c-8a280abda112"), "Gaming" },
                    { new Guid("67e8a9f8-29d7-444f-bd9b-86225ae41daf"), "Technology" }
                });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("1c326eb8-947a-41e9-a3a9-03a630af7151"), "Discussion" },
                    { new Guid("3b169889-2b30-47f5-81fc-4f68fb3369ba"), "Announcement" },
                    { new Guid("b53a915c-c138-4567-9718-d04f7080297d"), "Hot" }
                });

            migrationBuilder.InsertData(
                table: "BoardCategories",
                columns: new[] { "BoardId", "CategoryId" },
                values: new object[,]
                {
                    { new Guid("c5578431-7ae6-4ed9-a402-f1c3401c7100"), new Guid("67e8a9f8-29d7-444f-bd9b-86225ae41daf") },
                    { new Guid("f8385f75-481b-4b70-be0e-c975265e98ba"), new Guid("67e8a9f8-29d7-444f-bd9b-86225ae41daf") }
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "BoardId", "Content", "CreatedAt", "IsDeleted", "IsPinned", "ModifiedAt", "Title" },
                values: new object[,]
                {
                    { new Guid("6523ec54-87f8-4114-b42b-4e6cb75c802a"), new Guid("f8385f75-481b-4b70-be0e-c975265e98ba"), "My laptop gets very hot when gaming. Any tips?", new DateTime(2025, 6, 18, 11, 4, 7, 107, DateTimeKind.Utc).AddTicks(3428), false, false, new DateTime(2025, 6, 18, 11, 4, 7, 107, DateTimeKind.Utc).AddTicks(3429), "Laptop overheating issue" },
                    { new Guid("71d465ed-bd31-4c2c-9700-e1274685ca5d"), new Guid("c5578431-7ae6-4ed9-a402-f1c3401c7100"), "We're glad to have you here.", new DateTime(2025, 6, 18, 11, 4, 7, 107, DateTimeKind.Utc).AddTicks(3421), false, true, new DateTime(2025, 6, 18, 11, 4, 7, 107, DateTimeKind.Utc).AddTicks(3421), "Welcome to the forums!" }
                });

            migrationBuilder.InsertData(
                table: "PostTags",
                columns: new[] { "PostId", "TagId" },
                values: new object[,]
                {
                    { new Guid("6523ec54-87f8-4114-b42b-4e6cb75c802a"), new Guid("1c326eb8-947a-41e9-a3a9-03a630af7151") },
                    { new Guid("71d465ed-bd31-4c2c-9700-e1274685ca5d"), new Guid("3b169889-2b30-47f5-81fc-4f68fb3369ba") }
                });

            migrationBuilder.InsertData(
                table: "Replies",
                columns: new[] { "Id", "Content", "CreatedAt", "PostId" },
                values: new object[,]
                {
                    { new Guid("7bded954-6e81-4e44-a7e3-19234f568f0c"), "Thanks! Happy to be here.", new DateTime(2025, 6, 18, 11, 4, 7, 108, DateTimeKind.Utc).AddTicks(1438), new Guid("71d465ed-bd31-4c2c-9700-e1274685ca5d") },
                    { new Guid("9669f2a1-b62d-4a18-8e49-3edabb18d418"), "Try cleaning the fan and applying new thermal paste.", new DateTime(2025, 6, 18, 11, 4, 7, 108, DateTimeKind.Utc).AddTicks(1449), new Guid("6523ec54-87f8-4114-b42b-4e6cb75c802a") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BoardCategories_CategoryId",
                table: "BoardCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Boards_Name",
                table: "Boards",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_BoardId",
                table: "Posts",
                column: "BoardId");

            migrationBuilder.CreateIndex(
                name: "IX_PostTags_PostId",
                table: "PostTags",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Replies_PostId",
                table: "Replies",
                column: "PostId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "BoardCategories");

            migrationBuilder.DropTable(
                name: "PostTags");

            migrationBuilder.DropTable(
                name: "Replies");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Boards");
        }
    }
}
