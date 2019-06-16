using GymSheet.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymSheet.Data.Maps
{
    public class ObjectiveMap : IEntityTypeConfiguration<Objective>
    {
        public void Configure(EntityTypeBuilder<Objective> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.Name).IsRequired().HasMaxLength(50);
            builder.HasIndex(o => o.Name).IsUnique();

            builder.Property(o => o.Observation).IsRequired(false).HasMaxLength(50);

            builder.HasMany(o => o.Students).WithOne(o => o.Objective).OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("Objetivos");
        }
    }
}
