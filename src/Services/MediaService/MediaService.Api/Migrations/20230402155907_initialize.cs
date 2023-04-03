using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Study402Online.MediaService.Api.Migrations
{
    /// <inheritdoc />
    public partial class initialize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MediaFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Organization = table.Column<int>(type: "int", nullable: false),
                    OrganizationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StoragePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccessPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Updater = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UploadTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuditStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuditOpinion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaFiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MediaProcesses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MediaFile = table.Column<int>(type: "int", nullable: false),
                    MediaFileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StorageBucket = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UploadTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompletionTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AccessPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FailureMessage = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaProcesses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MediaProcessHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StorageBucket = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UploadTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompletionTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AccessPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StoragePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FailureMessage = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaProcessHistories", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MediaFiles");

            migrationBuilder.DropTable(
                name: "MediaProcesses");

            migrationBuilder.DropTable(
                name: "MediaProcessHistories");
        }
    }
}
