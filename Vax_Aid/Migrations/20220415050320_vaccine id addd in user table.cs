using Microsoft.EntityFrameworkCore.Migrations;

namespace Vax_Aid.Migrations
{
    public partial class vaccineidadddinusertable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserDetails_VaccineInfos_VaccineID",
                table: "UserDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_UserDetails_VendorLocation_VendorID",
                table: "UserDetails");

            migrationBuilder.AlterColumn<int>(
                name: "VendorID",
                table: "UserDetails",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "VaccineID",
                table: "UserDetails",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "VaccineInfoId",
                table: "UserDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VendorLocationId",
                table: "UserDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserDetails_VaccineInfos_VaccineID",
                table: "UserDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_UserDetails_VendorLocation_VendorID",
                table: "UserDetails");

            migrationBuilder.DropColumn(
                name: "VaccineInfoId",
                table: "UserDetails");

            migrationBuilder.DropColumn(
                name: "VendorLocationId",
                table: "UserDetails");

            migrationBuilder.AlterColumn<int>(
                name: "VendorID",
                table: "UserDetails",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "VaccineID",
                table: "UserDetails",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserDetails_VaccineInfos_VaccineID",
                table: "UserDetails",
                column: "VaccineID",
                principalTable: "VaccineInfos",
                principalColumn: "VaccineInfoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserDetails_VendorLocation_VendorID",
                table: "UserDetails",
                column: "VendorID",
                principalTable: "VendorLocation",
                principalColumn: "VendorLocationId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
