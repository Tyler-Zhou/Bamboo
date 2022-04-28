using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bamboo.Server.Data.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "sys_User",
                schema: "dbo",
                columns: table => new
                {
                    iId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sAccount = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    sUserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    sPassword = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    tCreateDate = table.Column<DateTime>(type: "DateTime", nullable: false),
                    tUpdateDate = table.Column<DateTime>(type: "DateTime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_User", x => x.iId);
                });

            migrationBuilder.CreateTable(
                name: "tb_Book",
                schema: "dbo",
                columns: table => new
                {
                    iId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sKey = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    sName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    sLink = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    sAuthor = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    sTag = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    sIntroduction = table.Column<string>(type: "TEXT", nullable: false),
                    tStatus = table.Column<byte>(type: "TINYINT", nullable: false),
                    tCreateDate = table.Column<DateTime>(type: "DateTime", nullable: false),
                    tUpdateDate = table.Column<DateTime>(type: "DateTime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Book", x => x.iId);
                });

            migrationBuilder.CreateTable(
                name: "tb_Book_Chapter",
                schema: "dbo",
                columns: table => new
                {
                    iId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sBookKey = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    sKey = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    sName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    sContent = table.Column<string>(type: "TEXT", nullable: false),
                    sLink = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    iOrderIndex = table.Column<int>(type: "int", maxLength: 200, nullable: false),
                    tCreateDate = table.Column<DateTime>(type: "DateTime", nullable: false),
                    tUpdateDate = table.Column<DateTime>(type: "DateTime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Book_Chapter", x => x.iId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sys_User",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tb_Book",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tb_Book_Chapter",
                schema: "dbo");
        }
    }
}
