using CleanArchitecture.Application.Features.Orders.Command.UpdateOrder;
using CleanArchitecture.Application.Interfaces.AutoMapper;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Application.Features.Orders.Rules;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Moq;
using CleanArchitecture.Application.Dtos;
using CleanArchitecture.Persistence.Context;
using CleanArchitecture.Persistence.UnitOfWorks;
using CleanArchitecture.Application.Features.Orders.Exceptions;

namespace CleanArchitecture.Tests.Orders.Tests.Command.Tests
{
    public class UpdateOrderCommentHandlerTests : IDisposable
    {
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private readonly Mock<IOrderRules> _orderRulesMock;
        private readonly UpdateOrderCommentHandler _handler;
        private readonly DbContextOptions<ApplicationDbContext> _options;

        public UpdateOrderCommentHandlerTests()
        {
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique database for each test
                .Options;

            _mapperMock = new Mock<IMapper>();
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            _orderRulesMock = new Mock<IOrderRules>();

            var httpContext = new DefaultHttpContext();
            httpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
            new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString())
        }, "mock"));

            _httpContextAccessorMock.Setup(h => h.HttpContext).Returns(httpContext);

            _handler = new UpdateOrderCommentHandler(
                new UnitOfWork(new ApplicationDbContext(_options)),
                _mapperMock.Object,
                _httpContextAccessorMock.Object,
                _orderRulesMock.Object
            );
        }

        public void Dispose()
        {
            // Dispose of resources if necessary
        }

        [Fact]
        public async Task Handle_ValidRequest_ShouldUpdateOrder()
        {
            using (var dbContext = new ApplicationDbContext(_options))
            {
                // Arrange
                var userId = Guid.NewGuid();
                var oldOrder = new Order
                {
                    Id = 1,
                    UserId = userId,
                    OrderType = Domain.Enums.OrderType.Received,
                    TotalAmount = 0m,
                    AddedOnDate = DateTime.UtcNow
                };
                dbContext.orders.Add(oldOrder);
                await dbContext.SaveChangesAsync();

                var product = new Product
                {
                    Id = 1,
                    Title = "Sample Product",
                    Description = "Sample Description",
                    Price = 10.0m,
                    Discount = 0.0m,
                    BrandId = 1
                };
                dbContext.products.Add(product);
                await dbContext.SaveChangesAsync();

                var request = new UpdateOrderCommandRequest
                {
                    Id = oldOrder.Id,
                    makeOrderDTOs = new List<MakeOrderDTO>
                {
                    new MakeOrderDTO
                    {
                        ProductCount = 2,
                        ProductId = product.Id
                    }
                }
                };

                _orderRulesMock.Setup(r => r.TheOrderShouldBeExist(It.IsAny<Order>())).Returns(Task.CompletedTask);
                _orderRulesMock.Setup(r => r.TheSameUserForTheSameOrder(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(Task.CompletedTask);

                // Act
                await _handler.Handle(request, CancellationToken.None);

                // Assert
                var updatedOrder = await dbContext.orders.AsNoTracking().FirstOrDefaultAsync(o => o.Id == oldOrder.Id);
                Assert.NotNull(updatedOrder);
                Assert.Equal(20.0m, updatedOrder.TotalAmount);
                Assert.Equal(Domain.Enums.OrderType.Received, updatedOrder.OrderType);

                var productsOrders = await dbContext.orderProducts.AsNoTracking().ToListAsync();
                Assert.Single(productsOrders);
                Assert.Equal(product.Id, productsOrders[0].ProductId);
            }
        }

        [Fact]
        public async Task Handle_InvalidProduct_ShouldNotUpdateOrder()
        {
            // Arrange: Create a unique in-memory database for this test
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique database for each test
                .Options;

            // Create a new DbContext for setup
            using (var dbContext = new ApplicationDbContext(options))
            {
                var userId = Guid.NewGuid();
                var oldOrder = new Order
                {
                    Id = 1,
                    UserId = userId,
                    OrderType = Domain.Enums.OrderType.Received,
                    TotalAmount = 0m,
                    AddedOnDate = DateTime.UtcNow
                };
                dbContext.orders.Add(oldOrder);
                await dbContext.SaveChangesAsync();
            }

            // Act: Use the same DbContext for the handler
            using (var dbContext = new ApplicationDbContext(options))
            {
                // Recreate the handler with the correct DbContext
                var handler = new UpdateOrderCommentHandler(
                    new UnitOfWork(dbContext),
                    _mapperMock.Object,
                    _httpContextAccessorMock.Object,
                    _orderRulesMock.Object
                );

                var request = new UpdateOrderCommandRequest
                {
                    Id = 1,
                    makeOrderDTOs = new List<MakeOrderDTO>
            {
                new MakeOrderDTO { ProductId = 999, ProductCount = 2 } // Non-existing product
            }
                };

                _orderRulesMock.Setup(r => r.TheOrderShouldBeExist(It.IsAny<Order>())).Returns(Task.CompletedTask);
                _orderRulesMock.Setup(r => r.TheSameUserForTheSameOrder(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(Task.CompletedTask);

                // Act: Handle the request
                await handler.Handle(request, CancellationToken.None);

                // Assert: Verify that the order is not updated and no invalid products are present
                var updatedOrder = await dbContext.orders.AsNoTracking().FirstOrDefaultAsync(o => o.Id == 1);
                Assert.NotNull(updatedOrder);

                var productsOrders = await dbContext.orderProducts.AsNoTracking().ToListAsync();
                Assert.Empty(productsOrders);
            }
        }


        [Fact]
        public async Task Handle_UserMismatch_ShouldThrowException()
        {
            using (var dbContext = new ApplicationDbContext(_options))
            {
                // Arrange
                var userId = Guid.NewGuid();
                var otherUserId = Guid.NewGuid();
                var oldOrder = new Order
                {
                    Id = 1,
                    UserId = otherUserId,
                    OrderType = Domain.Enums.OrderType.Received,
                    TotalAmount = 0m,
                    AddedOnDate = DateTime.UtcNow
                };
                dbContext.orders.Add(oldOrder);
                await dbContext.SaveChangesAsync();

                var request = new UpdateOrderCommandRequest
                {
                    Id = oldOrder.Id,
                    makeOrderDTOs = new List<MakeOrderDTO>
                {
                    new MakeOrderDTO { ProductId = 1, ProductCount = 2 }
                }
                };

                _orderRulesMock.Setup(r => r.TheOrderShouldBeExist(It.IsAny<Order>())).Returns(Task.CompletedTask);
                _orderRulesMock.Setup(r => r.TheSameUserForTheSameOrder(It.IsAny<Guid>(), It.IsAny<Guid>())).ThrowsAsync(new TheSameUserForTheSameOrderException("User mismatch"));

                // Act & Assert
                await Assert.ThrowsAsync<TheSameUserForTheSameOrderException>(() => _handler.Handle(request, CancellationToken.None));
            }
        }

        [Fact]
        public async Task Handle_OrderNotFound_ShouldThrowException()
        {
            using (var dbContext = new ApplicationDbContext(_options))
            {
                // Arrange
                var userId = Guid.NewGuid();
                var request = new UpdateOrderCommandRequest
                {
                    Id = 999, // Non-existing order ID
                    makeOrderDTOs = new List<MakeOrderDTO>
                {
                    new MakeOrderDTO { ProductId = 1, ProductCount = 2 }
                }
                };

                _orderRulesMock.Setup(r => r.TheOrderShouldBeExist(It.IsAny<Order>())).ThrowsAsync(new Exception("Order not found"));

                // Act & Assert
                await Assert.ThrowsAsync<Exception>(() => _handler.Handle(request, CancellationToken.None));
            }
        }
    }
}
