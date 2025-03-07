using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlashHackForum.Migrations
{
    /// <inheritdoc />
    public partial class askdfjasf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Anonymous",
                table: "ThreadPosts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Anonymous",
                table: "ThreadPosts");
        }
    }
}
