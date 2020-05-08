using Microsoft.EntityFrameworkCore.Migrations;

namespace treasurehunt.Core.Data.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Question",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LaQuestion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Quete",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titre = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quete", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Evenement",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numero = table.Column<int>(nullable: false),
                    Titre = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    EstInitial = table.Column<bool>(nullable: false),
                    LaQuestionID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evenement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Evenement_Question_LaQuestionID",
                        column: x => x.LaQuestionID,
                        principalTable: "Question",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Choix",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionId = table.Column<int>(nullable: false),
                    EvenementId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Choix", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Choix_Evenement_EvenementId",
                        column: x => x.EvenementId,
                        principalTable: "Evenement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Choix_Question_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Choix_EvenementId",
                table: "Choix",
                column: "EvenementId");

            migrationBuilder.CreateIndex(
                name: "IX_Choix_QuestionId",
                table: "Choix",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Evenement_LaQuestionID",
                table: "Evenement",
                column: "LaQuestionID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Choix");

            migrationBuilder.DropTable(
                name: "Quete");

            migrationBuilder.DropTable(
                name: "Evenement");

            migrationBuilder.DropTable(
                name: "Question");
        }
    }
}
