using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlashHackForum.Migrations
{
    /// <inheritdoc />
    public partial class updated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReplyToPostId",
                table: "ThreadPosts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ThreadPosts_ReplyToPostId",
                table: "ThreadPosts",
                column: "ReplyToPostId");

            migrationBuilder.AddForeignKey(
                name: "FK_ThreadPosts_ThreadPosts_ReplyToPostId",
                table: "ThreadPosts",
                column: "ReplyToPostId",
                principalTable: "ThreadPosts",
                principalColumn: "ThreadPostId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ThreadPosts_ThreadPosts_ReplyToPostId",
                table: "ThreadPosts");

            migrationBuilder.DropIndex(
                name: "IX_ThreadPosts_ReplyToPostId",
                table: "ThreadPosts");

            migrationBuilder.DropColumn(
                name: "ReplyToPostId",
                table: "ThreadPosts");
        }
    }
}
