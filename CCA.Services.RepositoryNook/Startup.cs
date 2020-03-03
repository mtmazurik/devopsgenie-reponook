using System;
using System.Net.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Microsoft.Extensions.Logging;
using CCA.Services.RepositoryNook.Config;
using CCA.Services.RepositoryNook.Models;
using CCA.Services.RepositoryNook.Services;
using CCA.Services.RepositoryNook.HelperClasses;
using OpenApi = Swashbuckle.AspNetCore.Swagger;

namespace CCA.Services.RepositoryNook
{
    public class Startup
    {
        private ILoggerFactory _loggerFactory;             // leverage built in ASPNetCore logging 
        private ILogger<Startup> _logger;
        private IConfigurationRoot _configuration { get; }


        public Startup(Microsoft.AspNetCore.Hosting.IHostingEnvironment env, ILogger<Startup> logger, ILoggerFactory loggerFactory)       // ctor
        {
            var builder = new ConfigurationBuilder()        
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            _configuration = builder.Build();
            _logger = logger;
            _loggerFactory = loggerFactory;
        }
        private void OnShutdown()                                                           // shutdown; leverages applicationLifetime.ApplicationStopping which triggers it
        {
           _logger.Log(LogLevel.Information, "RepositoryNook service stopped.");
        }

        public void ConfigureServices(IServiceCollection services)                          // called by the WebHost runtime 
        {
            services.AddMvc( option => option.EnableEndpointRouting=false)                  // refactor to .NET Core 3.x as per https://stackoverflow.com/questions/55666826/where-did-imvcbuilder-addjsonoptions-go-in-net-core-3-0
                .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Include;
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });

            // injectables (DI)
            services.AddTransient<IResponse, Response>();                                   
            services.AddTransient<HttpClient>();
            services.AddTransient<IJsonConfiguration, JsonConfiguration>();
            services.AddTransient<IRepositoryService, RepositoryService>();
            services.AddTransient<IAdminService, AdminService>();
        }
        public void Configure(IApplicationBuilder app, IApplicationLifetime applicationLifetime)
        {
            app.UseMvc(routes =>
            {
                routes.MapRoute("admin", "{controller=AdminController}/{action=Index}");
                routes.MapRoute("default", "{controller=RepositoryNookController}/{action=Index}");
            });


            applicationLifetime.ApplicationStopping.Register( OnShutdown );                 // hook callback for on-shutdown event
        }
    }
}
