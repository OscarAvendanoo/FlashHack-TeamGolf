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
            migrationBuilder.DropForeignKey(
                name: "FK_SecondCategories_MainCategories_MainCategoryId",
                table: "SecondCategories");

            migrationBuilder.AlterColumn<int>(
                name: "MainCategoryId",
                table: "SecondCategories",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SecondCategories_MainCategories_MainCategoryId",
                table: "SecondCategories",
                column: "MainCategoryId",
                principalTable: "MainCategories",
                principalColumn: "MainCategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SecondCategories_MainCategories_MainCategoryId",
                table: "SecondCategories");

            migrationBuilder.AlterColumn<int>(
                name: "MainCategoryId",
                table: "SecondCategories",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_SecondCategories_MainCategories_MainCategoryId",
                table: "SecondCategories",
                column: "MainCategoryId",
                principalTable: "MainCategories",
                principalColumn: "MainCategoryId");
        }
    }
}
