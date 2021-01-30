using ECommerce.Api.Products.Db;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Api.Products.Tests
{
    public class DatabaseFixture : IDisposable
    {
        public ProductsDbContext dbContext { get; set; }

        public DatabaseFixture()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase(nameof(DatabaseFixture))
                .Options;
            dbContext = new ProductsDbContext(options);
            CreateProducts(dbContext);
        }

        private void CreateProducts(ProductsDbContext dbContext)
        {
            if (dbContext != null && !dbContext.Products.Any())
            {
                for (int i = 1; i <= 10; i++)
                {
                    dbContext.Products.Add(new Product()
                    {
                        Id = i,
                        Name = Guid.NewGuid().ToString(),
                        Inventory = i + 10,
                        Price = (decimal)(i * 3.14)
                    });
                }
                dbContext.SaveChanges();
            }
        }

        public void Dispose()
        {
            dbContext = null;
        }
    }
}
