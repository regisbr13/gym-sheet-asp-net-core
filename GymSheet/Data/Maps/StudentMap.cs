using GymSheet.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymSheet.Data.Maps
{
    public class StudentMap : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Name).IsRequired().HasMaxLength(100);
            builder.HasIndex(s => s.Name).IsUnique();

            builder.Property(s => s.Age).IsRequired();

            builder.Property(s => s.Height).IsRequired();

            builder.Property(s => s.Weight).IsRequired();

            builder.Property(s => s.WeeklyFrequency).IsRequired();

            builder.HasOne(s => s.Teacher).WithMany(s => s.Students).HasForeignKey(s => s.TeacherId);

            builder.HasOne(s => s.Objective).WithMany(s => s.Students).HasForeignKey(s => s.ObjectiveId);

            builder.HasMany(s => s.Sheets).WithOne(s => s.Student).OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Alunos");
        }
    }
}
