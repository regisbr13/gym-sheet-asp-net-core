using GymSheet.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymSheet.Data
{
    public class Context : DbContext
    {
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<MuscleGroup> MuscleGroups { get; set; }
        public DbSet<Excercise> Excercises { get; set; }
        public DbSet<ExcerciseList> ExcerciseLists { get; set; }
        public DbSet<Objective> Objectives { get; set; }
        public DbSet<Sheet> Sheets { get; set; }
        public DbSet<Teacher> Teachers { get; set; }

        public Context(DbContextOptions options) : base(options)
        {
        }
    }
}
