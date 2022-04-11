using Microsoft.EntityFrameworkCore.Migrations;

namespace Vax_Aid.Migrations
{
    public partial class VendorLocationEdited : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MappedVaccinesVaccineInfoId",
                table: "VendorLocation",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VendorLocation_MappedVaccinesVaccineInfoId",
                table: "VendorLocation",
                column: "MappedVaccinesVaccineInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_VendorLocation_VaccineInfos_MappedVaccinesVaccineInfoId",
                table: "VendorLocation",
                column: "MappedVaccinesVaccineInfoId",
                principalTable: "VaccineInfos",
                principalColumn: "VaccineInfoId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VendorLocation_VaccineInfos_MappedVaccinesVaccineInfoId",
                table: "VendorLocation");

            migrationBuilder.DropIndex(
                name: "IX_VendorLocation_MappedVaccinesVaccineInfoId",
                table: "VendorLocation");

            migrationBuilder.DropColumn(
                name: "MappedVaccinesVaccineInfoId",
                table: "VendorLocation");
        }
    }
}
