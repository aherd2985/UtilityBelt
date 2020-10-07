using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using UtilityBelt.Models;

namespace UtilityBelt
{
    public class ServiceProviderBuilder
    {
        public static IServiceProvider GetServiceProvider(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddUserSecrets(typeof(Program).Assembly)
                .AddCommandLine(args)
                .Build();
            ServiceCollection services = new ServiceCollection();

            services.Configure<SecretsModel>(configuration.GetSection("SecretsModel"));

            ServiceProvider provider = services.BuildServiceProvider();
            return provider;
        }
    }
}