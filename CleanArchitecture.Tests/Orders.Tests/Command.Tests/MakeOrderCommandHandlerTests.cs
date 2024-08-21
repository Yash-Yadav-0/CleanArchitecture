using CleanArchitecture.Application.Dtos;
using CleanArchitecture.Application.Features.Orders.Comments.MakeOrder;
using CleanArchitecture.Application.Interfaces.AutoMapper;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Persistence.Context;
using CleanArchitecture.Persistence.UnitOfWorks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Security.Claims;

namespace CleanArchitecture.Tests.Orders.Tests.Command.Tests
{
    public class MakeOrderCommandHandlerTests
    {
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private readonly MakeOrderCommandHandler _handler;
        private readonly ApplicationDbContext _dbContext;

        public MakeOrderCommandHandlerTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_MakeOrder")
                .Options;

            _dbContext = new ApplicationDbContext(options);
            _mapperMock = new Mock<IMapper>();
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();

            var httpContext = new DefaultHttpContext();
            httpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString())
            }));

            _httpContextAccessorMock.Setup(h => h.HttpContext).Returns(httpContext);

            _handler = new MakeOrderCommandHandler(
                new UnitOfWork(_dbContext),
                _mapperMock.Object,
                _httpContextAccessorMock.Object
            );
        }

        [Fact]
        public async Task Handle_ValidRequest_ShouldCreateOrder()
        {
            // Arrange
            var product = new Product
            {
                Id = 1,
                Title = "Sample Product",
                Description = "Sample Description",
                Price = 10.0m,
                Discount = 0.0m,
                BrandId = 1
            };
            _dbContext.products.Add(product);
            await _dbContext.SaveChangesAsync();

            var request = new MakeOrderCommandRequest
            {
                makeOrderDTOs = new List<MakeOrderDTO>
                {
                    new MakeOrderDTO
                    {
                        ProductCount = 2,
                        ProductId = 1,
                    }
                }
            };

            // Act
            await _handler.Handle(request, CancellationToken.None);

            // Assert
            var order = await _dbContext.orders.FirstOrDefaultAsync();
            Assert.NotNull(order);
            Assert.Equal(20.0m, order.TotalAmount);
            Assert.Equal(Domain.Enums.OrderType.Received, order.OrderType);

            var productsOrders = await _dbContext.orderProducts.ToListAsync();
            Assert.Single(productsOrders);
            Assert.Equal(1, productsOrders[0].ProductId);
        }

        [Fact]
        public async Task Handle_InvalidProduct_ShouldNotCreateOrder()
        {
            // Arrange
            // Clear database to ensure no residual data
            _dbContext.orders.RemoveRange(_dbContext.orders);
            _dbContext.orderProducts.RemoveRange(_dbContext.orderProducts);

            var product = new Product
            {
                Id = 3,
                Title = "Sample Product 1",
                Description = "Sample Description 1",
                Price = 10.0m,
                Discount = 0.0m,
                BrandId = 1
            };
            _dbContext.products.Add(product);
            await _dbContext.SaveChangesAsync();

            var request = new MakeOrderCommandRequest
            {
                makeOrderDTOs = new List<MakeOrderDTO>
        {
            new MakeOrderDTO { ProductId = 999, ProductCount = 2 } // Non-existing product
        }
            };

            // Act
            await _handler.Handle(request, CancellationToken.None);

            // Assert
            var order = await _dbContext.orders.FirstOrDefaultAsync();
            Assert.Null(order);

            var productsOrders = await _dbContext.orderProducts.ToListAsync();
            Assert.Empty(productsOrders);
        }


        [Fact]
        public async Task Handle_NoValidProducts_ShouldReturnUnit()
        {
            // Arrange
            var request = new MakeOrderCommandRequest
            {
                makeOrderDTOs = new List<MakeOrderDTO>
                {
                    new MakeOrderDTO { ProductId = 999, ProductCount = 2 } // Non-existing product
                }
            };

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.Equal(Unit.Value, result);
        }
    }
}
