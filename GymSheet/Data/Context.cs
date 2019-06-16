using GymSheet.Data.Maps;
using GymSheet.Models;
using Microsoft.EntityFrameworkCore;

namespace GymSheet.Data
{
    public class Context : DbContext
    {
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<MuscleGroup> MuscleGroups { get; set; }
        public DbSet<Exercise> Excercises { get; set; }
        public DbSet<ExerciseList> ExerciseLists { get; set; }
        public DbSet<Objective> Objectives { get; set; }
        public DbSet<Sheet> Sheets { get; set; }
        public DbSet<Teacher> Teachers { get; set; }

        public Context(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AdministratorMap());
            modelBuilder.ApplyConfiguration(new ExerciseListMap());
            modelBuilder.ApplyConfiguration(new ExerciseMap());
            modelBuilder.ApplyConfiguration(new MuscleGroupMap());
            modelBuilder.ApplyConfiguration(new ObjectiveMap());
            modelBuilder.ApplyConfiguration(new SheetMap());
            modelBuilder.ApplyConfiguration(new StudentMap());
            modelBuilder.ApplyConfiguration(new TeacherMap());
        }
    }
}
