using Microsoft.EntityFrameworkCore.Migrations;

namespace GymSheet.Migrations
{
    public partial class third : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alunos_Objetivos_ObjectiveId",
                table: "Alunos");

            migrationBuilder.AddForeignKey(
                name: "FK_Alunos_Objetivos_ObjectiveId",
                table: "Alunos",
                column: "ObjectiveId",
                principalTable: "Objetivos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alunos_Objetivos_ObjectiveId",
                table: "Alunos");

            migrationBuilder.AddForeignKey(
                name: "FK_Alunos_Objetivos_ObjectiveId",
                table: "Alunos",
                column: "ObjectiveId",
                principalTable: "Objetivos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
