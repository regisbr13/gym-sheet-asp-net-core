using GymSheet.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymSheet.Data.Maps
{
    public class ExerciseListMap : IEntityTypeConfiguration<ExerciseList>
    {
        public void Configure(EntityTypeBuilder<ExerciseList> builder)
        {
            builder.HasKey(el => el.Id);

            builder.Property(el => el.Frequency).IsRequired();
            builder.Property(el => el.Repetitions).IsRequired();
            builder.Property(el => el.Charge).IsRequired();

            builder.HasOne(el => el.Sheet).WithMany(el => el.ExerciseLists).HasForeignKey(el => el.SheetId);
            builder.HasOne(el => el.Exercise).WithMany(el => el.ExerciseLists).HasForeignKey(el => el.ExerciseId);

            builder.ToTable("Listas_de_Exercícios");
        }
    }
}
