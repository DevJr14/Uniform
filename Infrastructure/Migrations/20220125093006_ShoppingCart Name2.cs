using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class ShoppingCartName2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CardDetails_CardHeaders_CardHeaderId",
                schema: "shoppingCard",
                table: "CardDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_CardDetails_Products_ProductId",
                schema: "shoppingCard",
                table: "CardDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CardHeaders",
                schema: "shoppingCard",
                table: "CardHeaders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CardDetails",
                schema: "shoppingCard",
                table: "CardDetails");

            migrationBuilder.EnsureSchema(
                name: "shoppingCart");

            migrationBuilder.RenameTable(
                name: "CardHeaders",
                schema: "shoppingCard",
                newName: "CartHeaders",
                newSchema: "shoppingCart");

            migrationBuilder.RenameTable(
                name: "CardDetails",
                schema: "shoppingCard",
                newName: "CartDetails",
                newSchema: "shoppingCart");

            migrationBuilder.RenameIndex(
                name: "IX_CardDetails_ProductId",
                schema: "shoppingCart",
                table: "CartDetails",
                newName: "IX_CartDetails_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_CardDetails_CardHeaderId",
                schema: "shoppingCart",
                table: "CartDetails",
                newName: "IX_CartDetails_CardHeaderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartHeaders",
                schema: "shoppingCart",
                table: "CartHeaders",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartDetails",
                schema: "shoppingCart",
                table: "CartDetails",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CartDetails_CartHeaders_CardHeaderId",
                schema: "shoppingCart",
                table: "CartDetails",
                column: "CardHeaderId",
                principalSchema: "shoppingCart",
                principalTable: "CartHeaders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CartDetails_Products_ProductId",
                schema: "shoppingCart",
                table: "CartDetails",
                column: "ProductId",
                principalSchema: "catalog",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartDetails_CartHeaders_CardHeaderId",
                schema: "shoppingCart",
                table: "CartDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_CartDetails_Products_ProductId",
                schema: "shoppingCart",
                table: "CartDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartHeaders",
                schema: "shoppingCart",
                table: "CartHeaders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartDetails",
                schema: "shoppingCart",
                table: "CartDetails");

            migrationBuilder.EnsureSchema(
                name: "shoppingCard");

            migrationBuilder.RenameTable(
                name: "CartHeaders",
                schema: "shoppingCart",
                newName: "CardHeaders",
                newSchema: "shoppingCard");

            migrationBuilder.RenameTable(
                name: "CartDetails",
                schema: "shoppingCart",
                newName: "CardDetails",
                newSchema: "shoppingCard");

            migrationBuilder.RenameIndex(
                name: "IX_CartDetails_ProductId",
                schema: "shoppingCard",
                table: "CardDetails",
                newName: "IX_CardDetails_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_CartDetails_CardHeaderId",
                schema: "shoppingCard",
                table: "CardDetails",
                newName: "IX_CardDetails_CardHeaderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CardHeaders",
                schema: "shoppingCard",
                table: "CardHeaders",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CardDetails",
                schema: "shoppingCard",
                table: "CardDetails",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CardDetails_CardHeaders_CardHeaderId",
                schema: "shoppingCard",
                table: "CardDetails",
                column: "CardHeaderId",
                principalSchema: "shoppingCard",
                principalTable: "CardHeaders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CardDetails_Products_ProductId",
                schema: "shoppingCard",
                table: "CardDetails",
                column: "ProductId",
                principalSchema: "catalog",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
