using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class PartnerId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PartnerId",
                schema: "promotion",
                table: "Discounts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Discounts_PartnerId",
                schema: "promotion",
                table: "Discounts",
                column: "PartnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Discounts_Partners_PartnerId",
                schema: "promotion",
                table: "Discounts",
                column: "PartnerId",
                principalTable: "Partners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Discounts_Partners_PartnerId",
                schema: "promotion",
                table: "Discounts");

            migrationBuilder.DropIndex(
                name: "IX_Discounts_PartnerId",
                schema: "promotion",
                table: "Discounts");

            migrationBuilder.DropColumn(
                name: "PartnerId",
                schema: "promotion",
                table: "Discounts");
        }
    }
}
