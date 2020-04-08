using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrintCenter.Data.Models;

namespace PrintCenter.Data.Configurations
{
    public class CompositeSerialProductionSerialProductionConfiguration : IEntityTypeConfiguration<CompositeSerialProductionSerialProduction>
    {
        public void Configure(EntityTypeBuilder<CompositeSerialProductionSerialProduction> builder)
        {
            builder.HasKey(t => new { t.CompositeSerialProductionId, t.SerialProductionId });

            builder.HasOne(pt => pt.CompositeSerialProduction)
                .WithMany(p => p.CompositeSerialProductionSerialProductions)
                .HasForeignKey(pt => pt.CompositeSerialProductionId);

            builder.HasOne(pt => pt.SerialProduction)
                .WithMany(t => t.CompositeSerialProductionSerialProductions)
                .HasForeignKey(pt => pt.SerialProductionId);
        }
    }

    public class CompositeSerialProductionConfiguration : IEntityTypeConfiguration<CompositeSerialProduction>
    {
        public void Configure(EntityTypeBuilder<CompositeSerialProduction> builder)
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
        }
    }
}