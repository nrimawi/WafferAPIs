using Microsoft.EntityFrameworkCore.Migrations;

namespace WafferAPIs.Migrations
{
    public partial class addFieldToSeller : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Logo",
                table: "Sellers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SocailMedaLink",
                table: "Sellers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WebsiteLink",
                table: "Sellers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Logo",
                table: "Sellers");

            migrationBuilder.DropColumn(
                name: "SocailMedaLink",
                table: "Sellers");

            migrationBuilder.DropColumn(
                name: "WebsiteLink",
                table: "Sellers");
        }
    }
}
