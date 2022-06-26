using Microsoft.EntityFrameworkCore.Migrations;

namespace WafferAPIs.Migrations
{
    public partial class editFileds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SocailMedaLink",
                table: "Sellers",
                newName: "SocailMediaLink");

            migrationBuilder.RenameColumn(
                name: "HasScreenShring",
                table: "Items",
                newName: "HasScreenSharing");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SocailMediaLink",
                table: "Sellers",
                newName: "SocailMedaLink");

            migrationBuilder.RenameColumn(
                name: "HasScreenSharing",
                table: "Items",
                newName: "HasScreenShring");
        }
    }
}
