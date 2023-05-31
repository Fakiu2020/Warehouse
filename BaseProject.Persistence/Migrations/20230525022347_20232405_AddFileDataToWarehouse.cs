using Microsoft.EntityFrameworkCore.Migrations;

namespace BaseProject.Persistence.Migrations
{
    public partial class _20232405_AddFileDataToWarehouse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileData",
                table: "Warehouse",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Warehouse",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MymeType",
                table: "Warehouse",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileData",
                table: "Warehouse");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Warehouse");

            migrationBuilder.DropColumn(
                name: "MymeType",
                table: "Warehouse");
        }
    }
}
