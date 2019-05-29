using GymSheet.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymSheet.Data.Maps
{
    public class ExerciseMap : IEntityTypeConfiguration<Exercise>
    {
        public void Configure(EntityTypeBuilder<Exercise> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name).HasMaxLength(50).IsRequired();
            builder.HasIndex(e => e.Name).IsUnique();

            builder.HasOne(e => e.MuscleGroup).WithMany(e => e.Excercises).HasForeignKey(e => e.MuscleGroupId);

            builder.HasMany(e => e.ExcerciseLists).WithOne(e => e.Excercise);

            builder.ToTable("Exercícios");
        }
    }
}
