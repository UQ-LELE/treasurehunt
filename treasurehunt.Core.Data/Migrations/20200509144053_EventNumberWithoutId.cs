using Microsoft.EntityFrameworkCore.Migrations;

namespace treasurehunt.Core.Data.Migrations
{
    public partial class EventNumberWithoutId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NexEventId",
                table: "Choix");

            migrationBuilder.AddColumn<string>(
                name: "NexEventNumber",
                table: "Choix",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NexEventNumber",
                table: "Choix");

            migrationBuilder.AddColumn<int>(
                name: "NexEventId",
                table: "Choix",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
