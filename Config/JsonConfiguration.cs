using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DevopsGenie.Reponook.Exceptions;


namespace DevopsGenie.Reponook.Config
{
    public class JsonConfiguration : IJsonConfiguration
    {
        private IConfiguration _configuration;
        public JsonConfiguration()              // ctor
        {
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();         // allows reading out of the local (not uploaded) launchSettings.json for Docker
                _configuration = configBuilder.Build();
        }


        public string AtlasMongoConnection
        {
            get
            {
                string connectionString = _configuration["AtlasMongoConnection"];
                if (connectionString is null) throw new ConfigFileReadError("Check appsettings.json; AtlasMongoConnection not found.");
                return connectionString;
            }
        }
    }
}
