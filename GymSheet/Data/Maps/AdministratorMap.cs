using GymSheet.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymSheet.Data.Maps
{
    public class AdministratorMap : IEntityTypeConfiguration<Administrator>
    {
        public void Configure(EntityTypeBuilder<Administrator> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Name).HasMaxLength(50).IsRequired();

            builder.Property(a => a.Email).HasMaxLength(50).IsRequired();
            builder.HasIndex(a => a.Email).IsUnique();

            builder.Property(a => a.Password).HasMaxLength(10).IsRequired();

            builder.ToTable("Administradores");
        }
    }
}
