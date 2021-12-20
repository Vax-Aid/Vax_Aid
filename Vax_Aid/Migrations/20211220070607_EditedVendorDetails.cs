using Microsoft.EntityFrameworkCore.Migrations;

namespace Vax_Aid.Migrations
{
    public partial class EditedVendorDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VaccineId",
                table: "VendorDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_VendorDetails_VaccineId",
                table: "VendorDetails",
                column: "VaccineId");

            migrationBuilder.AddForeignKey(
                name: "FK_VendorDetails_VaccineInfos_VaccineId",
                table: "VendorDetails",
                column: "VaccineId",
                principalTable: "VaccineInfos",
                principalColumn: "VaccineId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VendorDetails_VaccineInfos_VaccineId",
                table: "VendorDetails");

            migrationBuilder.DropIndex(
                name: "IX_VendorDetails_VaccineId",
                table: "VendorDetails");

            migrationBuilder.DropColumn(
                name: "VaccineId",
                table: "VendorDetails");
        }
    }
}
