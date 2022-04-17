using Microsoft.EntityFrameworkCore.Migrations;

namespace Vax_Aid.Migrations
{
    public partial class flowstatusaddedinusertable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FlowStatus",
                table: "UserDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FlowStatus",
                table: "UserDetails");
        }
    }
}
