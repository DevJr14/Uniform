using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class CartHeaderIdName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartDetails_CartHeaders_CardHeaderId",
                schema: "shoppingCart",
                table: "CartDetails");

            migrationBuilder.RenameColumn(
                name: "CardHeaderId",
                schema: "shoppingCart",
                table: "CartDetails",
                newName: "CartHeaderId");

            migrationBuilder.RenameIndex(
                name: "IX_CartDetails_CardHeaderId",
                schema: "shoppingCart",
                table: "CartDetails",
                newName: "IX_CartDetails_CartHeaderId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartDetails_CartHeaders_CartHeaderId",
                schema: "shoppingCart",
                table: "CartDetails",
                column: "CartHeaderId",
                principalSchema: "shoppingCart",
                principalTable: "CartHeaders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartDetails_CartHeaders_CartHeaderId",
                schema: "shoppingCart",
                table: "CartDetails");

            migrationBuilder.RenameColumn(
                name: "CartHeaderId",
                schema: "shoppingCart",
                table: "CartDetails",
                newName: "CardHeaderId");

            migrationBuilder.RenameIndex(
                name: "IX_CartDetails_CartHeaderId",
                schema: "shoppingCart",
                table: "CartDetails",
                newName: "IX_CartDetails_CardHeaderId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartDetails_CartHeaders_CardHeaderId",
                schema: "shoppingCart",
                table: "CartDetails",
                column: "CardHeaderId",
                principalSchema: "shoppingCart",
                principalTable: "CartHeaders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
