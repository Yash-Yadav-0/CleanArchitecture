using CleanArchitecture.Application.Bases;
using CleanArchitecture.Application.Features.Auth.Rules;
using CleanArchitecture.Application.Helpers;
using CleanArchitecture.Application.Interfaces.AutoMapper;
using CleanArchitecture.Application.Interfaces.Tokens;
using CleanArchitecture.Application.Interfaces.UnitOfWorks;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CleanArchitecture.Application.Features.Auth.Commands.RefreshToken
{
    public class RefreshTokenCommandHandler : BaseHandler, IRequestHandler<RefreshTokenCommandRequest, RefreshTokenCommandResponse>
    {
        private readonly UserManager<User> userManager;
        private readonly ITokenService tokenService;
        private readonly AuthRules authRules;


        public RefreshTokenCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor, UserManager<User> userManager, ITokenService tokenService, AuthRules authRules)
            : base(unitOfWork, mapper, httpContextAccessor)
        {
            this.userManager = userManager;
            this.tokenService = tokenService;
            this.authRules = authRules;
        }

        public async Task<RefreshTokenCommandResponse> Handle(RefreshTokenCommandRequest request, CancellationToken cancellationToken)
        {
            LoggerHelper.LogInformation("Handling RefreshTokencommand for AccessToken: {AccessToken}",request.AccessToken);
            ClaimsPrincipal? principal = tokenService.GetClaimsPrincipalFromExpiredToken(request.AccessToken);

            if (principal == null)
            {
                LoggerHelper.LogError("Failed to get ClaimsPrincipal from expired token for AccessToken: {AccessToken}", new Exception("Invalid access token"), request.AccessToken);
                throw new Exception("Invalid access token");
            }
            string? email = principal?.FindFirstValue(ClaimTypes.Email);
            if (email == null)
            {
                LoggerHelper.LogError("No email found in ClaimsPrincipal for AccessToken: {AccessToken}", new Exception("Email claim missing"), request.AccessToken);
                throw new Exception("Email claim missing");
            }

            User? user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                LoggerHelper.LogError("User not found for Email: {Email}", new Exception("User not found"), email);
                throw new Exception("User not found");
            }

            IList<string> roles = await userManager.GetRolesAsync(user);

            await authRules.RefreshTokenExpiryTimeShouldnotbeExpiredAsync(user?.RefreshTokenExpiryTime);

            JwtSecurityToken newSecurityToken = await tokenService.CreateToken(user, roles);

            string newRefreshToken = tokenService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;

            await userManager.UpdateAsync(user);

            LoggerHelper.LogInformation("Successfully refreshed token for Email: {Email}", email);

            return new()
            {
                RefreshToken = newRefreshToken,
                AccessToken = new JwtSecurityTokenHandler().WriteToken(newSecurityToken)
            };
        }
    }
}
