using Azure.Identity;

namespace VehicleTrackingSystem.Api.Extension
{
    internal static class AppConfigurationExtension
    {
        private static string azureAppConfigKey = "ConnectionStrings:AppConfig";

        //un-comment below condition to use Environment variable to access Key Vault instead of Visual Studio User Authentication
        public static string TenantId = Environment.GetEnvironmentVariable("AZURE_TENANT_ID", EnvironmentVariableTarget.User);
        public static string ClientId = Environment.GetEnvironmentVariable("AZURE_CLIENT_ID", EnvironmentVariableTarget.User);
        public static string ClientSecret = Environment.GetEnvironmentVariable("AZURE_CLIENT_SECRET", EnvironmentVariableTarget.User);

        internal static IWebHostBuilder ConfigureApp(this IWebHostBuilder builder)
        {
            return builder.ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.AddEnvironmentVariables();

                //comment below condition in QA, Staging, Production Environments (to connect Azure App Config Service in local run)
                if (hostingContext.HostingEnvironment.IsEnvironment("Local"))
                {
                    config.AddUserSecrets<Program>();
                }

                AddAzureAppConfiguration(config, hostingContext.HostingEnvironment.EnvironmentName);
            });
        }

        private static void AddAzureAppConfiguration(IConfigurationBuilder config, string environment)
        {
            var settings = config.Build();

            // if environment is local, use Development label to get KV variables
            string kvLabel = environment == "Local" ? "Development" : environment;

            if (!string.IsNullOrEmpty(settings[azureAppConfigKey]))
            {
                Console.WriteLine("Azure app configuration is valid");
                config.AddAzureAppConfiguration(options =>
                {
                    options.Connect(settings[azureAppConfigKey])
                           .Select("*", kvLabel)
                           .ConfigureKeyVault(kv =>
                           {
                               kv.SetCredential(
                                   //un-comment below condition to use Visual Studio User Authentication instead of Environment variable to access Key Vault
                                   new DefaultAzureCredential()
                                   //un-comment below condition to use Environment variable to access Key Vault instead of Visual Studio User Authentication
                                   //new ClientSecretCredential(TenantId, ClientId, ClientSecret)
                                   );
                           });
                }) // appsettings.environment.json can override Azure app config
                .AddJsonFile($"appsettings.{environment}.json", optional: true);
            }
            else
            {
                Console.WriteLine("Azure app configuration is not available");
            }
        }
    }
}
