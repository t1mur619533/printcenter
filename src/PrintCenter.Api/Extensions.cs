using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

namespace PrintCenter.Api
{
    public static class Extensions
    {
        public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Print Center API",
                    Description = "Print Center Web API",
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            return services;
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
                .WriteTo.Console(outputTemplate: "{Timestamp:HH:mm:ss} [{Level}] {SourceContext} {Message}{NewLine}{Exception}", theme: AnsiConsoleTheme.Code)
                .WriteTo.File($"Logs/{Assembly.GetExecutingAssembly().GetName().Name} - {DateTime.Now:dd.MM.yyyy(HH.mm.ss)}.log")
                .CreateLogger();

            loggerFactory.AddSerilog(log);
            Log.Logger = log;

            TaskScheduler.UnobservedTaskException += (s, e) => log.Error(e.Exception, "Unhandled error");
        }
    }
}