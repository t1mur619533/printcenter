using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrintCenter.Data.Models;

namespace PrintCenter.Data.Configurations
{
    public class StreamConfiguration : IEntityTypeConfiguration<Stream>
    {
        public void Configure(EntityTypeBuilder<Stream> builder)
        {
            builder
                .Property(b => b.Name)
                .IsRequired();

            builder
                .HasIndex(u => u.Name)
                .IsUnique();
        }
    }
}