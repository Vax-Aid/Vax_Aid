using Microsoft.EntityFrameworkCore.Migrations;

namespace Vax_Aid.Migrations
{
    public partial class useridaddedinvendorlocationtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "VendorLocation",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserID",
                table: "VendorLocation");
        }
    }
}
