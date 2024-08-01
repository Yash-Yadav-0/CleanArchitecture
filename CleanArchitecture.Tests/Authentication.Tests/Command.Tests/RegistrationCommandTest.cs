using System;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.Features.Auth.Commands.Registration;
using CleanArchitecture.Application.Interfaces.Mail;
using CleanArchitecture.Application.Interfaces.Storage;
using CleanArchitecture.Application.Interfaces.UnitOfWorks;
using CleanArchitecture.Application.Features.Auth.Rules;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;
using CleanArchitecture.Application.Features.Auth.Commands.EmailConfirmation;
using CleanArchitecture.Application.Helpers;
using CleanArchitecture.Application.Interfaces.AutoMapper;

namespace CleanArchitecture.Tests
{
    public class RegistrationCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private readonly Mock<UserManager<User>> _userManagerMock;
        private readonly Mock<RoleManager<Role>> _roleManagerMock;
        private readonly Mock<AuthRules> _authRulesMock;
        private readonly Mock<IMailService> _mailServiceMock;
        private readonly Mock<LinkGeneratorHelper> _linkGeneratorHelperMock;
        private readonly Mock<EmailConfirmationCommandRequest> _emailConfirmationCommandRequestMock;
        private readonly Mock<ILocalStorage> _localStorageMock;
        private readonly RegistrationCommandHandler _handler;

        public RegistrationCommandHandlerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            _userManagerMock = new Mock<UserManager<User>>(new Mock<IUserStore<User>>().Object, null, null, null, null, null, null, null, null);
            _roleManagerMock = new Mock<RoleManager<Role>>(new Mock<IRoleStore<Role>>().Object, null, null, null, null, null, null, null);
            _authRulesMock = new Mock<AuthRules>();
            _mailServiceMock = new Mock<IMailService>();
            _linkGeneratorHelperMock = new Mock<LinkGeneratorHelper>();
            _emailConfirmationCommandRequestMock = new Mock<EmailConfirmationCommandRequest>();
            _localStorageMock = new Mock<ILocalStorage>();

            _handler = new RegistrationCommandHandler(
                _unitOfWorkMock.Object,
                _mapperMock.Object,
                _httpContextAccessorMock.Object,
                _userManagerMock.Object,
                _roleManagerMock.Object,
                _authRulesMock.Object,
                _mailServiceMock.Object,
                _linkGeneratorHelperMock.Object,
                _emailConfirmationCommandRequestMock.Object,
                _localStorageMock.Object
            );
        }

        [Fact]
        public async Task Handle_ShouldRegisterUserSuccessfully()
        {
            // Arrange
            var request = new RegistrationCommandRequest
            {
                Email = "test@example.com",
                Password = "Password123!",
                Image = null // or mock an IFormFile if needed
            };

            var user = new User { UserName = request.Email };
            //_mapperMock.Setup(m => m.Map<User,RegistrationCommandRequest>(request)).Returns(user);
            //_mapperMock.Setup(m => m.Map<User, RegistrationCommandRequest>(request));
            _userManagerMock.Setup(um => um.FindByEmailAsync(request.Email)).ReturnsAsync((User)null);
            _userManagerMock.Setup(um => um.CreateAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
            _roleManagerMock.Setup(rm => rm.RoleExistsAsync("USER")).ReturnsAsync(true);
            _roleManagerMock.Setup(rm => rm.CreateAsync(It.IsAny<Role>())).ReturnsAsync(IdentityResult.Success);
            _userManagerMock.Setup(um => um.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.Equal(Unit.Value, result);
            _userManagerMock.Verify(um => um.CreateAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenUserCreationFails()
        {
            // Arrange
            var request = new RegistrationCommandRequest
            {
                Email = "test@example.com",
                Password = "Password123!",
                Image = null // or mock an IFormFile if needed
            };

            var user = new User { UserName = request.Email };
            //_mapperMock.Setup(m => m.Map<User>(request)).Returns(user);
            _userManagerMock.Setup(um => um.FindByEmailAsync(request.Email)).ReturnsAsync((User)null);
            _userManagerMock.Setup(um => um.CreateAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Error" }));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _handler.Handle(request, CancellationToken.None));
            Assert.Equal("User creation failed", exception.Message);
        }

        // Add more tests to cover other scenarios
    }
}
