using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PrintCenter.Api.Middlewares;
using PrintCenter.Data;
using PrintCenter.Infrastructure.Accessors;
using PrintCenter.Infrastructure.Security;

namespace PrintCenter.Api
{
    public class Startup
    {
        private readonly IWebHostEnvironment environment;
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            this.environment = environment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //Предназначен для использования в качестве базы данных общего назначения для тестирования и не предназначен для имитации реляционной базы данных.
            //services.AddDbContext<DataContext>(builder => builder.UseInMemoryDatabase(Guid.NewGuid().ToString()), ServiceLifetime.Singleton, ServiceLifetime.Singleton);

            var connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<DataContext>(builder => builder.UseNpgsql(connection));
            services.AddScoped<IDataContext>(provider => provider.GetRequiredService<DataContext>());
            services.AddScoped<ITransaction>(provider => provider.GetRequiredService<DataContext>());

            services.AddScoped(typeof(IPasswordHasher<>), typeof(PasswordHasher<>));
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<ICurrentUserAccessor, CurrentUserAccessor>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddMediatR();
            services.AddAutoMapper();
            services.AddCors();
            services
                .AddControllers(opt => { opt.Filters.Add(typeof(ValidatorActionFilter)); })
                .AddJsonOptions(opt => { opt.JsonSerializerOptions.IgnoreNullValues = true; })
                .AddFluentValidation(cfg => { cfg.RegisterValidatorsFromAssemblyContaining<Domain.Users.User>(); });
            services.AddSwagger();
            services.AddJwtAuthentication(Configuration, environment);
            services.AddAuthorization();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddSerilogLogging();

            //EnsureCreated() обходит миграции, чтобы создать схему. Использовать с InMemoryDatabase 
            //scope.ServiceProvider.GetRequiredService<DataContext>().Database.EnsureCreated();

            if (environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseRouting();
            
            app.UseCors(builder =>
                builder
                    .AllowAnyMethod()
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .WithExposedHeaders("Content-Range"));

            app.UseConfiguredSwagger();

            app.UseHttpsRedirection();
            
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}