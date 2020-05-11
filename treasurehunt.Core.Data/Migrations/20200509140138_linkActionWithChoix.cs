using Microsoft.EntityFrameworkCore.Migrations;

namespace treasurehunt.Core.Data.Migrations
{
    public partial class linkActionWithChoix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LeChoix",
                table: "Choix");

            migrationBuilder.AddColumn<int>(
                name: "ActionChoixId",
                table: "Choix",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActionChoixId",
                table: "Choix");

            migrationBuilder.AddColumn<string>(
                name: "LeChoix",
                table: "Choix",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
