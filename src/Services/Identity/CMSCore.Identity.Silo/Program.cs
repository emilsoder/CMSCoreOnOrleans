using System;
using System.Runtime.Loader;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using CMSCore.Identity.Data;
using CMSCore.Identity.Data.Extensions;
using CMSCore.Identity.Grains;
using CMSCore.Identity.Models;
using CMSCore.Shared.Configuration;
using EFCore.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Orleans.Configuration;
using Orleans.Hosting;

namespace CMSCore.Identity.Silo
{
    internal class Program
    {
        private static ISiloHost silo;
        private static readonly ManualResetEvent siloStopped = new ManualResetEvent(false);

        private static void Main(string[] args)
        {
            var connectionString = AzureStorageConst.CMSCoreConnectionString;

            silo = new SiloHostBuilder()
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = ClusterOptionsConst.ClusterId;
                    options.ServiceId = ClusterOptionsConst.ServiceId;
                })
                .ConfigureServices(services => { services.ConfigureRepository(); })
                .UseAzureStorageClustering(options => options.ConnectionString = connectionString)
                .ConfigureEndpoints(siloPort: 11111, gatewayPort: 30000)
                .ConfigureApplicationParts(parts =>
                {
                    parts.AddApplicationPart(typeof(AuthenticationGrain).Assembly).WithReferences();
                    parts.AddApplicationPart(typeof(IdentityManagerGrain).Assembly).WithReferences();
                })
                .ConfigureLogging(builder => builder.SetMinimumLevel(LogLevel.Warning)
                    .AddConsole())
                .Build();

            Task.Run(StartSilo);

            AssemblyLoadContext.Default.Unloading += context =>
            {
                Task.Run(StopSilo);
                siloStopped.WaitOne();
            };

            siloStopped.WaitOne();
        }

        private static async Task StartSilo()
        {
            await silo.StartAsync();
            Console.WriteLine("Silo started");
        }

        private static async Task StopSilo()
        {
            await silo.StopAsync();
            Console.WriteLine("Silo stopped");
            siloStopped.Set();
        }
    }

    public static class DependencyInjection
    {
        public static IServiceCollection ConfigureRepository(this IServiceCollection services)
        {
            services.AddDbContext<CMSCoreIdentityDbContext>(IdentityDbContextOptions.DefaultPostgresOptionsBuilder, ServiceLifetime.Singleton);
            services.AddIdentity<ApplicationUser, IdentityRole<string>>(x =>
            {
                x.Password.RequireDigit = false;
                x.Password.RequireLowercase = false;
                x.Password.RequireNonAlphanumeric = false;
                x.Password.RequireUppercase = false;
                x.Password.RequiredLength = 3;
                x.Password.RequiredUniqueChars = 0;

                x.User.RequireUniqueEmail = true;
                
                x.ClaimsIdentity.RoleClaimType = ClaimTypes.Role;
                x.ClaimsIdentity.UserIdClaimType = ClaimTypes.NameIdentifier;
                x.ClaimsIdentity.UserNameClaimType = ClaimTypes.Name;
            });

            services.AddSingleton<IRepository, RepositoryBase>();
            return services;
        }
    }
}