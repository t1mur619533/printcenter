using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PrintCenter.Data;

namespace PrintCenter.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //Предназначен для использования в качестве базы данных общего назначения для тестирования и не предназначен для имитации реляционной базы данных.
            //services.AddDbContext<DataContext>(builder => builder.UseInMemoryDatabase(Guid.NewGuid().ToString()), ServiceLifetime.Singleton, ServiceLifetime.Singleton);

            var connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<DataContext>(builder => builder.UseNpgsql(connection));

            services.AddControllers();

            services.ConfigureSwagger();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddSerilogLogging();

            using var scope = app.ApplicationServices.CreateScope();

            //todo: В будущем требуется более надежная стратегия развертывания, такая как создание скриптов SQL
            scope.ServiceProvider.GetRequiredService<DataContext>().Database.Migrate();

            //EnsureCreated() обходит миграции, чтобы создать схему. Использовать с InMemoryDatabase 
            //scope.ServiceProvider.GetRequiredService<DataContext>().Database.EnsureCreated();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseConfiguredSwagger();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}