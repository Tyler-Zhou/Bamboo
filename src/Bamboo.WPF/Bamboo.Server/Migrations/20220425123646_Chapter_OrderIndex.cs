using Microsoft.EntityFrameworkCore.Migrations;

namespace Bamboo.Server.Migrations
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Chapter_OrderIndex : Migration
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "iOrderIndex",
                schema: "dbo",
                table: "tb_Book_Chapter",
                type: "int",
                maxLength: 200,
                nullable: false,
                defaultValue: 0);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "iOrderIndex",
                schema: "dbo",
                table: "tb_Book_Chapter");
        }
    }
}
