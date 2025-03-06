using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlashHackForum.Migrations
{
    /// <inheritdoc />
    public partial class asdfas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsThreadDiscussionStarter",
                table: "ThreadPosts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsThreadDiscussionStarter",
                table: "ThreadPosts");
        }
    }
}
