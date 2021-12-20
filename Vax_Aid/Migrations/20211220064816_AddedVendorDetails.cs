using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Vax_Aid.Migrations
{
    public partial class AddedVendorDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.RenameColumn(
            //    name: "UserId",
            //    table: "UserDetails",
            //    newName: "UserName");

            migrationBuilder.CreateTable(
                name: "VendorDetails",
                columns: table => new
                {
                    VendorDetailsId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VendorName = table.Column<string>(nullable: true),
                    VendorLocation = table.Column<string>(nullable: true),
                    VaccineAvailability = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorDetails", x => x.VendorDetailsId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VendorDetails");

            //migrationBuilder.RenameColumn(
            //    name: "UserName",
            //    table: "UserDetails",
            //    newName: "UserId");
        }
    }
}
