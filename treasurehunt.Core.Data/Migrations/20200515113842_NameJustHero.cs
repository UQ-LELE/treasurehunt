using Microsoft.EntityFrameworkCore.Migrations;

namespace treasurehunt.Core.Data.Migrations
{
    public partial class NameJustHero : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPoisoned",
                table: "Hero");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Enemy");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Avatar");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPoisoned",
                table: "Hero",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Enemy",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Avatar",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }
    }
}
