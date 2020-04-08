using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrintCenter.Data.Models;

namespace PrintCenter.Data.Configurations
{
    class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            //builder
            //    .Property(b => b.Content)
            //    .IsRequired();

            //builder
            //    .Property(b => b.Date)
            //    .IsRequired();
        }
    }
}