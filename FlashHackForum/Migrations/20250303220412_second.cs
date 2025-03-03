using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlashHackForum.Migrations
{
    /// <inheritdoc />
    public partial class second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ThreadPosts_ForumThreads_ForumThreadId",
                table: "ThreadPosts");

            migrationBuilder.AddForeignKey(
                name: "FK_ThreadPosts_ForumThreads_ForumThreadId",
                table: "ThreadPosts",
                column: "ForumThreadId",
                principalTable: "ForumThreads",
                principalColumn: "ForumThreadID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ThreadPosts_ForumThreads_ForumThreadId",
                table: "ThreadPosts");

            migrationBuilder.AddForeignKey(
                name: "FK_ThreadPosts_ForumThreads_ForumThreadId",
                table: "ThreadPosts",
                column: "ForumThreadId",
                principalTable: "ForumThreads",
                principalColumn: "ForumThreadID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
