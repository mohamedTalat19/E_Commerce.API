using E_Commerce.Domain.Contracts;
using System.Threading.Tasks;

namespace E_Commerce.API.Extensions
{
    public static class WepAppExtension
    {
        public static async Task<WebApplication> InitializeDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var seeder = scope.ServiceProvider.GetRequiredKeyedService<IDataSeeder>("Catalog");
            var IdentitySeeder = scope.ServiceProvider.GetRequiredKeyedService<IDataSeeder>("Identity");
            await seeder.SeedDataAsync();
            await IdentitySeeder.SeedDataAsync();

            return app;
        }
    }
}
