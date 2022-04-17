using Microsoft.EntityFrameworkCore.Migrations;

namespace Vax_Aid.Migrations
{
    public partial class ModelsDeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingDetails");

            migrationBuilder.DropTable(
                name: "Vaccinations");

            migrationBuilder.DropTable(
                name: "VendorDetails");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookingDetails",
                columns: table => new
                {
                    BookingDetailsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Conformation = table.Column<bool>(type: "bit", nullable: false),
                    Dose = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserDetailsId = table.Column<int>(type: "int", nullable: false),
                    VaccineInfoId = table.Column<int>(type: "int", nullable: false),
                    VendorLocationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingDetails", x => x.BookingDetailsId);
                    table.ForeignKey(
                        name: "FK_BookingDetails_UserDetails_UserDetailsId",
                        column: x => x.UserDetailsId,
                        principalTable: "UserDetails",
                        principalColumn: "UserDetailsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingDetails_VaccineInfos_VaccineInfoId",
                        column: x => x.VaccineInfoId,
                        principalTable: "VaccineInfos",
                        principalColumn: "VaccineInfoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingDetails_VendorLocation_VendorLocationId",
                        column: x => x.VendorLocationId,
                        principalTable: "VendorLocation",
                        principalColumn: "VendorLocationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vaccinations",
                columns: table => new
                {
                    VaccinationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserDetailsId = table.Column<int>(type: "int", nullable: false),
                    VaccineInfoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vaccinations", x => x.VaccinationId);
                    table.ForeignKey(
                        name: "FK_Vaccinations_UserDetails_UserDetailsId",
                        column: x => x.UserDetailsId,
                        principalTable: "UserDetails",
                        principalColumn: "UserDetailsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vaccinations_VaccineInfos_VaccineInfoId",
                        column: x => x.VaccineInfoId,
                        principalTable: "VaccineInfos",
                        principalColumn: "VaccineInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VendorDetails",
                columns: table => new
                {
                    VendorDetailsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VaccineAvailability = table.Column<bool>(type: "bit", nullable: false),
                    VaccineInfoId = table.Column<int>(type: "int", nullable: false),
                    VendorLocationId = table.Column<int>(type: "int", nullable: false),
                    VendorName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorDetails", x => x.VendorDetailsId);
                    table.ForeignKey(
                        name: "FK_VendorDetails_VaccineInfos_VaccineInfoId",
                        column: x => x.VaccineInfoId,
                        principalTable: "VaccineInfos",
                        principalColumn: "VaccineInfoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VendorDetails_VendorLocation_VendorLocationId",
                        column: x => x.VendorLocationId,
                        principalTable: "VendorLocation",
                        principalColumn: "VendorLocationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingDetails_UserDetailsId",
                table: "BookingDetails",
                column: "UserDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingDetails_VaccineInfoId",
                table: "BookingDetails",
                column: "VaccineInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingDetails_VendorLocationId",
                table: "BookingDetails",
                column: "VendorLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Vaccinations_UserDetailsId",
                table: "Vaccinations",
                column: "UserDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_Vaccinations_VaccineInfoId",
                table: "Vaccinations",
                column: "VaccineInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorDetails_VaccineInfoId",
                table: "VendorDetails",
                column: "VaccineInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorDetails_VendorLocationId",
                table: "VendorDetails",
                column: "VendorLocationId");
        }
    }
}
