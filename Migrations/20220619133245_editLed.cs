using Microsoft.EntityFrameworkCore.Migrations;

namespace WafferAPIs.Migrations
{
    public partial class editLed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LEDLight",
                table: "Items",
                newName: "LedLight");

            migrationBuilder.RenameColumn(
                name: "HasLEDScreen",
                table: "Items",
                newName: "HasLedScreen");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LedLight",
                table: "Items",
                newName: "LEDLight");

            migrationBuilder.RenameColumn(
                name: "HasLedScreen",
                table: "Items",
                newName: "HasLEDScreen");
        }
    }
}
