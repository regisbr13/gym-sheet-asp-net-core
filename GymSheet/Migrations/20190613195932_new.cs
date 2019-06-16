using Microsoft.EntityFrameworkCore.Migrations;

namespace GymSheet.Migrations
{
    public partial class @new : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Listas_de_Exercícios_Exercícios_ExcerciseId",
                table: "Listas_de_Exercícios");

            migrationBuilder.RenameColumn(
                name: "ExcerciseId",
                table: "Listas_de_Exercícios",
                newName: "ExerciseId");

            migrationBuilder.RenameIndex(
                name: "IX_Listas_de_Exercícios_ExcerciseId",
                table: "Listas_de_Exercícios",
                newName: "IX_Listas_de_Exercícios_ExerciseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Listas_de_Exercícios_Exercícios_ExerciseId",
                table: "Listas_de_Exercícios",
                column: "ExerciseId",
                principalTable: "Exercícios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Listas_de_Exercícios_Exercícios_ExerciseId",
                table: "Listas_de_Exercícios");

            migrationBuilder.RenameColumn(
                name: "ExerciseId",
                table: "Listas_de_Exercícios",
                newName: "ExcerciseId");

            migrationBuilder.RenameIndex(
                name: "IX_Listas_de_Exercícios_ExerciseId",
                table: "Listas_de_Exercícios",
                newName: "IX_Listas_de_Exercícios_ExcerciseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Listas_de_Exercícios_Exercícios_ExcerciseId",
                table: "Listas_de_Exercícios",
                column: "ExcerciseId",
                principalTable: "Exercícios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
