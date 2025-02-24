using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlashHackForum.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Profile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.CompanyId);
                });

            migrationBuilder.CreateTable(
                name: "Educations",
                columns: table => new
                {
                    EducationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SchoolName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProgramName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LengthOfEducation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YearStarted = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearEnded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Educations", x => x.EducationId);
                });

            migrationBuilder.CreateTable(
                name: "MainCategories",
                columns: table => new
                {
                    MainCategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainCategories", x => x.MainCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ConfirmPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: true),
                    IsPremium = table.Column<bool>(type: "bit", nullable: true),
                    Biography = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Signature = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Employer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfileImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountRating = table.Column<int>(type: "int", nullable: true),
                    ShowAdvertisements = table.Column<bool>(type: "bit", nullable: true),
                    ShowContact = table.Column<bool>(type: "bit", nullable: true),
                    ShowToCompanies = table.Column<bool>(type: "bit", nullable: true),
                    AccountCreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "SecondCategories",
                columns: table => new
                {
                    SecondCategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MainCategoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecondCategories", x => x.SecondCategoryId);
                    table.ForeignKey(
                        name: "FK_SecondCategories_MainCategories_MainCategoryId",
                        column: x => x.MainCategoryId,
                        principalTable: "MainCategories",
                        principalColumn: "MainCategoryId");
                });

            migrationBuilder.CreateTable(
                name: "Competenses",
                columns: table => new
                {
                    CompetensId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EducationId = table.Column<int>(type: "int", nullable: true),
                    Grade = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountUserId = table.Column<int>(type: "int", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competenses", x => x.CompetensId);
                    table.ForeignKey(
                        name: "FK_Competenses_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "CompanyId");
                    table.ForeignKey(
                        name: "FK_Competenses_Educations_EducationId",
                        column: x => x.EducationId,
                        principalTable: "Educations",
                        principalColumn: "EducationId");
                    table.ForeignKey(
                        name: "FK_Competenses_Users_AccountUserId",
                        column: x => x.AccountUserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "ForumThreads",
                columns: table => new
                {
                    ForumThreadID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatorId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SecondCategoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForumThreads", x => x.ForumThreadID);
                    table.ForeignKey(
                        name: "FK_ForumThreads_SecondCategories_SecondCategoryId",
                        column: x => x.SecondCategoryId,
                        principalTable: "SecondCategories",
                        principalColumn: "SecondCategoryId");
                    table.ForeignKey(
                        name: "FK_ForumThreads_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountFavorites",
                columns: table => new
                {
                    AccountUserId = table.Column<int>(type: "int", nullable: false),
                    FavoritesForumThreadID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountFavorites", x => new { x.AccountUserId, x.FavoritesForumThreadID });
                    table.ForeignKey(
                        name: "FK_AccountFavorites_ForumThreads_FavoritesForumThreadID",
                        column: x => x.FavoritesForumThreadID,
                        principalTable: "ForumThreads",
                        principalColumn: "ForumThreadID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountFavorites_Users_AccountUserId",
                        column: x => x.AccountUserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "ThreadPosts",
                columns: table => new
                {
                    ThreadPostId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostCreatorId = table.Column<int>(type: "int", nullable: false),
                    PostMessage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ForumThreadID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThreadPosts", x => x.ThreadPostId);
                    table.ForeignKey(
                        name: "FK_ThreadPosts_ForumThreads_ForumThreadID",
                        column: x => x.ForumThreadID,
                        principalTable: "ForumThreads",
                        principalColumn: "ForumThreadID");
                    table.ForeignKey(
                        name: "FK_ThreadPosts_Users_PostCreatorId",
                        column: x => x.PostCreatorId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountFavorites_FavoritesForumThreadID",
                table: "AccountFavorites",
                column: "FavoritesForumThreadID");

            migrationBuilder.CreateIndex(
                name: "IX_Competenses_AccountUserId",
                table: "Competenses",
                column: "AccountUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Competenses_CompanyId",
                table: "Competenses",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Competenses_EducationId",
                table: "Competenses",
                column: "EducationId");

            migrationBuilder.CreateIndex(
                name: "IX_ForumThreads_CreatorId",
                table: "ForumThreads",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_ForumThreads_SecondCategoryId",
                table: "ForumThreads",
                column: "SecondCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SecondCategories_MainCategoryId",
                table: "SecondCategories",
                column: "MainCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ThreadPosts_ForumThreadID",
                table: "ThreadPosts",
                column: "ForumThreadID");

            migrationBuilder.CreateIndex(
                name: "IX_ThreadPosts_PostCreatorId",
                table: "ThreadPosts",
                column: "PostCreatorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountFavorites");

            migrationBuilder.DropTable(
                name: "Competenses");

            migrationBuilder.DropTable(
                name: "ThreadPosts");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Educations");

            migrationBuilder.DropTable(
                name: "ForumThreads");

            migrationBuilder.DropTable(
                name: "SecondCategories");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "MainCategories");
        }
    }
}
