using Microsoft.EntityFrameworkCore.Migrations;

namespace Vax_Aid.Migrations
{
    public partial class MappedVaccinesTypeChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "MappedVaccines",
                table: "VendorLocation",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MappedVaccines",
                table: "VendorLocation");

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
    }
}
