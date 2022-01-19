using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class DiscountPercentage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Discounts_DiscountId",
                schema: "catalog",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_DiscountId",
                schema: "catalog",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DiscountId",
                schema: "catalog",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Amount",
                schema: "promotion",
                table: "Discounts");

            migrationBuilder.AddColumn<int>(
                name: "Percentage",
                schema: "promotion",
                table: "Discounts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Percentage",
                schema: "promotion",
                table: "Discounts");

            migrationBuilder.AddColumn<Guid>(
                name: "DiscountId",
                schema: "catalog",
                table: "Products",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                schema: "promotion",
                table: "Discounts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Products_DiscountId",
                schema: "catalog",
                table: "Products",
                column: "DiscountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Discounts_DiscountId",
                schema: "catalog",
                table: "Products",
                column: "DiscountId",
                principalSchema: "promotion",
                principalTable: "Discounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
