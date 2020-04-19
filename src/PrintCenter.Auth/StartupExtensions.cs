using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PrintCenter.Data;
using PrintCenter.Data.Models;
using PrintCenter.Domain.Infrastructure;
using PrintCenter.Domain.Users;
using PrintCenter.Infrastructure.Extensions;
using PrintCenter.Infrastructure.Security;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using User = PrintCenter.Data.Models.User;

namespace PrintCenter.Auth
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference {Type = ReferenceType.SecurityScheme, Id = "Bearer"}
                        },
                        new string[] { }
                    }
                });

                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Print Center Auth",
                    Description = "Print Center Auth Web API",
                });
                c.CustomSchemaIds(y => y.FullName);

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            return services;
        }

        public static IServiceCollection AddMediatR(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetAssembly(typeof(UsersEnvelope)));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(TransactionPipelineBehavior<,>));
            return services;
        }

        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly(), Assembly.GetAssembly(typeof(UsersEnvelope)),
                Assembly.GetAssembly(typeof(User)));
            return services;
        }

        public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.RequireAuthenticatedSignIn = false;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Secret"])),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };

                    if (environment.IsDevelopment())
                    {
                        x.Events = new JwtBearerEvents
                        {
                            OnMessageReceived = context =>
                            {
                                // если запрос локальный
                                if (context.HttpContext.Request.IsLocal())
                                {
                                    //проверяем наличие токена
                                    var token = context.HttpContext.Request.Headers["Authorization"];
                                    //в случае, если токена нет
                                    if (token.Count <= 0)
                                    {
                                        //создаем токен для пользователя с логином Local и правами SuperAdmin
                                        context.Token =
                                            new JwtTokenGenerator(configuration).CreateToken(0, "Local",
                                                nameof(Role.SuperAdmin));
                                    }
                                }

                                return Task.CompletedTask;
                            }
                        };
                    }
                });
        }

        public static IApplicationBuilder UseConfiguredSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Print Center Auth V1");
                c.RoutePrefix = string.Empty;
            });
            return app;
        }

        public static void AddSerilogLogging(this ILoggerFactory loggerFactory)
        {
            if (!Directory.Exists("Logs"))
                Directory.CreateDirectory("Logs");

            var log = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Console(
                    outputTemplate: "{Timestamp:HH:mm:ss} [{Level}] {SourceContext} {Message}{NewLine}{Exception}",
                    theme: AnsiConsoleTheme.Code)
                .WriteTo.File(
                    $"Logs/{Assembly.GetExecutingAssembly().GetName().Name} - {DateTime.Now:dd.MM.yyyy(HH.mm.ss)}.log")
                .CreateLogger();

            loggerFactory.AddSerilog(log);
            Log.Logger = log;

            TaskScheduler.UnobservedTaskException += (s, e) => log.Error(e.Exception, "Unhandled error");
        }

        public static IHost Seed(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            var serviceProvider = scope.ServiceProvider;
            try
            {
                //todo: В будущем требуется более надежная стратегия развертывания, такая как создание скриптов SQL
                var dbContext = serviceProvider.GetRequiredService<DataContext>();
                dbContext.Database.Migrate();
                var user = dbContext.Users.FirstOrDefault(_ => _.Role == Role.SuperAdmin);
                if (user == null)
                {
                    user = new User
                    {
                        Login = "admin",
                        Name = "admin",
                        Surname = "admin",
                        Role = Role.SuperAdmin
                    };
                    user.PasswordHash = serviceProvider.GetRequiredService<IPasswordHasher<User>>()
                        .HashPassword(user, "admin");
                    dbContext.Users.Add(user);
                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred seeding the DB.");
            }

            return host;
        }
    }
}
