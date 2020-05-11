using Microsoft.EntityFrameworkCore.Migrations;

namespace treasurehunt.Core.Data.Migrations
{
    public partial class stringForNamePersonnage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Personnages",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 20);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Name",
                table: "Personnages",
                type: "int",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 20);

            migrationBuilder.AddColumn<int>(
                name: "EvenementId",
                table: "Choix",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Choix_EvenementId",
                table: "Choix",
                column: "EvenementId");

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
