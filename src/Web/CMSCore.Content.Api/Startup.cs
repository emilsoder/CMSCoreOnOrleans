using System;
using System.Threading.Tasks;
using CMSCore.Content.GrainInterfaces;
using CMSCore.Shared.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
namespace CMSCore.Content.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSingleton<IClusterClient>(CreateClusterClient);
        }

        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseDefaultFiles();
            app.UseStaticFiles();
        }

        private IClusterClient CreateClusterClient(IServiceProvider serviceProvider)
        {
            //const string connectionString = "YOUR_CONNECTION_STRING_HERE";

            var client = new ClientBuilder()
                .ConfigureApplicationParts(parts =>
                {
                    parts.AddApplicationPart(typeof(IContentManagerGrain).Assembly).WithCodeGeneration();
                    parts.AddApplicationPart(typeof(IContentReaderGrain).Assembly).WithCodeGeneration();
                    parts.AddApplicationPart(typeof(IAccountManagerGrain).Assembly).WithCodeGeneration();
                    parts.AddApplicationPart(typeof(IAccountReaderGrain).Assembly).WithCodeGeneration();
                })
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = ClusterOptionsConst.ClusterId;
                    options.ServiceId = ClusterOptionsConst.ServiceId;
                })
                .UseLocalhostClustering()
                //.UseAzureStorageClustering(options => options.ConnectionString = connectionString)
                .Build();

            StartClientWithRetries(client).Wait();

            return client;
        }

        private static async Task StartClientWithRetries(IClusterClient client)
        {
            for (var i = 0; i < 5; i++)
            {
                try
                {
                    await client.Connect();
                    return;
                }
                catch (Exception)
                {
                }

                await Task.Delay(TimeSpan.FromSeconds(5));
            }
        }
    }
}