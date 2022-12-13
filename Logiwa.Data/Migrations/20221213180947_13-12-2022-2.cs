using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Logiwa.Data.Migrations
{
    public partial class _131220222 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MinimumStockQuantity",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MinimumStockQuantity",
                table: "Categories");
        }
    }
}
