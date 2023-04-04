using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Study402Online.MediaService.Api.Migrations
{
    /// <inheritdoc />
    public partial class changemediaprocessmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileId",
                table: "MediaProcessHistories");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "MediaProcessHistories");

            migrationBuilder.RenameColumn(
                name: "StoragePath",
                table: "MediaProcessHistories",
                newName: "MediaFileName");

            migrationBuilder.AddColumn<int>(
                name: "FailureCount",
                table: "MediaProcessHistories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MediaFile",
                table: "MediaProcessHistories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FailureCount",
                table: "MediaProcesses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FailureCount",
                table: "MediaProcessHistories");

            migrationBuilder.DropColumn(
                name: "MediaFile",
                table: "MediaProcessHistories");

            migrationBuilder.DropColumn(
                name: "FailureCount",
                table: "MediaProcesses");

            migrationBuilder.RenameColumn(
                name: "MediaFileName",
                table: "MediaProcessHistories",
                newName: "StoragePath");

            migrationBuilder.AddColumn<string>(
                name: "FileId",
                table: "MediaProcessHistories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "MediaProcessHistories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
