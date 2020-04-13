using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrintCenter.Data.Models;

namespace PrintCenter.Data.Configurations
{
    class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.HasKey(_ => _.Id);
            
            builder
                .Property(_ => _.Content)
                .IsRequired();

            builder
                .Property(_ => _.CreatedDate)
                .IsRequired();
        }
    }
}