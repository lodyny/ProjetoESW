using AdotAqui.Data;
using AdotAqui.Models;
using AdotAqui.Models.Entities;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace AdotAqui
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetService<UserManager<User>>();
                var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole<int>>>();
                DataSeeder.SeedDatabase(userManager, roleManager);
            }
            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                var builtConfig = config.Build();
                config.AddAzureKeyVault(
                    $"https://{builtConfig["Vault"]}.vault.azure.net/",
                    builtConfig["ClientId"],
                    builtConfig["ClientSecret"]);
            }).UseStartup<Startup>();
    }
}
