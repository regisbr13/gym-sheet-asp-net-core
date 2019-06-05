using GymSheet.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymSheet.Data.Maps
{
    public class TeacherMap : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Name).HasMaxLength(50).IsRequired();
            builder.HasIndex(t => t.Name).IsUnique();

            builder.Property(t => t.Period).HasMaxLength(15).IsRequired();
            builder.Property(t => t.Phone).IsRequired();
            builder.Property(t => t.ImgPath).IsRequired(false);

            builder.HasMany(t => t.Students).WithOne(t => t.Teacher);

            builder.ToTable("Professores");
        }
    }
}
