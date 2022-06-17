using Microsoft.EntityFrameworkCore.Migrations;

namespace WafferAPIs.Migrations
{
    public partial class editItemFileds3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WaterFilteratiom",
                table: "Items",
                newName: "WaterFilteration");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WaterFilteration",
                table: "Items",
                newName: "WaterFilteratiom");
        }
    }
}
