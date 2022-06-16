using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WafferAPIs.Migrations
{
    public partial class addItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_Categories_CategoryId",
                table: "Item");

            migrationBuilder.DropForeignKey(
                name: "FK_Item_Sellers_SellerId",
                table: "Item");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Item",
                table: "Item");

            migrationBuilder.RenameTable(
                name: "Item",
                newName: "Items");

            migrationBuilder.RenameIndex(
                name: "IX_Item_SellerId",
                table: "Items",
                newName: "IX_Items_SellerId");

            migrationBuilder.RenameIndex(
                name: "IX_Item_CategoryId",
                table: "Items",
                newName: "IX_Items_CategoryId");

            migrationBuilder.AlterColumn<string>(
                name: "PhotoLink",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ModelNumber",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Dimensions",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Brand",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "BatterryInfo",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BoxComponent",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BrushesNumber",
                table: "Items",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CabelLength",
                table: "Items",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cameras",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Capacity",
                table: "Items",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ChildLock",
                table: "Items",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ConnectionToWifi",
                table: "Items",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ControlType",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DispalyType",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DoorIceDispsnser",
                table: "Items",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DoorNumbers",
                table: "Items",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DoorOpenAlrm",
                table: "Items",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnergyGrade",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FreezerCapacity",
                table: "Items",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "FreezerInclude",
                table: "Items",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Functions",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasDryerFunction",
                table: "Items",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasLEDScreen",
                table: "Items",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasMagicMotion",
                table: "Items",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasQuickWashFunction",
                table: "Items",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasSataliteReciver",
                table: "Items",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasScreenShring",
                table: "Items",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HasSteamFunctions",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HeatOptions",
                table: "Items",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "InDoorWaterDispsnser",
                table: "Items",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsSmart",
                table: "Items",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "LEDLight",
                table: "Items",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaxSpanSpeed",
                table: "Items",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "MobileConnection",
                table: "Items",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Momory",
                table: "Items",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MotorType",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MotorWarrenty",
                table: "Items",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NumberOfPrograms",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Power",
                table: "Items",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "RemoteControl",
                table: "Items",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Resoultion",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SIM",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ScreenSize",
                table: "Items",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ScreenWarrenty",
                table: "Items",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SpeedOptions",
                table: "Items",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Items",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "SubCategoryId",
                table: "Items",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "WaterFilteratiom",
                table: "Items",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "WorkOnBattery",
                table: "Items",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "WorkOnChargerCable",
                table: "Items",
                type: "bit",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Items",
                table: "Items",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Items_SubCategoryId",
                table: "Items",
                column: "SubCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Categories_CategoryId",
                table: "Items",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Sellers_SellerId",
                table: "Items",
                column: "SellerId",
                principalTable: "Sellers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_SubCategories_SubCategoryId",
                table: "Items",
                column: "SubCategoryId",
                principalTable: "SubCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Categories_CategoryId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Sellers_SellerId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_SubCategories_SubCategoryId",
                table: "Items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Items",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_SubCategoryId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "BatterryInfo",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "BoxComponent",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "BrushesNumber",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "CabelLength",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Cameras",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ChildLock",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ConnectionToWifi",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ControlType",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "DispalyType",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "DoorIceDispsnser",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "DoorNumbers",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "DoorOpenAlrm",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "EnergyGrade",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "FreezerCapacity",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "FreezerInclude",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Functions",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "HasDryerFunction",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "HasLEDScreen",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "HasMagicMotion",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "HasQuickWashFunction",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "HasSataliteReciver",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "HasScreenShring",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "HasSteamFunctions",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "HeatOptions",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "InDoorWaterDispsnser",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "IsSmart",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "LEDLight",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "MaxSpanSpeed",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "MobileConnection",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Momory",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "MotorType",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "MotorWarrenty",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "NumberOfPrograms",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Power",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "RemoteControl",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Resoultion",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "SIM",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ScreenSize",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ScreenWarrenty",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "SpeedOptions",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "SubCategoryId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "WaterFilteratiom",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "WorkOnBattery",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "WorkOnChargerCable",
                table: "Items");

            migrationBuilder.RenameTable(
                name: "Items",
                newName: "Item");

            migrationBuilder.RenameIndex(
                name: "IX_Items_SellerId",
                table: "Item",
                newName: "IX_Item_SellerId");

            migrationBuilder.RenameIndex(
                name: "IX_Items_CategoryId",
                table: "Item",
                newName: "IX_Item_CategoryId");

            migrationBuilder.AlterColumn<string>(
                name: "PhotoLink",
                table: "Item",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Item",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModelNumber",
                table: "Item",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Dimensions",
                table: "Item",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "Item",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Brand",
                table: "Item",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Item",
                table: "Item",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Categories_CategoryId",
                table: "Item",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Sellers_SellerId",
                table: "Item",
                column: "SellerId",
                principalTable: "Sellers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
