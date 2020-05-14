using Microsoft.EntityFrameworkCore.Migrations;

namespace treasurehunt.Core.Data.Migrations
{
    public partial class deleteOrderInEnemyImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "EnemyImage");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "Order",
                table: "EnemyImage",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }
    }
}
