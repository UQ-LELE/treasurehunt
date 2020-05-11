using Microsoft.EntityFrameworkCore.Migrations;

namespace treasurehunt.Core.Data.Migrations
{
    public partial class stringForEventNumberInChoix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Choix_Evenement_EvenementId",
                table: "Choix");

            migrationBuilder.AlterColumn<int>(
                name: "EvenementId",
                table: "Choix",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "EventNumber",
                table: "Choix",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Choix_Evenement_EvenementId",
                table: "Choix",
                column: "EvenementId",
                principalTable: "Evenement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Choix_Evenement_EvenementId",
                table: "Choix");

            migrationBuilder.DropColumn(
                name: "EventNumber",
                table: "Choix");

            migrationBuilder.AlterColumn<int>(
                name: "EvenementId",
                table: "Choix",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Choix_Evenement_EvenementId",
                table: "Choix",
                column: "EvenementId",
                principalTable: "Evenement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
