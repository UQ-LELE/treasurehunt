using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace treasurehunt.Core.Data.Migrations
{
    public partial class OneToOneCharacterImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AvatarImage");

            migrationBuilder.DropTable(
                name: "EnemyImage");

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "Hero",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "Enemy",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "Avatar",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Hero");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Enemy");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Avatar");

            migrationBuilder.CreateTable(
                name: "AvatarImage",
                columns: table => new
                {
                    AvatarId = table.Column<int>(type: "int", nullable: false),
                    ImageId = table.Column<int>(type: "int", nullable: false),
                    AvatarId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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
                    EnemyId = table.Column<int>(type: "int", nullable: false),
                    ImageId = table.Column<int>(type: "int", nullable: false),
                    EnemyId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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
        }
    }
}
