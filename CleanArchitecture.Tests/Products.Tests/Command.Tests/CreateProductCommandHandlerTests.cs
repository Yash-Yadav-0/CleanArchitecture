using CleanArchitecture.Application.Features.Products.Commands.CreateProduct;
using CleanArchitecture.Application.Features.Products.Exceptions;
using CleanArchitecture.Application.Interfaces.AutoMapper;
using CleanArchitecture.Application.Interfaces.Storage;
using CleanArchitecture.Application.Features.Products.Rules;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Persistence.Context;
using Moq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using CleanArchitecture.Application.Interfaces.Repositories;
using CleanArchitecture.Persistence.UnitOfWorks;

namespace CleanArchitecture.Tests.Products.Tests.Command.Tests
{
    public class CreateProductCommandHandlerTests : IDisposable
    {
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private readonly Mock<UserManager<User>> _userManagerMock;
        private readonly Mock<RoleManager<Role>> _roleManagerMock;
        private readonly Mock<ILocalStorage> _localStorageMock;
        private readonly CreateProductCommandHandler _handler;
        private readonly DbContextOptions<ApplicationDbContext> _options;

        public CreateProductCommandHandlerTests()
        {
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _mapperMock = new Mock<IMapper>();
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            _userManagerMock = new Mock<UserManager<User>>(Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
            _roleManagerMock = new Mock<RoleManager<Role>>(Mock.Of<IRoleStore<Role>>(), null, null, null, null);
            _localStorageMock = new Mock<ILocalStorage>();

            var httpContext = new DefaultHttpContext();
            var userId = Guid.NewGuid().ToString();

            httpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId)
            }, "mock"));

            _httpContextAccessorMock.Setup(h => h.HttpContext).Returns(httpContext);

            _handler = new CreateProductCommandHandler(
                new UnitOfWork(new ApplicationDbContext(_options)),
                new ProductRules(),
                _mapperMock.Object,
                _httpContextAccessorMock.Object,
                _userManagerMock.Object,
                _localStorageMock.Object,
                _roleManagerMock.Object
            );
        }

        public void Dispose()
        {
            // Dispose of resources if necessary
        }

        [Fact]
        public async Task Handle_ValidRequest_ShouldCreateProduct()
        {
            using var dbContext = new ApplicationDbContext(_options);
            // Arrange
            var userId = Guid.NewGuid().ToString();
            var request = new CreateProductCommandRequest
            {
                Title = "New Product",
                Description = "Product Description",
                Price = 100.0m,
                Discount = 10.0m,
                BrandId = 1,
                CategortIds = new List<int> { 1, 2 },
                Images = null
            };

            var user = new User
            {
                Id = Guid.Parse(userId),
                UserName = "admin",
                Email = "admin@example.com"
            };
            var role = new Role
            {
                Id = Guid.NewGuid(),
                Name = "ADMIN",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = Guid.NewGuid().ToString(),
            };

            _userManagerMock.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(user);
            _userManagerMock.Setup(rm => rm.AddToRoleAsync(user, "ADMIN"));
            _userManagerMock.Setup(um => um.IsInRoleAsync(user, "ADMIN")).ReturnsAsync(true);

            _roleManagerMock.Setup(rm => rm.RoleExistsAsync("ADMIN")).ReturnsAsync(true);

            var productRepoMock = new Mock<IWriteRepository<Product>>();
            var productCategoryRepoMock = new Mock<IWriteRepository<ProductsCategories>>();
            var imageRepoMock = new Mock<IWriteRepository<Image>>();

            // Act
            await _handler.Handle(request, CancellationToken.None);

            // Assert
            var createdProduct = await dbContext.products.AsNoTracking().FirstOrDefaultAsync(p => p.Title == request.Title);
            Assert.NotNull(createdProduct);
            Assert.Equal(request.Title, createdProduct.Title);
            Assert.Equal(request.Description, createdProduct.Description);
            Assert.Equal(request.Price, createdProduct.Price);
            Assert.Equal(request.Discount, createdProduct.Discount);

            var categories = await dbContext.productsCategories.AsNoTracking().ToListAsync();
            Assert.Equal(request.CategortIds.Count, categories.Count);
        }

        [Fact]
        public async Task Handle_DuplicateTitle_ShouldThrowException()
        {
            using var dbContext = new ApplicationDbContext(_options);
            // Arrange
            var existingProductTitle = "Existing Product";
            var userId = Guid.NewGuid().ToString();
            var existingProduct = new Product
            {
                Title = existingProductTitle,
                Description = "Description",
                Price = 50.0m,
                Discount = 5.0m,
                BrandId = 1
            };
            dbContext.products.Add(existingProduct);
            await dbContext.SaveChangesAsync();

            var request = new CreateProductCommandRequest
            {
                Title = existingProductTitle, // Duplicate title
                Description = "New Product Description",
                Price = 100.0m,
                Discount = 10.0m,
                BrandId = 1,
                CategortIds = new List<int> { 1 },
                Images = null
            };

            var user = new User
            {
                Id = Guid.Parse(userId),
                UserName = "admin",
                Email = "admin@example.com"
            };

            _userManagerMock.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(user);
            _userManagerMock.Setup(um => um.IsInRoleAsync(user, "ADMIN")).ReturnsAsync(true);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ProductsTitleMustNotBeTheSameException>(() => _handler.Handle(request, CancellationToken.None));
            Assert.IsType<ProductsTitleMustNotBeTheSameException>(exception);
        }

        [Fact]
        public async Task Handle_UnauthorizedUser_ShouldThrowException()
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();
            var request = new CreateProductCommandRequest
            {
                Title = "New Product",
                Description = "Product Description",
                Price = 100.0m,
                Discount = 10.0m,
                BrandId = 1,
                CategortIds = new List<int> { 1 },
                Images = null
            };

            _userManagerMock.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync((User)null);
            _userManagerMock.Setup(um => um.IsInRoleAsync(It.IsAny<User>(), "ADMIN")).ReturnsAsync(false);

            // Act & Assert
            await Assert.ThrowsAsync<UnauthorizedAccessException>(() => _handler.Handle(request, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_AuthorizedAdminUser_ShouldCreateProduct()
        {
            using (var dbContext = new ApplicationDbContext(_options))
            {
                // Arrange
                var userId = Guid.NewGuid().ToString();
                var request = new CreateProductCommandRequest
                {
                    Title = "New Product",
                    Description = "Product Description",
                    Price = 100.0m,
                    Discount = 10.0m,
                    BrandId = 1,
                    CategortIds = new List<int> { 1, 2 },
                    Images = null
                };

                // Setup user with ADMIN role
                var user = new User
                {
                    Id = Guid.Parse(userId),
                    UserName = "admin",
                    Email = "admin@example.com"
                };

                _userManagerMock.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(user);
                _userManagerMock.Setup(um => um.IsInRoleAsync(user, "ADMIN")).ReturnsAsync(true);

                _roleManagerMock.Setup(rm => rm.RoleExistsAsync("ADMIN")).ReturnsAsync(true);

                // Act
                await _handler.Handle(request, CancellationToken.None);

                // Assert
                var createdProduct = await dbContext.products.AsNoTracking().FirstOrDefaultAsync(p => p.Title == request.Title);
                Assert.NotNull(createdProduct);
                Assert.Equal(request.Title, createdProduct.Title);
                Assert.Equal(request.Description, createdProduct.Description);
                Assert.Equal(request.Price, createdProduct.Price);
                Assert.Equal(request.Discount, createdProduct.Discount);

                var categories = await dbContext.productsCategories.AsNoTracking().ToListAsync();
                Assert.Equal(request.CategortIds.Count, categories.Count);
            }
        }
    }
}
