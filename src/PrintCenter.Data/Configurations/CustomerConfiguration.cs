using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrintCenter.Data.Models;

namespace PrintCenter.Data.Configurations
{
    class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder
                .Property(b => b.Name)
                .IsRequired();

            builder
                .HasIndex(u => u.Name)
                .IsUnique();

            builder.HasData(
                new Customer
                {
                    Id = 1,
                    Name = "Customer",
                    Description = "Temp customer"
                }
            );
        }
    }
}