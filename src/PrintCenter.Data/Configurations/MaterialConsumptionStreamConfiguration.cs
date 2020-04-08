using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrintCenter.Data.Models;

namespace PrintCenter.Data.Configurations
{
    public class MaterialConsumptionStreamConfiguration : IEntityTypeConfiguration<MaterialConsumptionStream>
    {
        public void Configure(EntityTypeBuilder<MaterialConsumptionStream> builder)
        {
            builder.HasKey(t => new { t.MaterialConsumptionId, t.StreamId });

            builder.HasOne(pt => pt.MaterialConsumption)
                .WithMany(p => p.MaterialConsumptionStreams)
                .HasForeignKey(pt => pt.MaterialConsumptionId);

            builder.HasOne(pt => pt.Stream)
                .WithMany(t => t.MaterialConsumptionStreams)
                .HasForeignKey(pt => pt.StreamId);
        }
    }
}