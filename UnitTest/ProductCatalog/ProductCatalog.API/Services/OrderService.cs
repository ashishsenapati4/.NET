using ProductCatalog.API.DTOs;
using ProductCatalog.API.Models;
using ProductCatalog.API.Repositories;

namespace ProductCatalog.API.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICustomerRepository _customerRepository;
        public OrderService(
            IOrderRepository orderRepository,
            IProductRepository productRepository,
            ICustomerRepository customerRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _customerRepository = customerRepository;
        }

        public async Task<OrderResponseDTO?> CreateOrderAsync(OrderCreateDTO orderCreateDTO)
        {
            var customer = await _customerRepository.GetByIdAsync(orderCreateDTO.CustomerId);
            if(customer == null)
            {
                throw new NotFoundException($"Customer with id {orderCreateDTO.CustomerId} not found.");
            }
            if(orderCreateDTO.Items == null || !orderCreateDTO.Items.Any())
            {
                throw new ArgumentException($"Order must have atleast one item.");
            }
            try
            {
                Order order = new Order
                {
                    CustomerId = customer.Id,
                    OrderDate = DateTime.UtcNow,
                    OrderItems = new List<OrderItem>()
                };
                decimal baseAmount = 0;

                foreach(var itemDto in orderCreateDTO.Items)
                {
                    var product = await _productRepository.GetByIdAsync(itemDto.ProductId);
                    if(product == null)
                    {
                        throw new NotFoundException($"Product with id {itemDto.ProductId} not found.");
                    }
                    if(itemDto.Quantity <= 0)
                    {
                        throw new ArgumentException("Quantity must be greater than zero.");
                    }
                    if(product.Stock < itemDto.Quantity)
                    {
                        throw new InvalidOperationException($"Not enough stock for product {product.Name}. Available: {product.Stock}, requested: {itemDto.Quantity}");
                    }

                    decimal lineTotal = itemDto.Quantity * product.Price;

                    order.OrderItems.Add(new OrderItem
                    {
                        ProductId = product.Id,
                        Quantity = itemDto.Quantity,
                        UnitPrice = product.Price,
                        LineTotal = lineTotal
                    });

                    baseAmount += lineTotal;
                    product.Stock -= itemDto.Quantity;
                }
                order.BaseAmount = baseAmount;
                order.DiscountAmount = CalculateDiscount(baseAmount);
                order.TotalAmount = baseAmount - order.DiscountAmount;

                await _orderRepository.AddAsync(order);
                await _productRepository.SaveChangesAsync();
                await _orderRepository.SaveChangesAsync();

                return MapToOrderDTO(customer, order); 

            }
            catch
            {
                throw;
            }
           
        }

        public async Task<OrderResponseDTO?> GetOrderByIdAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if(order == null)
            {
                return null;
            }
            var customer = order.Customer ?? await _customerRepository.GetByIdAsync(id);

            return MapToOrderDTO(customer, order);
        }

        private decimal CalculateDiscount(decimal baseAmount)
        {
            if(baseAmount > 5000)
            {
                return baseAmount * 0.05m;
            }
            return 0;
        }

        private OrderResponseDTO MapToOrderDTO(Customer? customer, Order order)
        {
            return new OrderResponseDTO
            {
                OrderId = order.Id,
                CustomerId = order.CustomerId,
                CustomerName = customer?.Name ?? string.Empty,
                CustomerEmail = customer?.Email ?? string.Empty,
                OrderDate = order.OrderDate,
                BaseAmount = order.BaseAmount,
                Discount = order.DiscountAmount,
                TotalAmount = order.TotalAmount,
                OrderItems = order.OrderItems.Select(item => new OrderItemResponseDTO
                {
                    OrderItemId = item.Id,
                    ProductId = item.ProductId,
                    ProductName = item.Product?.Name ?? string.Empty,
                    Description = item.Product?.Description ?? string.Empty,
                    UnitPrice = item.UnitPrice,
                    Quantity = item.Quantity,
                    LineTotal = item.LineTotal
                }).ToList()
            };

        }
    }
}
