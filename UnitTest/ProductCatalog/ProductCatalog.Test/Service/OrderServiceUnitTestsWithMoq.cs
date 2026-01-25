using Moq;
using ProductCatalog.API.DTOs;
using ProductCatalog.API.Repositories;
using ProductCatalog.API.Services;
using ProductCatalog.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductCatalog.API;

namespace ProductCatalog.Test.Service
{
    public class OrderServiceUnitTestsWithMoq
    {
        private readonly Mock<IOrderRepository> _mockOrderRepo;
        private readonly Mock<IProductRepository> _mockProductRepo;
        private readonly Mock<ICustomerRepository> _mockCustomerRepo;

        private readonly IOrderService _orderService;
        public OrderServiceUnitTestsWithMoq()
        {
            _mockOrderRepo = new Mock<IOrderRepository>();
            _mockProductRepo = new Mock<IProductRepository>();
            _mockCustomerRepo = new Mock<ICustomerRepository>();


            // Finally, create the OrderService under test, injecting the mocks’ .Object properties
            _orderService = new OrderService(_mockOrderRepo.Object, _mockProductRepo.Object, _mockCustomerRepo.Object);
        }

        // Unit Test for Successful Order Creation
        [Fact]
        public async Task CreateOrderAsync_WithValidInput_ReturnsOrderResponse()
        {
            int customerId = 1;

            var orderDto = new OrderCreateDTO
            {
                CustomerId = customerId,
                Items = new List<OrderItemDTO>
                {
                    new() { ProductId = 10, Quantity = 2 },
                    new() { ProductId = 20, Quantity = 1 }
                }
            };
            _mockCustomerRepo.Setup(r => r.GetByIdAsync(customerId))
                .ReturnsAsync(new Customer { Id = customerId, Name = "Test Customer" });

            _mockProductRepo.Setup(r => r.GetByIdAsync(10))
                .ReturnsAsync(new Product { Id = 10, Name = "Product10", Price = 50, Stock = 5 });

            _mockProductRepo.Setup(r => r.GetByIdAsync(20))
                .ReturnsAsync(new Product { Id = 20, Name = "Product20", Price = 100, Stock = 3 });

            // Setup the order repository mock’s AddAsync to do nothing (complete the Task) but be marked Verifiable
            // Whenever AddAsync is called with any Order object (doesn’t matter which one), just return a completed Task.
            // In Verify, it checks "was this method called with any Order?"(regardless of the actual order details).
            _mockOrderRepo.Setup(r => r.AddAsync(It.IsAny<Order>()))
                .Returns(Task.CompletedTask).Verifiable();

            // Act: Call the service’s CreateOrderAsync
            var createdOrder = await _orderService.CreateOrderAsync(orderDto);

            // Assert: The returned DTO should not be null (order was created successfully)
            Assert.NotNull(createdOrder);

            // Assert: The CustomerId in the result should equal the one we passed (1)
            Assert.Equal(1, createdOrder.CustomerId);

            // Assert: The service should return 2 order items (we passed two items)
            Assert.Equal(2, createdOrder.OrderItems.Count);

            // Verify that AddAsync(Order) was called exactly once on the mock order repository
            _mockOrderRepo.Verify(r => r.AddAsync(It.IsAny<Order>()), Times.Once);

        }

        //Unit Test for customer not found
        [Fact]
        public async Task CreateOrderAsync_CustomerNotFound_ThrowsNotFoundException()
        {
            // Setup CustomerRepository.GetByIdAsync(any int) to always return null (no such customer)
            // This will return null regardless of what CustomerId you ask for.
            _mockCustomerRepo.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Customer?)null);

            var orderDto = new OrderCreateDTO
            {
                CustomerId = 999,
                Items = new List<OrderItemDTO>
                {
                    new() { ProductId = 1, Quantity = 1 }
                }
            };

            // Act & Assert: calling CreateOrderAsync should immediately throw NotFoundException
            await Assert.ThrowsAsync<NotFoundException>(() =>
            _orderService.CreateOrderAsync(orderDto));
        }

        // Unit Test for Product Not Found
        [Fact]
        public async Task CreateOrderAsync_ProductNotFound_ThrowsNotFoundException()
        {
            int customerId = 1;
            _mockCustomerRepo.Setup(r => r.GetByIdAsync(customerId))
                .ReturnsAsync(new Customer { Id = customerId });
  

            _mockProductRepo.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Product?)null);

            var orderDto = new OrderCreateDTO
            {
                CustomerId = customerId,
                Items = new List<OrderItemDTO>
                {
                    new() {ProductId=999, Quantity=1}
                }
            };

            await Assert.ThrowsAsync<NotFoundException>(() =>
                _orderService.CreateOrderAsync(orderDto)
            );  
        }

        // Unit Test for Insufficient Stock
        [Fact]
        public async Task CreateOrderAsync_InsufficientStock_ThrowsInvalidOperationException()
        {
            int customerId = 1;
            _mockCustomerRepo.Setup(r => r.GetByIdAsync(customerId))
                .ReturnsAsync(new Customer { Id = customerId });

            // Setup ProductRepository.GetByIdAsync(10) to return a Product with only Stock=1
            _mockProductRepo.Setup(r => r.GetByIdAsync(10))
                .ReturnsAsync(new Product { Id = 10, Stock = 1 });

            var orderDto = new OrderCreateDTO
            {
                CustomerId = customerId,
                Items = new List<OrderItemDTO>
                {
                    new() {ProductId=10, Quantity=5}
                }
            };

            // Act & Assert: the service should throw InvalidOperationException because 5 > stock(1)
            await Assert.ThrowsAsync<InvalidOperationException>(() => 
                _orderService.CreateOrderAsync(orderDto)
            );
        }

        // Unit Test for Fetching an Existing Order
        [Fact]
        public async Task GetOrderByIdAsync_ExistingOrder_ReturnsOrderResponse()
        {
            int orderId = 100;
            var order = new Order
            {
                Id = orderId,
                CustomerId = 1,
                BaseAmount = 150,
                DiscountAmount = 0,
                TotalAmount = 150,
                OrderItems = new List<OrderItem>
                {
                    new OrderItem{ProductId = 10,Quantity=3,UnitPrice=50, LineTotal=150}
                }
            };

            _mockOrderRepo.Setup(r => r.GetByIdAsync(orderId))
                .ReturnsAsync(order);

            var result = await _orderService.GetOrderByIdAsync(orderId);

            Assert.NotNull(result);

            Assert.Equal(orderId, result.OrderId);

            Assert.Equal(1, result.CustomerId);

            Assert.Single(result.OrderItems);

        }

        //Unit Test for Fetching a Non-Existent Order
        [Fact]
        public async Task GetOrderByIdAsync_OrderMissing_ReturnsNull()
        {
            _mockOrderRepo.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Order?)null);
            int orderId = 999;

            var order = await _orderService.GetOrderByIdAsync(orderId);

            Assert.Null(order);
            _mockOrderRepo.Verify(r => r.GetByIdAsync(orderId), Times.Once);
        }
    }
}
