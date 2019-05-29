using GymSheet.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymSheet.Data.Maps
{
    public class MuscleGroupMap : IEntityTypeConfiguration<MuscleGroup>
    {
        public void Configure(EntityTypeBuilder<MuscleGroup> builder)
        {
            builder.HasKey(mg => mg.Id);

            builder.Property(mg => mg.Name).IsRequired().HasMaxLength(50);
            builder.HasIndex(mg => mg.Name).IsUnique();

            builder.HasMany(mg => mg.Excercises).WithOne(mg => mg.MuscleGroup);

            builder.ToTable("Grupos_Musculares");
        }
    }
}
