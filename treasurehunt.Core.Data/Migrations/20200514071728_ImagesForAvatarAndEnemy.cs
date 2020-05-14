using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace treasurehunt.Core.Data.Migrations
{
    public partial class ImagesForAvatarAndEnemy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Choice_Question_QuestionId",
                table: "Choice");

            migrationBuilder.DropForeignKey(
                name: "FK_Choice_StoryEvent_StoryEventId",
                table: "Choice");

            migrationBuilder.DropForeignKey(
                name: "FK_Question_StoryEvent_StoryEventId",
                table: "Question");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StoryEvent",
                table: "StoryEvent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Question",
                table: "Question");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemOnGame",
                table: "ItemOnGame");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Choice",
                table: "Choice");

            migrationBuilder.RenameTable(
                name: "StoryEvent",
                newName: "StoryEvents");

            migrationBuilder.RenameTable(
                name: "Question",
                newName: "Questions");

            migrationBuilder.RenameTable(
                name: "ItemOnGame",
                newName: "ItemsOnGame");

            migrationBuilder.RenameTable(
                name: "Choice",
                newName: "Choices");

            migrationBuilder.RenameIndex(
                name: "IX_Question_StoryEventId",
                table: "Questions",
                newName: "IX_Questions_StoryEventId");

            migrationBuilder.RenameIndex(
                name: "IX_Choice_StoryEventId",
                table: "Choices",
                newName: "IX_Choices_StoryEventId");

            migrationBuilder.RenameIndex(
                name: "IX_Choice_QuestionId",
                table: "Choices",
                newName: "IX_Choices_QuestionId");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Choices",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_StoryEvents",
                table: "StoryEvents",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Questions",
                table: "Questions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemsOnGame",
                table: "ItemsOnGame",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Choices",
                table: "Choices",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageTitle = table.Column<string>(nullable: true),
                    ImageData = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AvatarImage",
                columns: table => new
                {
                    AvatarId = table.Column<int>(nullable: false),
                    ImageId = table.Column<int>(nullable: false),
                    AvatarId1 = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvatarImage", x => new { x.AvatarId, x.ImageId });
                    table.ForeignKey(
                        name: "FK_AvatarImage_Avatar_AvatarId1",
                        column: x => x.AvatarId1,
                        principalTable: "Avatar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AvatarImage_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EnemyImage",
                columns: table => new
                {
                    EnemyId = table.Column<int>(nullable: false),
                    ImageId = table.Column<int>(nullable: false),
                    Order = table.Column<byte>(nullable: false),
                    EnemyId1 = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnemyImage", x => new { x.EnemyId, x.ImageId });
                    table.ForeignKey(
                        name: "FK_EnemyImage_Enemy_EnemyId1",
                        column: x => x.EnemyId1,
                        principalTable: "Enemy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EnemyImage_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AvatarImage_AvatarId1",
                table: "AvatarImage",
                column: "AvatarId1");

            migrationBuilder.CreateIndex(
                name: "IX_AvatarImage_ImageId",
                table: "AvatarImage",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_EnemyImage_EnemyId1",
                table: "EnemyImage",
                column: "EnemyId1");

            migrationBuilder.CreateIndex(
                name: "IX_EnemyImage_ImageId",
                table: "EnemyImage",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Choices_Questions_QuestionId",
                table: "Choices",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Choices_StoryEvents_StoryEventId",
                table: "Choices",
                column: "StoryEventId",
                principalTable: "StoryEvents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_StoryEvents_StoryEventId",
                table: "Questions",
                column: "StoryEventId",
                principalTable: "StoryEvents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Choices_Questions_QuestionId",
                table: "Choices");

            migrationBuilder.DropForeignKey(
                name: "FK_Choices_StoryEvents_StoryEventId",
                table: "Choices");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_StoryEvents_StoryEventId",
                table: "Questions");

            migrationBuilder.DropTable(
                name: "AvatarImage");

            migrationBuilder.DropTable(
                name: "EnemyImage");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StoryEvents",
                table: "StoryEvents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Questions",
                table: "Questions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemsOnGame",
                table: "ItemsOnGame");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Choices",
                table: "Choices");

            migrationBuilder.RenameTable(
                name: "StoryEvents",
                newName: "StoryEvent");

            migrationBuilder.RenameTable(
                name: "Questions",
                newName: "Question");

            migrationBuilder.RenameTable(
                name: "ItemsOnGame",
                newName: "ItemOnGame");

            migrationBuilder.RenameTable(
                name: "Choices",
                newName: "Choice");

            migrationBuilder.RenameIndex(
                name: "IX_Questions_StoryEventId",
                table: "Question",
                newName: "IX_Question_StoryEventId");

            migrationBuilder.RenameIndex(
                name: "IX_Choices_StoryEventId",
                table: "Choice",
                newName: "IX_Choice_StoryEventId");

            migrationBuilder.RenameIndex(
                name: "IX_Choices_QuestionId",
                table: "Choice",
                newName: "IX_Choice_QuestionId");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Choice",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_StoryEvent",
                table: "StoryEvent",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Question",
                table: "Question",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemOnGame",
                table: "ItemOnGame",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Choice",
                table: "Choice",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Choice_Question_QuestionId",
                table: "Choice",
                column: "QuestionId",
                principalTable: "Question",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Choice_StoryEvent_StoryEventId",
                table: "Choice",
                column: "StoryEventId",
                principalTable: "StoryEvent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Question_StoryEvent_StoryEventId",
                table: "Question",
                column: "StoryEventId",
                principalTable: "StoryEvent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
