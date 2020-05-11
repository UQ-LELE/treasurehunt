using Microsoft.EntityFrameworkCore.Migrations;

namespace treasurehunt.Core.Data.Migrations
{
    public partial class upChoixQuestionEvenement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Choix_Question_QuestionId",
                table: "Choix");

            migrationBuilder.DropForeignKey(
                name: "FK_Evenement_Question_LaQuestionID",
                table: "Evenement");

            migrationBuilder.DropIndex(
                name: "IX_Evenement_LaQuestionID",
                table: "Evenement");

            migrationBuilder.DropIndex(
                name: "IX_Choix_QuestionId",
                table: "Choix");

            migrationBuilder.DropColumn(
                name: "LaQuestionID",
                table: "Evenement");

            migrationBuilder.DropColumn(
                name: "QuestionId",
                table: "Choix");

            migrationBuilder.AddColumn<bool>(
                name: "IsDead",
                table: "Personnages",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Numero",
                table: "Evenement",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "QuestionId",
                table: "Evenement",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "LeChoix",
                table: "Choix",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDead",
                table: "Personnages");

            migrationBuilder.DropColumn(
                name: "QuestionId",
                table: "Evenement");

            migrationBuilder.DropColumn(
                name: "LeChoix",
                table: "Choix");

            migrationBuilder.AlterColumn<int>(
                name: "Numero",
                table: "Evenement",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LaQuestionID",
                table: "Evenement",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QuestionId",
                table: "Choix",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Evenement_LaQuestionID",
                table: "Evenement",
                column: "LaQuestionID");

            migrationBuilder.CreateIndex(
                name: "IX_Choix_QuestionId",
                table: "Choix",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Choix_Question_QuestionId",
                table: "Choix",
                column: "QuestionId",
                principalTable: "Question",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Evenement_Question_LaQuestionID",
                table: "Evenement",
                column: "LaQuestionID",
                principalTable: "Question",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
