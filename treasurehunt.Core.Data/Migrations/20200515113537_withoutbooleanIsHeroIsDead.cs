using Microsoft.EntityFrameworkCore.Migrations;

namespace treasurehunt.Core.Data.Migrations
{
    public partial class withoutbooleanIsHeroIsDead : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDead",
                table: "Hero");

            migrationBuilder.DropColumn(
                name: "IsHero",
                table: "Hero");

            migrationBuilder.DropColumn(
                name: "IsDead",
                table: "Enemy");

            migrationBuilder.DropColumn(
                name: "IsHero",
                table: "Enemy");

            migrationBuilder.DropColumn(
                name: "IsDead",
                table: "Avatar");

            migrationBuilder.DropColumn(
                name: "IsHero",
                table: "Avatar");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDead",
                table: "Hero",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsHero",
                table: "Hero",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDead",
                table: "Enemy",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsHero",
                table: "Enemy",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDead",
                table: "Avatar",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsHero",
                table: "Avatar",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
