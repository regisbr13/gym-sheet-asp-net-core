using Microsoft.EntityFrameworkCore.Migrations;

namespace GymSheet.Migrations
{
    public partial class thirty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alunos_Professores_TeacherId",
                table: "Alunos");

            migrationBuilder.AddForeignKey(
                name: "FK_Alunos_Professores_TeacherId",
                table: "Alunos",
                column: "TeacherId",
                principalTable: "Professores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alunos_Professores_TeacherId",
                table: "Alunos");

            migrationBuilder.AddForeignKey(
                name: "FK_Alunos_Professores_TeacherId",
                table: "Alunos",
                column: "TeacherId",
                principalTable: "Professores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
