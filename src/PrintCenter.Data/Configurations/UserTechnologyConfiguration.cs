using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrintCenter.Data.Models;

namespace PrintCenter.Data.Configurations
{
    class UserTechnologyConfiguration : IEntityTypeConfiguration<UserTechnology>
    {
        public void Configure(EntityTypeBuilder<UserTechnology> builder)
        {
            builder.HasKey(t => new { t.UserId, t.TechnologyId });

            builder.HasOne(pt => pt.User)
                .WithMany(p => p.UserTechnologies)
                .HasForeignKey(pt => pt.UserId);

            builder.HasOne(pt => pt.Technology)
                .WithMany(t => t.UserTechnologies)
                .HasForeignKey(pt => pt.TechnologyId);
        }
    }
}