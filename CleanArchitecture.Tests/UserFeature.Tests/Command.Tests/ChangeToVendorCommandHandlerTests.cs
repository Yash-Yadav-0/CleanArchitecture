using CleanArchitecture.Application.Features.UserFeature.Commands.ChangeToVendor;
using CleanArchitecture.Application.Features.UserFeature.Rules;
using CleanArchitecture.Application.Interfaces.AutoMapper;
using CleanArchitecture.Application.Interfaces.UnitOfWorks;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Persistence.Context;
using CleanArchitecture.Persistence.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Security.Claims;

namespace CleanArchitecture.Tests.UserFeature.Tests.Command.Tests
{
    //Change User to Vendor
    public class ChangeToVendorCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private readonly Mock<UserManager<User>> _userManagerMock;
        private readonly Mock<RoleManager<Role>> _roleManagerMock;
        private readonly Mock<UserRules> _userRulesMock;
        private readonly ChangeToVendorCommandHandler _handler;
        private readonly ApplicationDbContext _dbContext;
        private readonly Guid _userId;

        public ChangeToVendorCommandHandlerTests()
        {
            _userId = Guid.NewGuid();
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
            _userRulesMock = new Mock<UserRules>();

            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Unique database for each test
                .Options;

            _dbContext = new ApplicationDbContext(dbContextOptions);

            var httpContext = new DefaultHttpContext();
            httpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, _userId.ToString()),
                new Claim(ClaimTypes.Role, "ADMIN") // Set role directly in claims
            }));
            _httpContextAccessorMock.Setup(h => h.HttpContext).Returns(httpContext);

            _handler = new ChangeToVendorCommandHandler(
                new UnitOfWork(_dbContext),
                _userManagerMock.Object,
                _roleManagerMock.Object,
                _mapperMock.Object,
                _userRulesMock.Object,
                _httpContextAccessorMock.Object
            );
        }

        [Fact]
        public async Task Handle_ValidRequest_Success()
        {
            // Arrange
            var vendorRole = new Role
            {
                Name = "Vendor",
                Id = Guid.NewGuid(),
                NormalizedName = "VENDOR",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };
            var adminRole = new Role
            {
                Name = "Admin",
                Id = Guid.NewGuid(),
                NormalizedName = "ADMIN",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };
            var vendor = new User
            {
                Id = Guid.NewGuid(),
                Email = "testvendor@gmail.com",
                EmailConfirmed = true,
                UserName = "vendor user",
                FullName = "Vendor User"
            };
            var adminUser = new User
            {
                Id = _userId,
                Email = "adminuser@gmail.com",
                EmailConfirmed = true,
                UserName = "admin user",
                FullName = "Admin User"
            };

            _dbContext.Roles.Add(vendorRole);
            _dbContext.Roles.Add(adminRole);
            _dbContext.UserRoles.Add(new IdentityUserRole<Guid> { UserId = vendor.Id, RoleId = vendorRole.Id });
            _dbContext.UserRoles.Add(new IdentityUserRole<Guid> { UserId = adminUser.Id, RoleId = adminRole.Id });
            _dbContext.Users.Add(vendor);
            _dbContext.Users.Add(adminUser);
            await _dbContext.SaveChangesAsync();

            //normal user
            _userManagerMock.Setup(u => u.FindByIdAsync(vendor.Id.ToString())).ReturnsAsync(vendor);
            _userManagerMock.Setup(u => u.FindByEmailAsync(vendor.Email)).ReturnsAsync(vendor);
            _userManagerMock.Setup(u => u.IsInRoleAsync(vendor, "Vendor")).ReturnsAsync(true);
            _roleManagerMock.Setup(r => r.RoleExistsAsync(It.IsAny<string>())).ReturnsAsync(true);
            _userManagerMock.Setup(u => u.AddToRoleAsync(vendor, vendorRole.Name)).ReturnsAsync(IdentityResult.Success);
            //admin user
            _userManagerMock.Setup(u => u.FindByIdAsync(adminUser.Id.ToString())).ReturnsAsync(adminUser);
            _userManagerMock.Setup(u => u.IsInRoleAsync(adminUser, "Admin")).ReturnsAsync(true);
            _roleManagerMock.Setup(r => r.RoleExistsAsync(It.IsAny<string>())).ReturnsAsync(true);
            _userManagerMock.Setup(u => u.AddToRoleAsync(adminUser, adminRole.Name)).ReturnsAsync(IdentityResult.Success);

            // Act
            var request = new ChangeToVendorCommandRequest { Email = vendor.Email };
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(vendor.Id.ToString(), result.MessageToReturn);
            _userManagerMock.Verify(u => u.AddToRoleAsync(vendor, "VENDOR"), Times.Once);
            _userManagerMock.Verify(u => u.UpdateAsync(vendor), Times.Once);
        }

        [Fact]
        public async Task Handle_UserNotAdmin_ThrowsUnauthorizedAccessException()
        {
            // Arrange
            var email = "testvendor@gmail.com";
            var user = new User { Id = Guid.NewGuid(), Email = email };

            _userManagerMock.Setup(u => u.FindByIdAsync(_userId.ToString())).ReturnsAsync(null as User);
            _userManagerMock.Setup(u => u.IsInRoleAsync(It.IsAny<User>(), "ADMIN")).ReturnsAsync(false);

            // Act
            var request = new ChangeToVendorCommandRequest { Email = email };

            // Assert
            await Assert.ThrowsAsync<UnauthorizedAccessException>(() => _handler.Handle(request, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_UserNotFound_ThrowsException()
        {
            // Arrange
            var vendorRole = new Role
            {
                Name = "Vendor",
                Id = Guid.NewGuid(),
                NormalizedName = "VENDOR",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };
            var adminRole = new Role
            {
                Name = "Admin",
                Id = Guid.NewGuid(),
                NormalizedName = "ADMIN",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };
            var vendor = new User
            {
                Id = Guid.NewGuid(),
                Email = "testvendor@gmail.com",
                EmailConfirmed = true,
                UserName = "vendor user",
                FullName = "Vendor User"
            };
            var adminUser = new User
            {
                Id = _userId,
                Email = "adminuser@gmail.com",
                EmailConfirmed = true,
                UserName = "admin user",
                FullName = "Admin User"
            };

            _dbContext.Roles.Add(vendorRole);
            _dbContext.Roles.Add(adminRole);
            _dbContext.UserRoles.Add(new IdentityUserRole<Guid> { UserId = vendor.Id, RoleId = vendorRole.Id });
            _dbContext.UserRoles.Add(new IdentityUserRole<Guid> { UserId = adminUser.Id, RoleId = adminRole.Id });
            _dbContext.Users.Add(vendor);
            _dbContext.Users.Add(adminUser);
            await _dbContext.SaveChangesAsync();

            //normal user
            _userManagerMock.Setup(u => u.FindByIdAsync(vendor.Id.ToString())).ReturnsAsync(vendor);
            _userManagerMock.Setup(u => u.FindByEmailAsync(vendor.Email)).ReturnsAsync(vendor);
            _userManagerMock.Setup(u => u.IsInRoleAsync(vendor, "Vendor")).ReturnsAsync(true);
            _roleManagerMock.Setup(r => r.RoleExistsAsync(It.IsAny<string>())).ReturnsAsync(true);
            _userManagerMock.Setup(u => u.AddToRoleAsync(vendor, vendorRole.Name)).ReturnsAsync(IdentityResult.Success);
            //admin user
            _userManagerMock.Setup(u => u.FindByIdAsync(adminUser.Id.ToString())).ReturnsAsync(adminUser);
            _userManagerMock.Setup(u => u.IsInRoleAsync(adminUser, "Admin")).ReturnsAsync(true);
            _roleManagerMock.Setup(r => r.RoleExistsAsync(It.IsAny<string>())).ReturnsAsync(true);
            _userManagerMock.Setup(u => u.AddToRoleAsync(adminUser, adminRole.Name)).ReturnsAsync(IdentityResult.Success);

            // Act
            var request = new ChangeToVendorCommandRequest { Email = "check@gmail.com" };

            var exception = await Assert.ThrowsAsync<Exception>(async () =>
            {
                await _handler.Handle(request, CancellationToken.None);
            });
            Assert.Equal("User with specified Id does not exist", exception.Message);
        }
    }
}

