using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using ProductCatalog.API.Data;
using ProductCatalog.API.Models;
using ProductCatalog.API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.Test.Repositories
{
    public class ProductRepositoryTests
    {
        // Helper method that creates a fresh, isolated ApplicationDbContext using EF Core InMemory provider
        private ApplicationDbContext GetInmemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
                .Options;

            var context = new ApplicationDbContext(options);

            // Seed initial product data into the in-memory database for testing
            context.Products.AddRange(
                new Product { Id = 1, Name = "Test Laptop", Price = 60000m, Stock = 20 },
                new Product { Id = 2, Name = "Test Smartphone", Price = 25000m, Stock = 50 }
                );

            context.SaveChanges();

            return context;
                
        }

        // Test method to verify GetByIdAsync returns a product when the product exists in the database
        [Fact]
        public async Task GetByIdAsync_ProductExists_ReturnsProduct()
        {
            var context = GetInmemoryDbContext();
            var repository = new ProductRepository(context);

            var product = await repository.GetByIdAsync(1);

            Assert.NotNull(product);
            Assert.Equal("Test Laptop", product.Name);
            Assert.Equal(60000m, product.Price);
        }

        // Test method to verify GetByIdAsync returns null when the product does not exist
        [Fact]
        public async Task GetByIdAsync_ProductNotExist_ReturnsNull()
        {
            var context = GetInmemoryDbContext();
            var repository = new ProductRepository(context);

            var product = await repository.GetByIdAsync(999);
            Assert.Null(product);
        }

        // Test method to verify AddAsync successfully adds a product to the database
        [Fact]
        public async Task AddAsync_ProductIsAdded_ProductExistInDb()
        {
            var context = GetInmemoryDbContext();
            var repo = new ProductRepository(context);

            var product = new Product { Id = 3, Name = "Samsung s25", Price = 80000m, Stock = 50 };
            await repo.AddAsync(product);
            await repo.SaveChangesAsync();

            var productFromDb = await repo.GetByIdAsync(3);
            Assert.NotNull(productFromDb);
            Assert.Equal(product, productFromDb);
        }

        // Test method to verify changes to a product are persisted correctly after SaveChangesAsync
        [Fact]
        public async Task SaveChangesAsync_ModifiesData_DataIsPersisted()
        {
            var context = GetInmemoryDbContext();
            var repo = new ProductRepository(context);

            var productFromDb = await repo.GetByIdAsync(1);
            Assert.NotNull(productFromDb);

            productFromDb!.Stock = 30;
            await repo.SaveChangesAsync();

            var updatedProduct = await repo.GetByIdAsync(1);
            Assert.NotNull(updatedProduct);
            Assert.Equal(30, updatedProduct!.Stock);
        }
    }
}
