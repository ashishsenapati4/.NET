using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ProductCatalog.API.Data;
using ProductCatalog.API.Services;
using ProductCatalog.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductCatalog.API.Repositories;
using ProductCatalog.API.DTOs;
using ProductCatalog.API;

namespace ProductCatalog.Test.Service
{
    public class OrderServiceUnitTests
    {
        private(ApplicationDbContext db, IOrderService service) BuildTestContextAndService()
        {
            // Configure EF Core to use an in-memory database with a unique name
            var dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase($"OrderServiceTestDb_{Guid.NewGuid()}")
                .ConfigureWarnings(cfg => cfg.Ignore(InMemoryEventId.TransactionIgnoredWarning)).Options;

            // Instantiate the in-memory DbContext with the configured options.
            var dbContext = new ApplicationDbContext(dbOptions);

            // Seed test products into the in-memory DbContext.
            dbContext.Products.AddRange(
                new Product { Id=1, Name="TestProductA", Price=100m, Stock=5},
                new Product { Id=2, Name="TestProduct2", Price=200m,Stock=2}
                );
            // Seed a test customer into the in-memory DbContext.
            dbContext.Customers.AddRange(
                new Customer { Id=1, Name="TestCustomer", Email="test@sample.com"}
                );
            //persist seeded data into in-memory database.
            dbContext.SaveChanges();

            // Instantiate Real repository classes that use this in-memory DbContext.
            var orderRepo = new OrderRepository(dbContext);
            var productRepo = new ProductRepository(dbContext);
            var customerRepo = new CustomerRepository(dbContext);

            var service = new OrderService(orderRepo, productRepo, customerRepo);
            return (dbContext, service);
        }

        [Fact]
        public async Task CreateOrderAsync_WithValidInput_ReturnsOrderResponse()
        {
            // Arrange: get a brand-new in-memory context + OrderService
            var (dbContext, orderService) = BuildTestContextAndService();

            // Prepare a valid OrderCreateDTO:
            //  - CustomerId = 1 (exists in seeded data)
            //  - Two items: (ProductId=1, Quantity=2) and (ProductId=2, Quantity=1).
            var validOrderDto = new OrderCreateDTO
            {
                CustomerId = 1,
                Items = new List<OrderItemDTO>
                {
                    new(){ProductId=1,Quantity=2},
                    new(){ProductId=2, Quantity=1}
                }
            };

            // Act: attempt to create the order using the service under test
            var orderResponse = await orderService.CreateOrderAsync(validOrderDto);

            // Assert: the returned DTO is not null (order was created successfully)
            Assert.NotNull(orderResponse);

            // The returned DTO should have CustomerId = 1
            Assert.Equal(1, orderResponse.CustomerId);

            // There should be 2 items in the OrderResponseDTO
            Assert.Equal(2, orderResponse.OrderItems.Count);

            // Calculate the expected base amount: (2 × 100m) + (1 × 200m) = 400m
            var expectedBase = (2 * 100m) + (1 * 200m);
            Assert.Equal(expectedBase, orderResponse.BaseAmount);

            // Because 400m < 5000m, no discount should have been applied
            Assert.Equal(0m, orderResponse.Discount);

            // TotalAmount = BaseAmount − Discount = 400m − 0m = 400m
            Assert.Equal(expectedBase, orderResponse.TotalAmount);

            // Now verify that the in-memory database’s stock was actually updated: for Product1 and Product2
            var product1 = await dbContext.Products.FindAsync(1);
            Assert.Equal(3, product1?.Stock);

            var product2 = await dbContext.Products.FindAsync(2);
            Assert.Equal(1, product2?.Stock);
        }

        [Fact]
        public async Task CreateOrderAsync_ProductMissing_ThrowsNotFoundException()
        {
            var (dbContext, orderService) = BuildTestContextAndService();

            var invalidOrderDto = new OrderCreateDTO
            {
                CustomerId=1,
                Items = new List<OrderItemDTO>
                { 
                    new(){ProductId=999,Quantity=2}
                }
            };

            // Act & Assert: call CreateOrderAsync and expect NotFoundException with specific message

            var ex = await Assert.ThrowsAsync<NotFoundException>(
                () => orderService.CreateOrderAsync(invalidOrderDto)
                );

            Assert.Equal("Product with id 999 not found.", ex.Message);
        }

        [Fact]
        public async Task CreateOrderAsync_InsufficientStock_ThrowsInvalidOperationException()
        {
            var (dbContext, orderService) = BuildTestContextAndService();

            var insufficientStockDto = new OrderCreateDTO
            {
                CustomerId = 1,
                Items = new List<OrderItemDTO>
                {
                    new(){ProductId=1,Quantity=12}
                }
            };

            var ex = await Assert.ThrowsAsync<InvalidOperationException>(
                () => orderService.CreateOrderAsync(insufficientStockDto)
                );

            Assert.Contains("Not enough stock for product", ex.Message);
        }

        [Fact]
        public async Task GetOrderByIdAsync_ExistingOrder_ReturnsOrderResponse()
        {
            var (dbContext, orderService) = BuildTestContextAndService();

            // First, place an order so that it exists in the in-memory database.
            var createDto = new OrderCreateDTO
            {
                CustomerId = 1,
                Items = new List<OrderItemDTO>
                {
                    new() {ProductId=1, Quantity=1}
                }
            };

            var createdOrder = await orderService.CreateOrderAsync(createDto);

            // Assert: the returned DTO is not null (order was created successfully)
            Assert.NotNull(createdOrder);

            // Extract the newly assigned OrderId (populated by the service & repository)
            var orderId = createdOrder.OrderId;

            // Act: fetch the same order by ID
            var fetchedOrder = await orderService.GetOrderByIdAsync(orderId);

            // Assert: fetchedOrder should not be null (order exists)
            Assert.NotNull(fetchedOrder);

            // Validate that fetchedOrder.OrderId == orderId
            Assert.Equal(orderId, fetchedOrder.OrderId);

            // Validate that fetchedOrder.CustomerId == 1 (as we created)
            Assert.Equal(1, fetchedOrder.CustomerId);

            // Because we ordered 1 item, OrderItems.Count should be 1
            Assert.Single(fetchedOrder.OrderItems);
        }

        [Fact]
        public async Task GetOrderByIdAsync_OrderMissing_ReturnsNull()
        {
            var (dbContext, orderService) = BuildTestContextAndService();

            var missingOrder = await orderService.GetOrderByIdAsync(999);

            Assert.Null(missingOrder);
        }
    }
}
