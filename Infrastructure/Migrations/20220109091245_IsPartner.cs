using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class IsPartner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                schema: "Identity",
                table: "Users");

            migrationBuilder.AddColumn<bool>(
                name: "IsPartner",
                schema: "Identity",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPartner",
                schema: "Identity",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                schema: "Identity",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
