using Microsoft.EntityFrameworkCore.Migrations;

namespace Vax_Aid.Migrations
{
    public partial class BookingDetailsUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingDetails_VendorLocation_LocationId",
                table: "BookingDetails");

            migrationBuilder.AlterColumn<int>(
                name: "LocationId",
                table: "BookingDetails",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "Dose",
                table: "BookingDetails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Municipality",
                table: "BookingDetails",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingDetails_VendorLocation_LocationId",
                table: "BookingDetails",
                column: "LocationId",
                principalTable: "VendorLocation",
                principalColumn: "LocationId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingDetails_VendorLocation_LocationId",
                table: "BookingDetails");

            migrationBuilder.DropColumn(
                name: "Dose",
                table: "BookingDetails");

            migrationBuilder.DropColumn(
                name: "Municipality",
                table: "BookingDetails");

            migrationBuilder.AlterColumn<int>(
                name: "LocationId",
                table: "BookingDetails",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingDetails_VendorLocation_LocationId",
                table: "BookingDetails",
                column: "LocationId",
                principalTable: "VendorLocation",
                principalColumn: "LocationId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
