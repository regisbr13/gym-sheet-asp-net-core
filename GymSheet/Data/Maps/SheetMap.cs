using GymSheet.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymSheet.Data.Maps
{
    public class SheetMap : IEntityTypeConfiguration<Sheet>
    {
        public void Configure(EntityTypeBuilder<Sheet> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Name).IsRequired().HasMaxLength(15);
            builder.HasIndex(s => s.Name).IsUnique();

            builder.Property(s => s.Register).IsRequired();

            builder.Property(s => s.Validate).IsRequired();

            builder.HasOne(s => s.Student).WithMany(s => s.Sheets).HasForeignKey(s => s.StudentId);

            builder.HasMany(s => s.ExerciseLists).WithOne(s => s.Sheet).OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Fichas");
        }
    }
}
