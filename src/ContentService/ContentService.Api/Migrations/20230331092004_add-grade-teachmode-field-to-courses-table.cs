using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Study402Online.ContentService.Api.Migrations
{
    /// <inheritdoc />
    public partial class addgradeteachmodefieldtocoursestable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Grade",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TeachMode",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Grade",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "TeachMode",
                table: "Courses");
        }
    }
}
