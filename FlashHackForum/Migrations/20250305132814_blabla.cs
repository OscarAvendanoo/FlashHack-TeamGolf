using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlashHackForum.Migrations
{
    /// <inheritdoc />
    public partial class blabla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ShowSignature",
                table: "ThreadPosts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShowSignature",
                table: "ThreadPosts");
        }
    }
}
