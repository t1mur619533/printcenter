using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrintCenter.Data.Models;

namespace PrintCenter.Data.Configurations
{
    public class SerialProductionConfiguration : IEntityTypeConfiguration<SerialProduction>
    {
        public void Configure(EntityTypeBuilder<SerialProduction> builder)
        {
           builder
                .Property(b => b.Code)
                .IsRequired();

           builder
                .HasIndex(u => u.Code)
                .IsUnique();

           builder
                .Property(b => b.Name)
                .IsRequired();

           builder
                .HasIndex(u => u.Name)
                .IsUnique();

           builder
                .HasMany(b => b.MaterialConsumptions)
                .WithOne();
        }
    }
}