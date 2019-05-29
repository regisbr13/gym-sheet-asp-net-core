using GymSheet.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymSheet.Data.Maps
{
    public class ExerciseListMap : IEntityTypeConfiguration<ExcerciseList>
    {
        public void Configure(EntityTypeBuilder<ExcerciseList> builder)
        {
            builder.HasKey(el => el.Id);

            builder.Property(el => el.Frequency).IsRequired();
            builder.Property(el => el.Repetitions).IsRequired();
            builder.Property(el => el.Charge).IsRequired();

            builder.HasOne(el => el.Sheet).WithMany(el => el.ExcerciseLists).HasForeignKey(el => el.SheetId);
            builder.HasOne(el => el.Excercise).WithMany(el => el.ExcerciseLists).HasForeignKey(el => el.ExcerciseId);

            builder.ToTable("Listas_de_Exercícios");
        }
    }
}
