using System;
using System.Runtime.Loader;
using System.Threading;
using System.Threading.Tasks;
using CMSCore.Content.Data;
using CMSCore.Content.Grains;
using CMSCore.Shared.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;

namespace CMSCore.Content.Silo
{
    internal class Program
    {
        private static ISiloHost silo;
        private static readonly ManualResetEvent siloStopped = new ManualResetEvent(false);

        private static void Main(string[] args)
        {
            //var connectionString = AzureStorageConst.CMSCoreConnectionString;            

            silo = new SiloHostBuilder()
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = ClusterOptionsConst.ClusterId;
                    options.ServiceId = ClusterOptionsConst.ServiceId;
                })
                .ConfigureServices(services => { services.ConfigureRepository(); })
                .UseLocalhostClustering()
                //.UseAzureStorageClustering(options => options.ConnectionString = connectionString)
                //.ConfigureEndpoints(siloPort: 11111, gatewayPort: 30000)
                .ConfigureApplicationParts(parts =>
                {
                    parts.AddApplicationPart(typeof(ContentGrain).Assembly)
                        .WithReferences();
                    parts.AddApplicationPart(typeof(AccountGrain).Assembly)
                        .WithReferences();
                })
                .ConfigureLogging(builder =>
                {
                    builder.SetMinimumLevel(LogLevel.Information)
                        .AddConsole();
                })
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
            //services.AddDbContext<ContentDbContext>(x => x.UseNpgsql(DatabaseConnectionConst.CMSCore), ServiceLifetime.Singleton);
            services.AddDbContext<ContentDbContext>(x => x.UseNpgsql(DatabaseConnectionConst.CMSCore),
                ServiceLifetime.Singleton);
            //services.AddScoped<ContentDbContext>(provider => provider.GetService<ContentDbContext>());
            //services.AddSingleton<IRepository, RepositoryBase>(x => new RepositoryBase(x.GetService<DbContext>()));ContentDbContextOptions.DefaultPostgresOptionsBuilder
            return services;
        }
    }
}