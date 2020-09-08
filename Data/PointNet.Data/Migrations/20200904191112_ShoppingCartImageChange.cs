using Microsoft.EntityFrameworkCore.Migrations;

namespace PointNet.Data.Migrations
{
    public partial class ShoppingCartImageChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "ShoppingCartItems",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "ShoppingCartItems");
        }
    }
}
