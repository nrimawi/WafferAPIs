using Microsoft.EntityFrameworkCore.Migrations;

namespace WafferAPIs.Migrations
{
    public partial class editSellerAuth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sellers_AspNetUsers_ApplicationUserId1",
                table: "Sellers");

            migrationBuilder.DropIndex(
                name: "IX_Sellers_ApplicationUserId1",
                table: "Sellers");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId1",
                table: "Sellers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId1",
                table: "Sellers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sellers_ApplicationUserId1",
                table: "Sellers",
                column: "ApplicationUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Sellers_AspNetUsers_ApplicationUserId1",
                table: "Sellers",
                column: "ApplicationUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
