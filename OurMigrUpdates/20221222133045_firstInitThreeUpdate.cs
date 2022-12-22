using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Webshop.OurMigrUpdates
{
    public partial class firstInitThreeUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderDetailId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderDetailId",
                table: "Orders");
        }
    }
}
