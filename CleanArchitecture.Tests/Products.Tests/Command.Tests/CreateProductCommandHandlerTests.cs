using CleanArchitecture.Application.Features.Products.Commands.CreateProduct;
using CleanArchitecture.Application.Features.Products.Rules;
using CleanArchitecture.Application.Interfaces.AutoMapper;
using CleanArchitecture.Application.Interfaces.Storage;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Persistence.Context;
using CleanArchitecture.Persistence.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using MediatR;
using Moq;


namespace CleanArchitecture.Application.Tests.Features.Products.Commands
{
    public class CreateProductCommandHandlerTests
    {
        private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private readonly Mock<UserManager<User>> _userManagerMock;
        private readonly Mock<RoleManager<Role>> _roleManagerMock;
        private readonly Mock<ILocalStorage> _localStorageMock;
        private readonly Mock<IProductRules> _productRulesMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly ApplicationDbContext _dbContext;
        private readonly CreateProductCommandHandler _handler;
        private readonly Guid _userID;

        public CreateProductCommandHandlerTests()
        {
            _userID = Guid.NewGuid();
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Unique database for each test
                .Options;

            _dbContext = new ApplicationDbContext(dbContextOptions);

            _productRulesMock = new Mock<IProductRules>();
            _mapperMock = new Mock<IMapper>();

            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            var httpContext = new DefaultHttpContext();
            httpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, _userID.ToString()),
                new Claim(ClaimTypes.Role, "Admin") // Set role directly in claims
            }));
            _httpContextAccessorMock.Setup(h => h.HttpContext).Returns(httpContext);

            _localStorageMock = new Mock<ILocalStorage>();
            _userManagerMock = new Mock<UserManager<User>>(
                Mock.Of<IUserStore<User>>(),
                null, null, null, null, null, null, null, null
            );
            _roleManagerMock = new Mock<RoleManager<Role>>(
                Mock.Of<IRoleStore<Role>>(),
                null, null, null, null
            );

            _handler = new CreateProductCommandHandler(
                new UnitOfWork(_dbContext),
                _productRulesMock.Object,
                _mapperMock.Object,
                _httpContextAccessorMock.Object,
                _userManagerMock.Object,
                _localStorageMock.Object,
                _roleManagerMock.Object
            );
        }

        [Fact]
        public async Task Handle_ValidRequest_Success()
        {
            // Arrange
            var userId = _userID;
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

            _dbContext.Roles.Add(role);
            _dbContext.Users.Add(user);
            _dbContext.UserRoles.Add(new IdentityUserRole<Guid> { UserId = userId, RoleId = role.Id });
            await _dbContext.SaveChangesAsync();

            _userManagerMock.Setup(u => u.FindByIdAsync(userId.ToString())).ReturnsAsync(user);
            _userManagerMock.Setup(u => u.IsInRoleAsync(user, "Admin")).ReturnsAsync(true);
            _roleManagerMock.Setup(r => r.RoleExistsAsync(It.IsAny<string>())).ReturnsAsync(true);
            _userManagerMock.Setup(u => u.AddToRoleAsync(user, role.Name)).ReturnsAsync(IdentityResult.Success);


            var request = new CreateProductCommandRequest
            {
                Title = "Test Product",
                Description = "Product Description",
                Price = 100.00m,
                Discount = 10.00m,
                BrandId = 2,
                CategortIds = new List<int> { 1, 2 },
                Images = null
            };

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.Equal(Unit.Value, result);

            var product = _dbContext.products.FirstOrDefault(p => p.Title == "Test Product");
            Assert.NotNull(product);
        }

        [Fact]
        public async Task Handle_UserNotInAdminRole_ThrowsUnauthorizedAccessException()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = new User { Id = userId, FullName = "Testing test" };

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            _httpContextAccessorMock.Setup(h => h.HttpContext).Returns(new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString())
            }))
            });

            _userManagerMock.Setup(u => u.FindByIdAsync(userId.ToString())).ReturnsAsync(user);
            _userManagerMock.Setup(u => u.IsInRoleAsync(user, "Admin")).ReturnsAsync(false);

            var request = new CreateProductCommandRequest
            {
                Title = "Test Product",
                Description = "Product Description",
                Price = 100.00m,
                Discount = 10.00m,
                BrandId = 2,
                CategortIds = new List<int> { 1, 2 },
                Images = null
            };

            // Act & Assert
            await Assert.ThrowsAsync<UnauthorizedAccessException>(() => _handler.Handle(request, CancellationToken.None));
        }
    }
}
