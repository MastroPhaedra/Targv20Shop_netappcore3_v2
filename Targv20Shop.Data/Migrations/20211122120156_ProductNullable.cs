using Microsoft.EntityFrameworkCore.Migrations;

namespace Targv20Shop.Data.Migrations
{
    public partial class ProductNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Product",
                newName: "Amount");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Product",
                newName: "Amount");
        }
    }
}
