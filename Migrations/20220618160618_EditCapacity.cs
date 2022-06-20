using Microsoft.EntityFrameworkCore.Migrations;

namespace WafferAPIs.Migrations
{
    public partial class EditCapacity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Capacity",
                table: "Items",
                type: "float",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Capacity",
                table: "Items",
                type: "int",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);
        }
    }
}
