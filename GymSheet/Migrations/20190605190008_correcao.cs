using Microsoft.EntityFrameworkCore.Migrations;

namespace GymSheet.Migrations
{
    public partial class correcao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Turno",
                table: "Professores");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Turno",
                table: "Professores",
                maxLength: 10,
                nullable: false,
                defaultValue: "");
        }
    }
}
