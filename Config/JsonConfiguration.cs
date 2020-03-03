using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DOG.RepoNook.Exceptions;


namespace DOG.RepoNook.Config
{
    public class JsonConfiguration : IJsonConfiguration
    {
        private IConfiguration _configuration;
        public JsonConfiguration()              // ctor
        {
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
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
