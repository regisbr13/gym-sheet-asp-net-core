using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GymSheet.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Administradores",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Email = table.Column<string>(maxLength: 50, nullable: false),
                    Password = table.Column<string>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administradores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Grupos_Musculares",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grupos_Musculares", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Objetivos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Observation = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Objetivos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Professores",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    ImgPath = table.Column<string>(nullable: false),
                    Period = table.Column<string>(maxLength: 15, nullable: false),
                    Phone = table.Column<string>(nullable: false),
                    Turno = table.Column<string>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Exercícios",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    MuscleGroupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercícios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exercícios_Grupos_Musculares_MuscleGroupId",
                        column: x => x.MuscleGroupId,
                        principalTable: "Grupos_Musculares",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Alunos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Age = table.Column<int>(nullable: false),
                    Weight = table.Column<double>(nullable: false),
                    Height = table.Column<int>(nullable: false),
                    WeeklyFrequency = table.Column<int>(nullable: false),
                    TeacherId = table.Column<int>(nullable: false),
                    ObjectiveId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alunos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alunos_Objetivos_ObjectiveId",
                        column: x => x.ObjectiveId,
                        principalTable: "Objetivos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Alunos_Professores_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Professores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Fichas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 15, nullable: false),
                    Register = table.Column<DateTime>(nullable: false),
                    Validate = table.Column<DateTime>(nullable: false),
                    StudentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fichas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fichas_Alunos_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Alunos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Listas_de_Exercícios",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Frequency = table.Column<int>(nullable: false),
                    Repetitions = table.Column<int>(nullable: false),
                    Charge = table.Column<int>(nullable: false),
                    SheetId = table.Column<int>(nullable: false),
                    ExcerciseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Listas_de_Exercícios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Listas_de_Exercícios_Exercícios_ExcerciseId",
                        column: x => x.ExcerciseId,
                        principalTable: "Exercícios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Listas_de_Exercícios_Fichas_SheetId",
                        column: x => x.SheetId,
                        principalTable: "Fichas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Administradores_Email",
                table: "Administradores",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Alunos_Name",
                table: "Alunos",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Alunos_ObjectiveId",
                table: "Alunos",
                column: "ObjectiveId");

            migrationBuilder.CreateIndex(
                name: "IX_Alunos_TeacherId",
                table: "Alunos",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Exercícios_MuscleGroupId",
                table: "Exercícios",
                column: "MuscleGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Exercícios_Name",
                table: "Exercícios",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Fichas_Name",
                table: "Fichas",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Fichas_StudentId",
                table: "Fichas",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Grupos_Musculares_Name",
                table: "Grupos_Musculares",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Listas_de_Exercícios_ExcerciseId",
                table: "Listas_de_Exercícios",
                column: "ExcerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_Listas_de_Exercícios_SheetId",
                table: "Listas_de_Exercícios",
                column: "SheetId");

            migrationBuilder.CreateIndex(
                name: "IX_Objetivos_Name",
                table: "Objetivos",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Professores_Name",
                table: "Professores",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Administradores");

            migrationBuilder.DropTable(
                name: "Listas_de_Exercícios");

            migrationBuilder.DropTable(
                name: "Exercícios");

            migrationBuilder.DropTable(
                name: "Fichas");

            migrationBuilder.DropTable(
                name: "Grupos_Musculares");

            migrationBuilder.DropTable(
                name: "Alunos");

            migrationBuilder.DropTable(
                name: "Objetivos");

            migrationBuilder.DropTable(
                name: "Professores");
        }
    }
}
