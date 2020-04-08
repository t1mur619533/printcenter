using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PrintCenter.Data.Configurations;
using PrintCenter.Data.Models;

namespace PrintCenter.Data
{
    public sealed class DataContext : DbContext, IDataContext
    {
        public DbSet<User> Users { get; set; }
        
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Request> Requests { get; set; }

        public DbSet<SerialProduction> SerialProductions { get; set; }

        public DbSet<CompositeSerialProduction> CompositeSerialProductions { get; set; }

        public DbSet<Stream> Streams { get; set; }
        
        public DbSet<Invoice> Invoices { get; set; }

        public DbSet<Material> Materials { get; set; }

        public DbSet<Plan> Plans { get; set; }

        public DbSet<Technology> Technologies { get; set; }
        
        public DbSet<MaterialConsumption> MaterialConsumptions { get; set; }

        public DbSet<MaterialMovement> MaterialMovements { get; set; }

        public DbSet<Notification> Notifications { get; set; }

        public DbSet<UserTechnology> UserTechnologies { get; set; }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        public DbSet<T> DbSet<T>() where T : class
        {
            return Set<T>();
        }

        public new IQueryable<T> Query<T>() where T : class
        {
            return Set<T>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(UserConfiguration)));
            
            base.OnModelCreating(modelBuilder);
        }
    }
}