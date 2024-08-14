using CleanArchitecture.Application.Features.UserFeature.Commands.ChangeToMember;
using CleanArchitecture.Application.Features.UserFeature.Rules;
using CleanArchitecture.Application.Interfaces.UnitOfWorks;
using CleanArchitecture.Domain.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CleanArchitecture.Application.Tests.Features.UserFeature.Commands
{
    public class ChangeToMemberCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<UserManager<User>> _mockUserManager;
        private readonly Mock<RoleManager<Role>> _mockRoleManager;
        private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;
        private readonly Mock<UserRules> _mockUserRules;
        private readonly ChangeToMemberCommandHandler _handler;

        public ChangeToMemberCommandHandlerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockUserManager = new Mock<UserManager<User>>(
                new Mock<IUserStore<User>>().Object,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null);
            _mockRoleManager = new Mock<RoleManager<Role>>(
                new Mock<IRoleStore<Role>>().Object,
                null,
                null,
                null,
                null);
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _mockUserRules = new Mock<UserRules>();

            _handler = new ChangeToMemberCommandHandler(
                _mockUnitOfWork.Object,
                _mockUserManager.Object,
                _mockRoleManager.Object,
                null, // Mock your mapper if needed
                _mockUserRules.Object,
                _mockHttpContextAccessor.Object);
        }

        [Fact]
        public async Task Handle_ShouldThrowUnauthorizedAccessException_WhenUserIsNotAuthenticated()
        {
            // Arrange
            _mockHttpContextAccessor.Setup(x => x.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier))
                                    .Returns((string)null);

            var request = new ChangeToMemberCommandRequest { Email = "test@example.com" };

            // Act & Assert
            await Assert.ThrowsAsync<UnauthorizedAccessException>(() => _handler.Handle(request, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldThrowUnauthorizedAccessException_WhenUserIsNotAdmin()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _mockHttpContextAccessor.Setup(x => x.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier))
                                    .Returns(userId.ToString());
            _mockUserManager.Setup(x => x.FindByIdAsync(userId.ToString()))
                            .ReturnsAsync(new User { Id = userId });
            _mockUserManager.Setup(x => x.IsInRoleAsync(It.IsAny<User>(), "ADMIN"))
                            .ReturnsAsync(false);

            var request = new ChangeToMemberCommandRequest { Email = "test@example.com" };

            // Act & Assert
            await Assert.ThrowsAsync<UnauthorizedAccessException>(() => _handler.Handle(request, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenUserWithEmailDoesNotExist()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _mockHttpContextAccessor.Setup(x => x.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier))
                                    .Returns(userId.ToString());
            _mockUserManager.Setup(x => x.FindByIdAsync(userId.ToString()))
                            .ReturnsAsync(new User { Id = userId });
            _mockUserManager.Setup(x => x.IsInRoleAsync(It.IsAny<User>(), "ADMIN"))
                            .ReturnsAsync(true);
            _mockUserManager.Setup(x => x.FindByEmailAsync("test@example.com"))
                            .ReturnsAsync((User)null);

            var request = new ChangeToMemberCommandRequest { Email = "test@example.com" };

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _handler.Handle(request, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldChangeRoleToUser_WhenValidRequest()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = new User { Id = userId };
            _mockHttpContextAccessor.Setup(x => x.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier))
                                    .Returns(userId.ToString());
            _mockUserManager.Setup(x => x.FindByIdAsync(userId.ToString()))
                            .ReturnsAsync(new User { Id = userId });
            _mockUserManager.Setup(x => x.IsInRoleAsync(It.IsAny<User>(), "ADMIN"))
                            .ReturnsAsync(true);
            _mockUserManager.Setup(x => x.FindByEmailAsync("test@example.com"))
                            .ReturnsAsync(user);
            _mockRoleManager.Setup(x => x.RoleExistsAsync("USER"))
                            .ReturnsAsync(false);

            // Act
            var result = await _handler.Handle(new ChangeToMemberCommandRequest { Email = "test@example.com" }, CancellationToken.None);

            // Assert
            _mockRoleManager.Verify(x => x.CreateAsync(It.IsAny<Role>()), Times.Once);
            _mockUserManager.Verify(x => x.RemoveFromRoleAsync(user, It.IsAny<string>()), Times.AtLeastOnce);
            _mockUserManager.Verify(x => x.AddToRoleAsync(user, "USER"), Times.Once);
            _mockUserManager.Verify(x => x.UpdateAsync(user), Times.Once);
            result.MessageToReturn.Should().Be("Success");
        }
    }
}
