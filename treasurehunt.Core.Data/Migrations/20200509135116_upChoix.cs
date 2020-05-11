using Microsoft.EntityFrameworkCore.Migrations;

namespace treasurehunt.Core.Data.Migrations
{
    public partial class upChoix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Choix_Evenement_EvenementId",
                table: "Choix");

            migrationBuilder.AlterColumn<int>(
                name: "EvenementId",
                table: "Choix",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NexEventId",
                table: "Choix",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Choix_Evenement_EvenementId",
                table: "Choix",
                column: "EvenementId",
                principalTable: "Evenement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Choix_Evenement_EvenementId",
                table: "Choix");

            migrationBuilder.DropColumn(
                name: "NexEventId",
                table: "Choix");

            migrationBuilder.AlterColumn<int>(
                name: "EvenementId",
                table: "Choix",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Choix_Evenement_EvenementId",
                table: "Choix",
                column: "EvenementId",
                principalTable: "Evenement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
