using Microsoft.EntityFrameworkCore.Migrations;

namespace WafferAPIs.Migrations
{
    public partial class editItemFileds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Resoultion",
                table: "Items",
                newName: "Resolution");

            migrationBuilder.RenameColumn(
                name: "RemoteControl",
                table: "Items",
                newName: "SupportMagicMotion");

            migrationBuilder.RenameColumn(
                name: "MotorWarrenty",
                table: "Items",
                newName: "MotorWarranty");

            migrationBuilder.RenameColumn(
                name: "HasSataliteReciver",
                table: "Items",
                newName: "InDoorIceDispsnser");

            migrationBuilder.RenameColumn(
                name: "HasMagicMotion",
                table: "Items",
                newName: "HasSatelliteReceiver");

            migrationBuilder.RenameColumn(
                name: "DoorOpenAlrm",
                table: "Items",
                newName: "HasRemoteControl");

            migrationBuilder.RenameColumn(
                name: "DoorIceDispsnser",
                table: "Items",
                newName: "HasAlarm");

            migrationBuilder.RenameColumn(
                name: "CabelLength",
                table: "Items",
                newName: "CableLength");

            migrationBuilder.RenameColumn(
                name: "BoxComponent",
                table: "Items",
                newName: "BoxComponents");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SupportMagicMotion",
                table: "Items",
                newName: "RemoteControl");

            migrationBuilder.RenameColumn(
                name: "Resolution",
                table: "Items",
                newName: "Resoultion");

            migrationBuilder.RenameColumn(
                name: "MotorWarranty",
                table: "Items",
                newName: "MotorWarrenty");

            migrationBuilder.RenameColumn(
                name: "InDoorIceDispsnser",
                table: "Items",
                newName: "HasSataliteReciver");

            migrationBuilder.RenameColumn(
                name: "HasSatelliteReceiver",
                table: "Items",
                newName: "HasMagicMotion");

            migrationBuilder.RenameColumn(
                name: "HasRemoteControl",
                table: "Items",
                newName: "DoorOpenAlrm");

            migrationBuilder.RenameColumn(
                name: "HasAlarm",
                table: "Items",
                newName: "DoorIceDispsnser");

            migrationBuilder.RenameColumn(
                name: "CableLength",
                table: "Items",
                newName: "CabelLength");

            migrationBuilder.RenameColumn(
                name: "BoxComponents",
                table: "Items",
                newName: "BoxComponent");
        }
    }
}
