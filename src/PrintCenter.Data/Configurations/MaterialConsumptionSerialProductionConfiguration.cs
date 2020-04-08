using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrintCenter.Data.Models;

namespace PrintCenter.Data.Configurations
{
    public class MaterialConsumptionSerialProductionConfiguration : IEntityTypeConfiguration<MaterialConsumptionSerialProduction>
    {
        public void Configure(EntityTypeBuilder<MaterialConsumptionSerialProduction> builder)
        {
            builder.HasKey(t => new { t.MaterialConsumptionId, t.SerialProductionId });

            builder.HasOne(pt => pt.MaterialConsumption)
                .WithMany(p => p.MaterialConsumptionSerialProductions)
                .HasForeignKey(pt => pt.MaterialConsumptionId);

            builder.HasOne(pt => pt.SerialProduction)
                .WithMany(t => t.MaterialConsumptionSerialProductions)
                .HasForeignKey(pt => pt.SerialProductionId);
        }
    }
}