using CleanArchitecture.Application.Features.UserFeature.Commands.ChangeToMember;
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Tests.UserFeature.Tests.Command.Tests
{
    //Change Admin / Vendor to User (normal/customer)
    public class ChangeToMemberCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private readonly Mock<UserManager<User>> _userManagerMock;
        private readonly Mock<RoleManager<Role>> _roleManagerMock;
        private readonly Mock<UserRules> _userRulesMock;
        private readonly ChangeToMemberCommandHandler _handler;
        private readonly ApplicationDbContext _dbContext;
        private readonly Guid _userId;

        public ChangeToMemberCommandHandlerTests()
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
                new Claim(ClaimTypes.Role, "Admin") // Set role directly in claims
            }));
            _httpContextAccessorMock.Setup(h => h.HttpContext).Returns(httpContext);

            _handler = new ChangeToMemberCommandHandler(
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
            var role = new Role
            {
                Name = "Admin",
                Id = Guid.NewGuid(),
                NormalizedName = "ADMIN",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };
            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = "test@gmail.com",
                EmailConfirmed = true,
                UserName = "testing admin",
                FullName = "test admin"
            };
            var adminUser = new User
            {
                Id = _userId,
                Email = "mainadmin@gmail.com",
                EmailConfirmed = true,
                UserName = "admin test main",
                FullName = "main Admin"
            };

            _dbContext.Roles.Add(role);
            _dbContext.Users.Add(user);
            _dbContext.UserRoles.Add(new IdentityUserRole<Guid> { UserId = user.Id, RoleId = role.Id });
            _dbContext.Users.Add(adminUser);
            _dbContext.UserRoles.Add(new IdentityUserRole<Guid> { UserId = adminUser.Id, RoleId = role.Id });
            await _dbContext.SaveChangesAsync();
            //normal user
            _userManagerMock.Setup(u => u.FindByIdAsync(user.Id.ToString())).ReturnsAsync(user);
            _userManagerMock.Setup(u => u.FindByEmailAsync(user.Email)).ReturnsAsync(user);
            _userManagerMock.Setup(u => u.IsInRoleAsync(user, "Admin")).ReturnsAsync(true);
            _roleManagerMock.Setup(r => r.RoleExistsAsync(It.IsAny<string>())).ReturnsAsync(true);
            _userManagerMock.Setup(u => u.AddToRoleAsync(user, role.Name)).ReturnsAsync(IdentityResult.Success);
            //admin user
            _userManagerMock.Setup(u => u.FindByIdAsync(adminUser.Id.ToString())).ReturnsAsync(adminUser);
            _userManagerMock.Setup(u => u.IsInRoleAsync(adminUser, "Admin")).ReturnsAsync(true);
            _roleManagerMock.Setup(r => r.RoleExistsAsync(It.IsAny<string>())).ReturnsAsync(true);
            _userManagerMock.Setup(u => u.AddToRoleAsync(adminUser, role.Name)).ReturnsAsync(IdentityResult.Success);

            // Act
            var request = new ChangeToMemberCommandRequest { Email = user.Email };
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.Id.ToString(), result.MessageToReturn);
            _userManagerMock.Verify(u => u.AddToRoleAsync(user, "USER"), Times.Once);
            _userManagerMock.Verify(u => u.UpdateAsync(user), Times.Once);
        }

        [Fact]
        public async Task Handle_UserNotAdmin_ThrowsUnauthorizedAccessException()
        {
            // Arrange
            var email = "testuser@gmail.com";
            var user = new User { Id = Guid.NewGuid(), Email = email };

            _userManagerMock.Setup(u => u.FindByIdAsync(_userId.ToString())).ReturnsAsync(null as User);
            _userManagerMock.Setup(u => u.IsInRoleAsync(It.IsAny<User>(), "ADMIN")).ReturnsAsync(false);

            // Act
            var request = new ChangeToMemberCommandRequest { Email = email };

            // Assert
            await Assert.ThrowsAsync<UnauthorizedAccessException>(() => _handler.Handle(request, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_UserNotFound_ThrowsException()
        {
            // Arrange
            var email = "testuser@gmail.com";
            var adminUser = new User { Id = _userId };

            _userManagerMock.Setup(u => u.FindByIdAsync(_userId.ToString())).ReturnsAsync(adminUser);
            _userManagerMock.Setup(u => u.IsInRoleAsync(adminUser, "ADMIN")).ReturnsAsync(true);
            _userManagerMock.Setup(u => u.FindByEmailAsync(email)).ReturnsAsync(null as User);

            // Act
            var request = new ChangeToMemberCommandRequest { Email = email };

            // Assert
            await Assert.ThrowsAsync<UnauthorizedAccessException>(() => _handler.Handle(request, CancellationToken.None));
        }
    }
}
