using CleanArchitecture.Application.Features.Auth.Commands.Login;
using CleanArchitecture.Application.Features.Auth.Exceptions;
using CleanArchitecture.Application.Features.Auth.Rules;
using CleanArchitecture.Application.Interfaces.AutoMapper;
using CleanArchitecture.Application.Interfaces.Mail;
using CleanArchitecture.Application.Interfaces.Tokens;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Persistence.Context;
using CleanArchitecture.Persistence.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


namespace CleanArchitecture.Tests.Authentication.Tests.Command.Tests
{
    public class LoginCommandHandlerTests
    {
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private readonly Mock<UserManager<User>> _userManagerMock;
        private readonly Mock<ITokenService> _tokenServiceMock;
        private readonly Mock<AuthRules> _authRulesMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly Mock<IMailService> _mailServiceMock;
        private readonly LoginCommandHandler _handler;

        public LoginCommandHandlerTests()
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
            _tokenServiceMock = new Mock<ITokenService>();
            _authRulesMock = new Mock<AuthRules>();
            _configurationMock = new Mock<IConfiguration>();
            _mailServiceMock = new Mock<IMailService>();

            var httpContext = new DefaultHttpContext();
            httpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, "test-user-id")
            }, "mock"));

            _httpContextAccessorMock.Setup(h => h.HttpContext).Returns(httpContext);

            _handler = new LoginCommandHandler(
                _mapperMock.Object,
                new UnitOfWork(dbContext), // Mock UnitOfWork if needed
                _httpContextAccessorMock.Object,
                _userManagerMock.Object,
                _tokenServiceMock.Object,
                _authRulesMock.Object,
                _configurationMock.Object,
                _mailServiceMock.Object
            );
        }

        [Fact]
        public async Task Handle_SuccessfulLogin_ShouldReturnTokenAndRefreshToken()
        {
            // Arrange
            var request = new LoginCommandRequest
            {
                Email = "john.doe@example.com",
                Password = "Password123!"
            };

            var user = new User
            {
                UserName = request.Email,
                FullName = "John Doe",
                RefreshToken = null,
                RefreshTokenExpiryTime = null
            };

            var roles = new List<string> { "User" };
            var securityToken = new JwtSecurityToken(); // Consider initializing with real data
            var tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);
            var refreshToken = "refreshToken";

            _userManagerMock.Setup(um => um.FindByEmailAsync(request.Email)).ReturnsAsync(user);
            _userManagerMock.Setup(um => um.CheckPasswordAsync(user, request.Password)).ReturnsAsync(true);
            _userManagerMock.Setup(um => um.GetRolesAsync(user)).ReturnsAsync(roles);
            _tokenServiceMock.Setup(ts => ts.CreateToken(user, roles)).ReturnsAsync(securityToken);
            _tokenServiceMock.Setup(ts => ts.GenerateRefreshToken()).Returns(refreshToken);
            _configurationMock.Setup(c => c["JWT:RefreshTokenValidityInDays"]).Returns("7");

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(tokenString, result.Token); // Ensure this matches the actual token
            Assert.Equal(refreshToken, result.RefreshToken);
            Assert.Equal(securityToken.ValidTo, result.Expiration);

            _userManagerMock.Verify(um => um.UpdateAsync(It.IsAny<User>()), Times.Once);
            _userManagerMock.Verify(um => um.UpdateSecurityStampAsync(It.IsAny<User>()), Times.Once);
            _userManagerMock.Verify(um => um.SetAuthenticationTokenAsync(It.IsAny<User>(), "Default", "AccessToken", tokenString), Times.Once);
        }

        [Fact]
        public async Task Handle_InvalidPassword_ShouldThrowExceptionAndSendEmail()
        {
            // Arrange
            var request = new LoginCommandRequest
            {
                Email = "john.doe@example.com",
                Password = "WrongPassword!"
            };

            var user = new User
            {
                UserName = request.Email,
                FullName = "John Doe"
            };

            _userManagerMock.Setup(um => um.FindByEmailAsync(request.Email)).ReturnsAsync(user);
            _userManagerMock.Setup(um => um.CheckPasswordAsync(user, request.Password)).ReturnsAsync(false);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(request, CancellationToken.None));
            Assert.Equal("Invalid password", exception.Message);

            _mailServiceMock.Verify(ms => ms.SendMailAsync(
                request.Email,
                "Invalid password attempt",
                $"{user.FullName} 's login attempt in clean architecture: [password wrong]",
                null
            ), Times.Once);
        }
        [Fact]
        public async Task Handle_AuthRulesInvalid_ShouldThrowException()
        {
            // Arrange
            var request = new LoginCommandRequest
            {
                Email = "john.doe@example.com",
                Password = "Password123!"
            };

            var user = new User
            {
                UserName = request.Email,
                FullName = "John Doe"
            };

            var roles = new List<string> { "User" };
            var securityToken = new JwtSecurityToken();
            var refreshToken = "refreshToken";

            _userManagerMock.Setup(um => um.FindByEmailAsync(request.Email)).ReturnsAsync(user);
            _userManagerMock.Setup(um => um.CheckPasswordAsync(user, request.Password)).ReturnsAsync(true);
            _userManagerMock.Setup(um => um.GetRolesAsync(user)).ReturnsAsync(roles);
            _tokenServiceMock.Setup(ts => ts.CreateToken(user, roles)).ReturnsAsync(securityToken);
            _tokenServiceMock.Setup(ts => ts.GenerateRefreshToken()).Returns(refreshToken);
            _configurationMock.Setup(c => c["JWT:RefreshTokenValidityInDays"]).Returns("7");

            _authRulesMock.Setup(ar => ar.EmailOrPasswordShouldnotbeInvalidAsync(user, true))
                .Throws(new EmailOrPasswordShouldnotbeInvalidException("Auth rules validation failed"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<EmailOrPasswordShouldnotbeInvalidException>(() => _handler.Handle(request, CancellationToken.None));
            Assert.Equal("Auth rules validation failed", exception.Message);
        }
    }
}
