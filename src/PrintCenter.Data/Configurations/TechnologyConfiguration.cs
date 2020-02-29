using System;
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
                .Property(b => b.Name)
                .IsRequired();

            builder
                .Property(b => b.Unit)
                .IsRequired();

            builder
                .HasIndex(u => u.Name)
                .IsUnique();
        }
    }
}