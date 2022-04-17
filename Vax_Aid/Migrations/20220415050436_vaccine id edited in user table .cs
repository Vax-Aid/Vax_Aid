using Microsoft.EntityFrameworkCore.Migrations;

namespace Vax_Aid.Migrations
{
    public partial class vaccineideditedinusertable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserDetails_VaccineInfos_VaccineID",
                table: "UserDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_UserDetails_VendorLocation_VendorID",
                table: "UserDetails");

            migrationBuilder.DropIndex(
                name: "IX_UserDetails_VaccineID",
                table: "UserDetails");

            migrationBuilder.DropIndex(
                name: "IX_UserDetails_VendorID",
                table: "UserDetails");

            migrationBuilder.DropColumn(
                name: "VaccineID",
                table: "UserDetails");

            migrationBuilder.DropColumn(
                name: "VendorID",
                table: "UserDetails");

            migrationBuilder.CreateIndex(
                name: "IX_UserDetails_VaccineInfoId",
                table: "UserDetails",
                column: "VaccineInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDetails_VendorLocationId",
                table: "UserDetails",
                column: "VendorLocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserDetails_VaccineInfos_VaccineInfoId",
                table: "UserDetails",
                column: "VaccineInfoId",
                principalTable: "VaccineInfos",
                principalColumn: "VaccineInfoId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_UserDetails_VendorLocation_VendorLocationId",
                table: "UserDetails",
                column: "VendorLocationId",
                principalTable: "VendorLocation",
                principalColumn: "VendorLocationId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserDetails_VaccineInfos_VaccineInfoId",
                table: "UserDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_UserDetails_VendorLocation_VendorLocationId",
                table: "UserDetails");

            migrationBuilder.DropIndex(
                name: "IX_UserDetails_VaccineInfoId",
                table: "UserDetails");

            migrationBuilder.DropIndex(
                name: "IX_UserDetails_VendorLocationId",
                table: "UserDetails");

            migrationBuilder.AddColumn<int>(
                name: "VaccineID",
                table: "UserDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VendorID",
                table: "UserDetails",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserDetails_VaccineID",
                table: "UserDetails",
                column: "VaccineID");

            migrationBuilder.CreateIndex(
                name: "IX_UserDetails_VendorID",
                table: "UserDetails",
                column: "VendorID");

            migrationBuilder.AddForeignKey(
                name: "FK_UserDetails_VaccineInfos_VaccineID",
                table: "UserDetails",
                column: "VaccineID",
                principalTable: "VaccineInfos",
                principalColumn: "VaccineInfoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserDetails_VendorLocation_VendorID",
                table: "UserDetails",
                column: "VendorID",
                principalTable: "VendorLocation",
                principalColumn: "VendorLocationId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
