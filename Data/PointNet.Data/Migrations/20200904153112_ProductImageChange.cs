using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PointNet.Data.Migrations
{
    public partial class ProductImageChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageFile",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "Products",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "Products");

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageFile",
                table: "Products",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
