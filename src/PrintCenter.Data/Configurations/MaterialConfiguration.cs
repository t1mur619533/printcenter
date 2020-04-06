using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrintCenter.Data.Models;

namespace PrintCenter.Data.Configurations
{
    public class MaterialConfiguration : IEntityTypeConfiguration<Material>
    {
        public void Configure(EntityTypeBuilder<Material> builder)
        {
           builder
                .Property(m => m.Name)
                .IsRequired();

            builder
                .HasIndex(m => new { m.Name, m.Parameter })
                .IsUnique();
        }
    }
}