using CleanArchitecture.Application.Dtos;
using CleanArchitecture.Application.Features.Orders.Command.UpdateOrder;
using CleanArchitecture.Application.Features.Orders.Exceptions;
using CleanArchitecture.Application.Features.Orders.Rules;
using CleanArchitecture.Application.Interfaces.AutoMapper;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Persistence.Context;
using CleanArchitecture.Persistence.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Security.Claims;

namespace CleanArchitecture.Tests.Orders.Tests.Command.Tests
{
    public class UpdateOrderCommandHandlerTests
    {
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private readonly UpdateOrderCommentHandler _handler;
        private readonly ApplicationDbContext _dbContext;
        private readonly Mock<OrderRules> _orderRulesMock;

        public UpdateOrderCommandHandlerTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_UpdateOrder")
                .Options;

            _dbContext = new ApplicationDbContext(options);
            _mapperMock = new Mock<IMapper>();
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            _orderRulesMock = new Mock<OrderRules>();

            var httpContext = new DefaultHttpContext();
            httpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString())
            }, "mock"));

            _httpContextAccessorMock.Setup(h => h.HttpContext).Returns(httpContext);

            //_orderRulesMock.Setup(r => r.TheSameUserForTheSameOrder(It.IsAny<Guid>(), It.IsAny<Guid>())).Verifiable();

            _handler = new UpdateOrderCommentHandler(
                new UnitOfWork(_dbContext),
                _mapperMock.Object,
                _httpContextAccessorMock.Object,
                _orderRulesMock.Object
            );
        }

        [Fact]
        public async Task Handle_ValidRequest_ShouldUpdateOrder()
        {
            // Arrange
            var validUserId = Guid.NewGuid(); // Correctly generate a new GUID

            _dbContext.orders.RemoveRange(_dbContext.orders);
            _dbContext.orderProducts.RemoveRange(_dbContext.orderProducts);

            var existingOrder = new Order
            {
                Id = 1,
                UserId = validUserId,
                OrderType = Domain.Enums.OrderType.Received,
                TotalAmount = 20.0m,
                AddedOnDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };

            var existingProduct = new Product
            {
                Id = 1,
                Title = "Sample Product",
                Description = " sample product description",
                Price = 10.0m,
                Discount = 0.0m,
                BrandId = 1
            };

            _dbContext.orders.Add(existingOrder);
            _dbContext.products.Add(existingProduct);
            await _dbContext.SaveChangesAsync();

            var request = new UpdateOrderCommandRequest
            {
                Id = 1,
                makeOrderDTOs = new List<MakeOrderDTO>
            {
                new MakeOrderDTO { ProductId = 1, ProductCount = 3 }
            }
            };

            // Act
            await _handler.Handle(request, CancellationToken.None);

            // Assert
            var updatedOrder = await _dbContext.orders.FirstOrDefaultAsync(o => o.Id == 1);
            Assert.NotNull(updatedOrder);
            Assert.Equal(30.0m, updatedOrder.TotalAmount); // Expecting updated total amount

            var productsOrders = await _dbContext.orderProducts.ToListAsync();
            Assert.Single(productsOrders);
            Assert.Equal(1, productsOrders[0].ProductId);
        }

        [Fact]
        public async Task Handle_InvalidProduct_ShouldNotUpdateOrder()
        {
            // Arrange
            var validUserId = Guid.NewGuid(); // Correctly generate a new GUID

            _dbContext.orders.RemoveRange(_dbContext.orders);
            _dbContext.orderProducts.RemoveRange(_dbContext.orderProducts);
            
            var existingOrder = new Order
            {
                Id = 1,
                UserId = validUserId,
                OrderType = Domain.Enums.OrderType.Received,
                TotalAmount = 20.0m,
                AddedOnDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };

            _dbContext.orders.Add(existingOrder);
            await _dbContext.SaveChangesAsync();

            var request = new UpdateOrderCommandRequest
            {
                Id = 1,
                makeOrderDTOs = new List<MakeOrderDTO>
            {
                new MakeOrderDTO { ProductId = 999, ProductCount = 2 } // Non-existing product ID
            }
            };

            // Act
            await _handler.Handle(request, CancellationToken.None);

            // Assert
            var updatedOrder = await _dbContext.orders.FirstOrDefaultAsync(o => o.Id == 1);
            Assert.NotNull(updatedOrder); // Ensure the order still exists
            Assert.Equal(20.0m, updatedOrder.TotalAmount); // Ensure the total amount is unchanged

            var productsOrders = await _dbContext.orderProducts.ToListAsync();
            Assert.Empty(productsOrders); // Ensure no products were added to the order
        }

        [Fact]
        public async Task Handle_UserMismatch_ShouldThrowException()
        {
            // Arrange
            var validUserId = Guid.NewGuid(); // Correctly generate a new GUID

            _dbContext.orders.RemoveRange(_dbContext.orders);
            _dbContext.orderProducts.RemoveRange(_dbContext.orderProducts);

            var existingOrder = new Order
            {
                Id = 1,
                UserId = Guid.NewGuid(), // Different user ID
                OrderType = Domain.Enums.OrderType.Received,
                TotalAmount = 20.0m,
                AddedOnDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };

            _dbContext.orders.Add(existingOrder);
            await _dbContext.SaveChangesAsync();

            var request = new UpdateOrderCommandRequest
            {
                Id = 1,
                makeOrderDTOs = new List<MakeOrderDTO>
            {
                new MakeOrderDTO { ProductId = 1, ProductCount = 2 }
            }
            };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<TheSameUserForTheSameOrderException>(() => _handler.Handle(request, CancellationToken.None));
            Assert.Equal("The Same User should be For The Same Order", exception.Message);
        }

        [Fact]
        public async Task Handle_OrderNotFound_ShouldThrowException()
        {
            // Arrange
            var request = new UpdateOrderCommandRequest
            {
                Id = 999, // Non-existing order ID
                makeOrderDTOs = new List<MakeOrderDTO>
                {
                    new MakeOrderDTO { ProductId = 1, ProductCount = 2 }
                }
            };

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _handler.Handle(request, CancellationToken.None));
        }
    }
}
