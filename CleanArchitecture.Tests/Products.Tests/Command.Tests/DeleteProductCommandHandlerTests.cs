using CleanArchitecture.Application.Features.Products.Commands.DeleteProduct;
using CleanArchitecture.Application.Features.Products.Rules;
using CleanArchitecture.Application.Interfaces.AutoMapper;
using CleanArchitecture.Application.Interfaces.Storage;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Persistence.Context;
using CleanArchitecture.Persistence.UnitOfWorks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using SendGrid.Helpers.Errors.Model;
using System.Security.Claims;

namespace CleanArchitecture.Tests.Products.Tests.Command.Tests
{
    public class DeleteProductCommandHandlerTests
    {
        private readonly Mock<IHttpContextAccessor> _contextAccessorMock;
        private readonly Mock<UserManager<User>> _userManagerMock;
        private readonly Mock<RoleManager<Role>> _roleManagerMock;
        private readonly ApplicationDbContext _dbContext;
        private readonly Mock<IProductRules> _productRulesMock;
        private readonly Mock<ILocalStorage> _locakStorageMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Guid _userID;
        private readonly DeleteProductCommandHandler _handler;

        public DeleteProductCommandHandlerTests()
        {
            _userID = Guid.NewGuid();
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _dbContext = new ApplicationDbContext(dbContextOptions);

            _productRulesMock = new Mock<IProductRules>();
            _mapperMock = new Mock<IMapper>();

            _contextAccessorMock = new Mock<IHttpContextAccessor>();
            var httpContext = new DefaultHttpContext();
            httpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier,_userID.ToString()),
                new Claim(ClaimTypes.Role,"Admin")
            }));
            _contextAccessorMock.Setup(h => h.HttpContext).Returns(httpContext);

            _locakStorageMock = new Mock<ILocalStorage>();
            _userManagerMock = new Mock<UserManager<User>>(
                Mock.Of<IUserStore<User>>(),
                null, null, null, null, null, null, null, null
            );
            _roleManagerMock = new Mock<RoleManager<Role>>(
                Mock.Of<IRoleStore<Role>>(),
                null, null, null, null
            );

            _handler = new DeleteProductCommandHandler(
                new UnitOfWork(_dbContext),
                _mapperMock.Object,
                _contextAccessorMock.Object,
                _userManagerMock.Object,
                _roleManagerMock.Object
            );
        }
        [Fact]
        public async Task Handle_ValidRequest_Success()
        {
            //Arrange

            var userId = _userID;
            var role = new Role
            {
                Name = "Admin",
                Id = Guid.NewGuid(),
                NormalizedName = "ADMIN",
                ConcurrencyStamp = Guid.NewGuid().ToString(),
            };
            var user = new User
            {
                Id = userId,
                Email = "testadmin@gmail.com",
                EmailConfirmed = true,
                UserName = "test admin",
                FullName = "testing admin Fn",
            };

            var product = new Product
            {
                Id = 22,
                Title = "Original Title",
                Description = "Original Description",
                Price = 50.00m,
                Discount = 5.00m,
                BrandId = 1,
                AddedOnDate = DateTime.UtcNow,
            };

            _dbContext.Roles.Add( role );
            _dbContext.Users.Add( user );
            _dbContext.products.Add( product );
            _dbContext.UserRoles.Add(new IdentityUserRole<Guid> { UserId = userId , RoleId = role.Id});

            await _dbContext.SaveChangesAsync();

            _userManagerMock.Setup(u => u.FindByIdAsync(userId.ToString())).ReturnsAsync(user);
            _userManagerMock.Setup(u => u.IsInRoleAsync(user, "Admin")).ReturnsAsync(true);
            _roleManagerMock.Setup(r => r.RoleExistsAsync(It.IsAny<string>())).ReturnsAsync(true);
            _userManagerMock.Setup(u => u.AddToRoleAsync(user, role.Name)).ReturnsAsync(IdentityResult.Success);

            var request = new DeleteProductCommandRequest
            {
                Id = 22,
            };

            //Act
            var result = await _handler.Handle(request,CancellationToken.None);

            //Assert
            Assert.Equal(Unit.Value,result);
            Assert.NotNull(product);
        }
        [Fact]
        public async Task Handle_InvalidValidRequest_Returns_ProductNotFound()
        {
            //Arrange

            var userId = _userID;
            var role = new Role
            {
                Name = "Admin",
                Id = Guid.NewGuid(),
                NormalizedName = "ADMIN",
                ConcurrencyStamp = Guid.NewGuid().ToString(),
            };
            var user = new User
            {
                Id = userId,
                Email = "testadmin@gmail.com",
                EmailConfirmed = true,
                UserName = "test admin",
                FullName = "testing admin Fn",
            };

            var product = new Product
            {
                Id = 22,
                Title = "Original Title",
                Description = "Original Description",
                Price = 50.00m,
                Discount = 5.00m,
                BrandId = 1,
                AddedOnDate = DateTime.UtcNow,
            };

            _dbContext.Roles.Add(role);
            _dbContext.Users.Add(user);
            _dbContext.products.Add(product);
            _dbContext.UserRoles.Add(new IdentityUserRole<Guid> { UserId = userId, RoleId = role.Id });

            await _dbContext.SaveChangesAsync();

            _userManagerMock.Setup(u => u.FindByIdAsync(userId.ToString())).ReturnsAsync(user);
            _userManagerMock.Setup(u => u.IsInRoleAsync(user, "Admin")).ReturnsAsync(true);
            _roleManagerMock.Setup(r => r.RoleExistsAsync(It.IsAny<string>())).ReturnsAsync(true);
            _userManagerMock.Setup(u => u.AddToRoleAsync(user, role.Name)).ReturnsAsync(IdentityResult.Success);

            var request = new DeleteProductCommandRequest
            {
                Id = 42,
            };

            //Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(request, CancellationToken.None));
        }
    }
}
