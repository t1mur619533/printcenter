using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using PrintCenter.Shared;

namespace PrintCenter.Web.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");
            var configuration = await GetConfiguration(builder.Services);
            builder.Services.AddSingleton(s => configuration);
            builder.Services.AddSingleton(provider => new HttpClient
            {
                BaseAddress = new Uri(provider.GetService<Configuration>().ApiUri)
            });
            await builder.Build().RunAsync();

            async Task<Configuration> GetConfiguration(IServiceCollection services)
            {
                await using var provider = services.BuildServiceProvider();
                var baseUri = provider.GetRequiredService<NavigationManager>().BaseUri;
                var url = $"{(baseUri.EndsWith('/') ? baseUri : baseUri + "/")}api/Configuration";
                using var client = new HttpClient();
                return await client.GetJsonAsync<Configuration>(url);
            }
        }
    }
}