using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Study402Online.UserService.Api.Migrations
{
    /// <inheritdoc />
    public partial class wechatusertable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WechatUsers",
                columns: table => new
                {
                    LocalId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OpenId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnionId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WechatUsers", x => x.LocalId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WechatUsers");
        }
    }
}
