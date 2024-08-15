using CleanArchitecture.Application.Features.Products.Commands.UpdateProduct;
using CleanArchitecture.Application.Interfaces.AutoMapper;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Persistence.Context;
using CleanArchitecture.Persistence.UnitOfWorks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Security.Claims;


namespace CleanArchitecture.Tests.Products.Tests.Command.Tests
{
    public class UpdateProductCommandHandlerTests
    {
        private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private readonly Mock<UserManager<User>> _userManagerMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly ApplicationDbContext _dbContext;
        private readonly UpdateProductCommandHandler _handler;
        private readonly Guid _userId;
        private readonly Mock<RoleManager<Role>> _roleManagerMock;

        public UpdateProductCommandHandlerTests()
        {
            _userId = Guid.NewGuid();
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                // Unique database for each test
                .Options;

            _dbContext = new ApplicationDbContext(dbContextOptions);

            _mapperMock = new Mock<IMapper>();
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            _userManagerMock = new Mock<UserManager<User>>(
                Mock.Of<IUserStore<User>>(),
                null, null, null, null, null, null, null, null
            );
            _roleManagerMock = new Mock<RoleManager<Role>>(
                Mock.Of<IRoleStore<Role>>(),
                null, null, null, null
            );

            var httpContext = new DefaultHttpContext();
            httpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, _userId.ToString()),
                new Claim(ClaimTypes.Role, "Admin") // Set role directly in claims
            }));
            _httpContextAccessorMock.Setup(h => h.HttpContext).Returns(httpContext);

            _handler = new UpdateProductCommandHandler(
                new UnitOfWork(_dbContext),
                _mapperMock.Object,
                _httpContextAccessorMock.Object,
                _userManagerMock.Object
            );
        }

        [Fact]
        public async Task Handle_ValidRequest_Success()
        {
            // Arrange
            var productId = 22;
            var userId = _userId;
            var product = new Product
            {
                Id = productId,
                Title = "Original Title",
                Description = "Original Description",
                Price = 50.00m,
                Discount = 5.00m,
                BrandId = 1,
                AddedOnDate = DateTime.UtcNow,
            };
            var role = new Role
            {
                Name = "Admin",
                Id = Guid.NewGuid(),
                NormalizedName = "ADMIN",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };
            var user = new User
            {
                Id = userId,
                Email = "test@gmail.com",
                EmailConfirmed = true,
                UserName = "testing admin",
                FullName = "test admin"
            };

            _dbContext.products.Add(product);
            _dbContext.Roles.Add(role);
            _dbContext.Users.Add(user);
            _dbContext.UserRoles.Add(new IdentityUserRole<Guid> { UserId = userId, RoleId = role.Id });
            await _dbContext.SaveChangesAsync();

            

            _userManagerMock.Setup(u => u.FindByIdAsync(userId.ToString())).ReturnsAsync(user);
            _userManagerMock.Setup(u => u.IsInRoleAsync(user, "Admin")).ReturnsAsync(true);
            _roleManagerMock.Setup(r => r.RoleExistsAsync(It.IsAny<string>())).ReturnsAsync(true);
            _userManagerMock.Setup(u => u.AddToRoleAsync(user, role.Name)).ReturnsAsync(IdentityResult.Success);

            var request = new UpdateProductCommandRequest
            {
                Id = productId,
                Title = "Updated Title",
                Description = "Updated Description",
                Price = 100.00m,
                Discount = 10.00m,
                BrandId = 2,
                CategortIds = [3,4]
            };

            if (await _dbContext.SaveChangesAsync() > 0)
            {
                foreach (var categoryId in request.CategortIds)
                {
                    await _dbContext.AddAsync(new ProductsCategories
                    {
                        CategoryId = categoryId,
                        ProductId = product.Id
                    });
                }
            }
            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.Equal(Unit.Value, result);

            var updatedProduct = _dbContext.products.FirstOrDefault(p => p.Id == productId);
            Assert.NotNull(updatedProduct);
            Assert.Equal("Updated Title", updatedProduct.Title);
            Assert.Equal("Updated Description", updatedProduct.Description);
            Assert.Equal(100.00m, updatedProduct.Price);
            Assert.Equal(10.00m, updatedProduct.Discount);
            Assert.Equal(2, updatedProduct.BrandId);
            Assert.Equal(2, _dbContext.productsCategories.Count(pc => pc.ProductId == productId));
        }

        [Fact]
        public async Task Handle_UserNotAuthenticated_ThrowsUnauthorizedAccessException()
        {
            // Arrange
            var productId = 33;
            var request = new UpdateProductCommandRequest
            {
                Id = productId,
                Title = "Updated Title",
                Description = "Updated Description",
                Price = 100.00m,
                Discount = 10.00m,
                BrandId = 2,
                CategortIds = new List<int> { 3, 4 }
            };

            _httpContextAccessorMock.Setup(h => h.HttpContext).Returns(new DefaultHttpContext());

            // Act & Assert
            await Assert.ThrowsAsync<UnauthorizedAccessException>(() => _handler.Handle(request, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_UserNotInAdminRole_ThrowsUnauthorizedAccessException()
        {
            // Arrange
            var productId = 44;
            var user = new User { Id = _userId, FullName = "Testing test" };

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            _httpContextAccessorMock.Setup(h => h.HttpContext).Returns(new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, _userId.ToString())
                }))
            });

            _userManagerMock.Setup(u => u.FindByIdAsync(_userId.ToString())).ReturnsAsync(user);
            _userManagerMock.Setup(u => u.IsInRoleAsync(user, "Admin")).ReturnsAsync(false);

            var request = new UpdateProductCommandRequest
            {
                Id = productId,
                Title = "Updated Title",
                Description = "Updated Description",
                Price = 100.00m,
                Discount = 10.00m,
                BrandId = 2,
                CategortIds = new List<int> { 3, 4 }
            };

            // Act & Assert
            await Assert.ThrowsAsync<UnauthorizedAccessException>(() => _handler.Handle(request, CancellationToken.None));
        }
    }
}
