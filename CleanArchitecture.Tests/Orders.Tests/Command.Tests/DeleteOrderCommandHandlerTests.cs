using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.Features.Orders.Comments.DeleteOrder;
using CleanArchitecture.Application.Interfaces.UnitOfWorks;
using CleanArchitecture.Application.Interfaces.AutoMapper;
using CleanArchitecture.Application.Features.Orders.Rules;
using CleanArchitecture.Application.Helpers;
using CleanArchitecture.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using CleanArchitecture.Persistence.Context;
using CleanArchitecture.Persistence.UnitOfWorks;

namespace CleanArchitecture.Tests.Orders.Tests.Command.Tests
{
    public class DeleteOrderCommandHandlerTests
    {
        private readonly Mock<IOrderRules> _orderRulesMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private readonly DeleteOrderCommandHandler _handler;
        private readonly DbContextOptions<ApplicationDbContext> _options;

        public DeleteOrderCommandHandlerTests()
        {
            _orderRulesMock = new Mock<IOrderRules>();
            _mapperMock = new Mock<IMapper>();
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();

            // Setup HttpContextAccessor to return a mock user ID
            var httpContext = new Mock<HttpContext>();
            var user = new Mock<ClaimsPrincipal>();
            user.Setup(u => u.FindFirst(ClaimTypes.NameIdentifier)).Returns(new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()));
            httpContext.Setup(c => c.User).Returns(user.Object);
            _httpContextAccessorMock.Setup(a => a.HttpContext).Returns(httpContext.Object);

            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Unique database for each test
                .Options;

            _handler = new DeleteOrderCommandHandler(
                new UnitOfWork(new ApplicationDbContext(_options)),
                _mapperMock.Object,
                _httpContextAccessorMock.Object,
                _orderRulesMock.Object
            );
        }

        [Fact]
        public async Task Handle_OrderExists_ShouldMarkOrderAsDeleted()
        {
            // Arrange
            using (var dbContext = new ApplicationDbContext(_options))
            {
                var userId = Guid.NewGuid();
                var order = new Order
                {
                    Id = 1,
                    UserId = userId,
                    OrderType = Domain.Enums.OrderType.Received,
                    TotalAmount = 100m,
                    AddedOnDate = DateTime.UtcNow
                };
                dbContext.orders.Add(order);
                await dbContext.SaveChangesAsync();
            }

            var request = new DeleteOrderCommandRequest { Id = 1 };

            _orderRulesMock.Setup(r => r.TheOrderShouldBeExist(It.IsAny<Order>())).Returns(Task.CompletedTask);

            //_orderRulesMock.Setup(r => r.TheSameUserForTheSameOrder(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(Task.CompletedTask);
            _orderRulesMock.Setup(r => r.TheSameUserForTheSameOrder(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(Task.CompletedTask);

            // Act
            await _handler.Handle(request, CancellationToken.None);

            // Assert
            using (var dbContext = new ApplicationDbContext(_options))
            {
                var updatedOrder = await dbContext.orders.AsNoTracking().FirstOrDefaultAsync(o => o.Id == 1);
                Assert.NotNull(updatedOrder);
                Assert.True(updatedOrder.IsDeleted);
                Assert.NotNull(updatedOrder.DeletedDate);
            }
        }

        [Fact]
        public async Task Handle_OrderDoesNotExist_ShouldThrowException()
        {
            // Arrange
            var request = new DeleteOrderCommandRequest { Id = 999 }; // Non-existing order ID

            _orderRulesMock.Setup(r => r.TheOrderShouldBeExist(null))
                .ThrowsAsync(new Exception("Order not found"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(request, CancellationToken.None));
            Assert.Equal("Order not found", exception.Message);
        }
    }
}
