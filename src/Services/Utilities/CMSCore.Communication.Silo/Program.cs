using System;
using System.Runtime.Loader;
using System.Threading;
using System.Threading.Tasks;
using CMSCore.Shared.Configuration;
using Microsoft.Extensions.Logging;
using Orleans.Configuration;
using Orleans.Hosting;

namespace CMSCore.Communication.Silo
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
                .UseAzureStorageClustering(options => options.ConnectionString = connectionString)
                .ConfigureEndpoints(siloPort: 11111, gatewayPort: 30000)
                .ConfigureApplicationParts(parts =>
                {
                    //parts.AddApplicationPart(typeof(ValueGrain).Assembly).WithReferences();
                    //parts.AddApplicationPart(typeof(PoopGrain).Assembly).WithReferences();
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
        }

        private static async Task StopSilo()
        {
            await silo.StopAsync();
            siloStopped.Set();
        }
    }
}