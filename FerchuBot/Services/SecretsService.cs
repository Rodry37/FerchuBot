using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace FerchuBot.Services
{
    public class SecretsService
    {
        private static IConfigurationRoot config;
        private const string FerchuToken = "FerchuToken";
        public SecretsService()
        {
            MockConfiguration();
        }
        public string GetToken()
        {
            return config[FerchuToken];
        }

        private void MockConfiguration()
        {
            string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            if (string.IsNullOrWhiteSpace(env))
            {
                env = "Development";
            }

            var builder = new ConfigurationBuilder();

            if (env == "Development")
            {
                builder.AddUserSecrets<SecretsService>();
            }

            config = builder.Build();
        }
    }
}
