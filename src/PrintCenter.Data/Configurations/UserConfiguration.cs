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
                .HasAlternateKey(u => u.Login);

            builder
                .Property(u => u.Login)
                .IsRequired();

            builder
                .Property(b => b.PasswordHash)
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