using System;
using System.IO;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PrintCenter.Data.Models;
using PrintCenter.Domain.Users;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using User = PrintCenter.Data.Models.User;

namespace PrintCenter.Api
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
                    Title = "Print Center API",
                    Description = "Print Center Web API",
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
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
            //services.AddScoped(typeof(IPipelineBehavior<,>), typeof(DBContextTransactionPipelineBehavior<,>));
            return services;
        }

        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly(), Assembly.GetAssembly(typeof(UsersEnvelope)),
                Assembly.GetAssembly(typeof(User)));
            return services;
        }

        public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
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
                    //todo: костыльный обход авторизации, для удобства проверки апи при запуске локально, в дальнейшем нужно найти более гигиеничный сбособ
#if DEBUG
                    x.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            // если запрос локальный
                            if (context.HttpContext.Request.IsLocal() )
                            {
                                //проверяем наличие токена
                                var token = context.HttpContext.Request.Headers["Authorization"];
                                //в случае, если токена нет
                                if (token.Count <= 0)
                                {
                                    //подписываем юзера с логином local и ролью SuperAdmin, чтобы был доступ к апи
                                    context.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new[]
                                    {
                                        new Claim(ClaimsIdentity.DefaultNameClaimType, "local"),
                                        new Claim(ClaimsIdentity.DefaultRoleClaimType, nameof(Role.SuperAdmin))
                                    }));
                                }
                            }
                            return Task.CompletedTask;
                        }
                    };
#endif
                });
        }

        public static IApplicationBuilder UseConfiguredSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Print Center API V1");
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
    }
}