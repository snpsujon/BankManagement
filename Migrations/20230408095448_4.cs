using Microsoft.EntityFrameworkCore.Migrations;

namespace BankManagement.Migrations
{
    public partial class _4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TransExtType",
                table: "transTbls",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransExtType",
                table: "transTbls");
        }
    }
}
