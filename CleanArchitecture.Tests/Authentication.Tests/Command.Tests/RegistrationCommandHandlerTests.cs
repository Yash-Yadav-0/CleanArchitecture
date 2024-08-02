using CleanArchitecture.Application.Features.Auth.Commands.EmailConfirmation;
using CleanArchitecture.Application.Features.Auth.Commands.Registration;
using CleanArchitecture.Application.Features.Auth.Exceptions;
using CleanArchitecture.Application.Features.Auth.Rules;
using CleanArchitecture.Application.Helpers;
using CleanArchitecture.Application.Interfaces.AutoMapper;
using CleanArchitecture.Application.Interfaces.Mail;
using CleanArchitecture.Application.Interfaces.Storage;
using CleanArchitecture.Application.Interfaces.UnitOfWorks;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Persistence.Context;
using CleanArchitecture.Persistence.UnitOfWorks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CleanArchitecture.Tests.Authentication.Tests.Command.Tests
{
    public class RegistrationCommandHandlerTests
    {
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private readonly Mock<UserManager<User>> _userManagerMock;
        private readonly Mock<RoleManager<Role>> _roleManagerMock;
        private readonly Mock<AuthRules> _authRulesMock;
        private readonly Mock<IMailService> _mailServiceMock;
        private readonly Mock<ILinkGeneratorHelper> _linkGeneratorHelperMock;
        private readonly Mock<ILocalStorage> _localStorageMock;
        private readonly RegistrationCommandHandler _handler;

        public RegistrationCommandHandlerTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_Registration")
                .Options;

            var dbContext = new ApplicationDbContext(options);

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
            _authRulesMock = new Mock<AuthRules>();
            _mailServiceMock = new Mock<IMailService>();
            _linkGeneratorHelperMock = new Mock<ILinkGeneratorHelper>();
            _localStorageMock = new Mock<ILocalStorage>();

            var httpContext = new DefaultHttpContext();
            httpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, "test-user-id")
            }, "mock"));

            _httpContextAccessorMock.Setup(h => h.HttpContext).Returns(httpContext);


            _handler = new RegistrationCommandHandler(
                new UnitOfWork(dbContext), // Assuming UnitOfWork takes CQRSDbContext
                _mapperMock.Object,
                _httpContextAccessorMock.Object,
                _userManagerMock.Object,
                _roleManagerMock.Object,
                _authRulesMock.Object,
                _mailServiceMock.Object,
                _linkGeneratorHelperMock.Object,
                new EmailConfirmationCommandRequest(),
                _localStorageMock.Object
            );
        }

        [Fact]
        public async Task Handle_SuccessfulRegistration_ShouldCreateUserAndSendEmail()
        {
            // Arrange
            var request = new RegistrationCommandRequest
            {
                FullName = "John Doe",
                Email = "john.doe@example.com",
                Password = "Password123!",
                ConfirmPassword = "Password123!",
                Image = null
            };

            var user = new User { UserName = request.Email };

            // Setup Mapper with optional parameter explicitly
            _mapperMock.Setup(m => m.Map<User, RegistrationCommandRequest>(It.IsAny<RegistrationCommandRequest>(), null))
                   .Returns(user);

            // Setup UserManager
            _userManagerMock.Setup(um => um.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                            .ReturnsAsync(IdentityResult.Success);
            _userManagerMock.Setup(um => um.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>()))
                            .ReturnsAsync(IdentityResult.Success);

            // Setup RoleManager
            _roleManagerMock.Setup(rm => rm.RoleExistsAsync(It.IsAny<string>()))
                            .ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.Equal(Unit.Value, result);
            _userManagerMock.Verify(um => um.CreateAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task Handle_UserAlreadyExists_ShouldThrowUserAlreadyExistsException()
        {
            // Arrange
            var request = new RegistrationCommandRequest
            {
                FullName = "Jane Doe",
                Email = "jane.doe@example.com",
                Password = "Password123!",
                ConfirmPassword = "Password123!"
            };

            // Setup UserManager to find an existing user
            _userManagerMock.Setup(um => um.FindByEmailAsync(It.IsAny<string>()))
                            .ReturnsAsync(new User());

            // Act & Assert
            await Assert.ThrowsAsync<UserAlreadyExistsException>(() => _handler.Handle(request, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_FailedUserCreation_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var request = new RegistrationCommandRequest
            {
                FullName = "Jim Doe",
                Email = "jim.doe@example.com",
                Password = "Password123!",
                ConfirmPassword = "Password123!"
            };

            var user = new User { UserName = request.Email };

            // Setup Mapper with optional parameter explicitly
            _mapperMock.Setup(m => m.Map<User, RegistrationCommandRequest>(It.IsAny<RegistrationCommandRequest>(), null))
                   .Returns(user);

            // Setup UserManager to return failure
            _userManagerMock.Setup(um => um.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                            .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Error" }));

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _handler.Handle(request, CancellationToken.None));
        }
    }
}
