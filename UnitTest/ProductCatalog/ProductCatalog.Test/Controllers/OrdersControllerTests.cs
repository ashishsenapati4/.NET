using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductCatalog.API.Controllers;
using ProductCatalog.API.DTOs;
using ProductCatalog.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
Mock<IOrderService> creates a fake version of the service to simulate its behavior without connecting to a real database.
The controller is instantiated with the mocked service, isolating the controller’s logic during tests.
Tests follow the Arrange-Act-Assert pattern:
Arrange: Set up mock behavior and test data.
Act: Call the controller method.
Assert: Verify that the returned HTTP response type and data are as expected.
The Setup method configures how the mock responds to specific calls.
Assert.IsType<T>(…) checks that the controller returns the expected HTTP status code.
Model validation errors are manually added in tests to simulate invalid inputs
*/

namespace ProductCatalog.Test.Controllers
{
    public class OrdersControllerTests
    {
        private readonly Mock<IOrderService> _mockOrderService;

        private readonly OrdersController _controller;
        public OrdersControllerTests()
        {
            _mockOrderService = new Mock<IOrderService>();
            _controller = new OrdersController(_mockOrderService.Object);
        }

        // Test method: GET /order/{id} with an existing order ID returns 200 OK and the order data
        [Fact]
        public async Task GetOrder_ExistingId_ReturnsOkWithOrder()
        {
            int orderId = 1;
            var orderResponse = new OrderResponseDTO { OrderId = orderId, CustomerId = 123 };

            _mockOrderService.Setup(s => s.GetOrderByIdAsync(orderId)).ReturnsAsync(orderResponse);

            var result = await _controller.GetOrder(orderId);

            var okResult = Assert.IsType<OkObjectResult>(result);

            Assert.Equal(orderResponse, okResult.Value);

        }

        // Test method: GET /order/{id} with a non-existing order ID returns 404 NotFound
        [Fact]
        public async Task GetOrder_NonExistingId_ReturnsNotFound()
        {
            _mockOrderService.Setup(x => x.GetOrderByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((OrderResponseDTO?)null);

            var result = await _controller.GetOrder(99);

            Assert.IsType<NotFoundResult>(result);
        }

        // Test method: POST /order with invalid model returns 400 BadRequest
        [Fact]
        public async Task CreateOrder_InvalidModel_ReturnsBadRequest()
        {
            // Arrange: manually add a model validation error to simulate invalid input
            _controller.ModelState.AddModelError("CustomerId", "Required");

            // Act: call CreateOrder with an empty DTO (which is invalid due to missing CustomerId)
            var result = await _controller.CreateOrder(new OrderCreateDTO());

            // Assert: verify the response is BadRequestObjectResult (HTTP 400)
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);

            Assert.NotNull(badRequestResult.Value);

        }

    }
}
