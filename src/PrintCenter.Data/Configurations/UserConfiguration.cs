using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrintCenter.Data.Models;

namespace PrintCenter.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .Property(b => b.Login)
                .IsRequired();

            builder
                .HasIndex(u => u.Login)
                .IsUnique();

            builder
                .Property(b => b.Password)
                .IsRequired();

            builder
                .Property(b => b.Name)
                .IsRequired();
            
            builder
                .Property(b => b.Surname)
                .IsRequired();
        }
    }
}