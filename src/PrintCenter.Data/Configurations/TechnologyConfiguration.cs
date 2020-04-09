using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrintCenter.Data.Models;

namespace PrintCenter.Data.Configurations
{
    public class TechnologyConfiguration : IEntityTypeConfiguration<Technology>
    {
        public void Configure(EntityTypeBuilder<Technology> builder)
        {
            builder
                .Property(t => t.Name)
                .IsRequired();

            builder
                .Property(t => t.Unit)
                .IsRequired();

            builder
                .HasIndex(t => t.Name)
                .IsUnique();
        }
    }
}