using System.Linq;
using BTDBPart.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace WebApplication3.Models
{
    public static class SettingsExtensions
    {
        public static IWebHostBuilder UseSettings(this IWebHostBuilder @this, string path)
        {
            return @this
                .ConfigureServices((ctx, services) =>
                {
                    var configuration = new ConfigurationBuilder()
                        .SetBasePath(ctx.HostingEnvironment.ContentRootPath)
                        .Add(new SettingsConfigurationSource {Path = path})
                        .Build();

                    var configurationProvider = (SettingsConfigurationProvider) configuration.Providers.Single();
                    services.AddSingleton<ISettingsManager>(sp =>
                        new SettingsManager(configurationProvider, sp.GetRequiredService<IOptionsMonitor<Settings>>()));

                    services.Configure<Settings>(configuration);
                });
        }
    }
}