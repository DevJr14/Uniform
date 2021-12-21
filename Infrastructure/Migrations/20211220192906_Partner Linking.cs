using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class PartnerLinking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PartnerId",
                schema: "catalog",
                table: "Tags",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PartnerId",
                schema: "catalog",
                table: "Products",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PartnerId",
                schema: "catalog",
                table: "Categories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PartnerId",
                schema: "catalog",
                table: "Brands",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Tags_PartnerId",
                schema: "catalog",
                table: "Tags",
                column: "PartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_PartnerId",
                schema: "catalog",
                table: "Products",
                column: "PartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_PartnerId",
                schema: "catalog",
                table: "Categories",
                column: "PartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Brands_PartnerId",
                schema: "catalog",
                table: "Brands",
                column: "PartnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Brands_Partners_PartnerId",
                schema: "catalog",
                table: "Brands",
                column: "PartnerId",
                principalTable: "Partners",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Partners_PartnerId",
                schema: "catalog",
                table: "Categories",
                column: "PartnerId",
                principalTable: "Partners",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Partners_PartnerId",
                schema: "catalog",
                table: "Products",
                column: "PartnerId",
                principalTable: "Partners",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Partners_PartnerId",
                schema: "catalog",
                table: "Tags",
                column: "PartnerId",
                principalTable: "Partners",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brands_Partners_PartnerId",
                schema: "catalog",
                table: "Brands");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Partners_PartnerId",
                schema: "catalog",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Partners_PartnerId",
                schema: "catalog",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Partners_PartnerId",
                schema: "catalog",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_PartnerId",
                schema: "catalog",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Products_PartnerId",
                schema: "catalog",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Categories_PartnerId",
                schema: "catalog",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Brands_PartnerId",
                schema: "catalog",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "PartnerId",
                schema: "catalog",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "PartnerId",
                schema: "catalog",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PartnerId",
                schema: "catalog",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "PartnerId",
                schema: "catalog",
                table: "Brands");
        }
    }
}
