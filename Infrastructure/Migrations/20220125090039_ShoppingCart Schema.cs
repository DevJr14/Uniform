using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class ShoppingCartSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "shoppingCard");

            migrationBuilder.RenameTable(
                name: "CardHeaders",
                newName: "CardHeaders",
                newSchema: "shoppingCard");

            migrationBuilder.RenameTable(
                name: "CardDetails",
                newName: "CardDetails",
                newSchema: "shoppingCard");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "CardHeaders",
                schema: "shoppingCard",
                newName: "CardHeaders");

            migrationBuilder.RenameTable(
                name: "CardDetails",
                schema: "shoppingCard",
                newName: "CardDetails");
        }
    }
}
