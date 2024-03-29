﻿// <auto-generated />
using System;
using GymSheet.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GymSheet.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20190605190008_correcao")]
    partial class correcao
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GymSheet.Models.Administrator", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Administradores");
                });

            modelBuilder.Entity("GymSheet.Models.ExcerciseList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Charge");

                    b.Property<int>("ExcerciseId");

                    b.Property<int>("Frequency");

                    b.Property<int>("Repetitions");

                    b.Property<int>("SheetId");

                    b.HasKey("Id");

                    b.HasIndex("ExcerciseId");

                    b.HasIndex("SheetId");

                    b.ToTable("Listas_de_Exercícios");
                });

            modelBuilder.Entity("GymSheet.Models.Exercise", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MuscleGroupId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("MuscleGroupId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Exercícios");
                });

            modelBuilder.Entity("GymSheet.Models.MuscleGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Grupos_Musculares");
                });

            modelBuilder.Entity("GymSheet.Models.Objective", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Observation")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Objetivos");
                });

            modelBuilder.Entity("GymSheet.Models.Sheet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(15);

                    b.Property<DateTime>("Register");

                    b.Property<int>("StudentId");

                    b.Property<DateTime>("Validate");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("StudentId");

                    b.ToTable("Fichas");
                });

            modelBuilder.Entity("GymSheet.Models.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Age");

                    b.Property<int>("Height");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("ObjectiveId");

                    b.Property<int>("TeacherId");

                    b.Property<int>("WeeklyFrequency");

                    b.Property<double>("Weight");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("ObjectiveId");

                    b.HasIndex("TeacherId");

                    b.ToTable("Alunos");
                });

            modelBuilder.Entity("GymSheet.Models.Teacher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ImgPath")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Period")
                        .IsRequired()
                        .HasMaxLength(15);

                    b.Property<string>("Phone")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Professores");
                });

            modelBuilder.Entity("GymSheet.Models.ExcerciseList", b =>
                {
                    b.HasOne("GymSheet.Models.Exercise", "Excercise")
                        .WithMany("ExcerciseLists")
                        .HasForeignKey("ExcerciseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GymSheet.Models.Sheet", "Sheet")
                        .WithMany("ExcerciseLists")
                        .HasForeignKey("SheetId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GymSheet.Models.Exercise", b =>
                {
                    b.HasOne("GymSheet.Models.MuscleGroup", "MuscleGroup")
                        .WithMany("Excercises")
                        .HasForeignKey("MuscleGroupId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GymSheet.Models.Sheet", b =>
                {
                    b.HasOne("GymSheet.Models.Student", "Student")
                        .WithMany("Sheets")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GymSheet.Models.Student", b =>
                {
                    b.HasOne("GymSheet.Models.Objective", "Objective")
                        .WithMany("Students")
                        .HasForeignKey("ObjectiveId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GymSheet.Models.Teacher", "Teacher")
                        .WithMany("Students")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
