using E_Commerce.Domain.Entities.ProductEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Data.Configurations
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(p => p.ProductBrand).WithMany().HasForeignKey(p => p.BrandId);
            builder.HasOne(p => p.ProductType).WithMany().HasForeignKey(p => p.TypeId);

            builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
        }
    }
}
