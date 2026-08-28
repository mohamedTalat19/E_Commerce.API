using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Entities.Orders;
using E_Commerce.Domain.Entities.ProductEntities;
using E_Commerce.Infrastructure.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Data.DataSeeders
{
    internal class DataSeeder(StoreDbContext _dbContext, ILogger<DataSeeder> _logger) : IDataSeeder
    {
        public async Task SeedDataAsync(CancellationToken ct = default)
        {
            var pendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync();
            if (pendingMigrations.Any())
                await _dbContext.Database.MigrateAsync(ct);

            var rootPath = Path.Combine(AppContext.BaseDirectory, "SeedFiles");

            //file path => read from it => Desrialize => addRange (dbContext)

            await SeedIfEmpty<ProductType, int>(rootPath, "types.json", ct);
            await SeedIfEmpty<ProductBrand, int>(rootPath, "brands.json", ct);
            await SeedIfEmpty<Product, int>(rootPath, "products.json", ct);
            await SeedIfEmpty<DeliveryMethod, int>(rootPath, "delivery.json", ct);

        }

        private async Task SeedIfEmpty<TEntity , TKey>(string rootPath, string fileName, CancellationToken ct)
            where TEntity : BaseEntity<TKey>
        {
            if (await _dbContext.Set<TEntity>().AnyAsync(ct))
            {
                _logger.LogInformation($"{typeof(TEntity).Name}s Are Already Seeded");
                return;
            }
            var filePath = Path.Combine(rootPath, fileName);
            if (!File.Exists(filePath))
            {
                _logger.LogInformation($"{filePath} Wasn't found To Seed Form");
                return;
            }
            using var fileStream = File.OpenRead(filePath);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            var items = await JsonSerializer.DeserializeAsync<List<TEntity>>(fileStream, options , ct);
            if (!items?.Any() ?? false) return;

            _dbContext.Set<TEntity>().AddRange(items);
            var result = await _dbContext.SaveChangesAsync();

            if(result > 0)
                _logger.LogInformation($"{result} {typeof(TEntity).Name}s Seeded");






        }
    }
}
