using CleanArchitecture.Application.Bases;
using CleanArchitecture.Application.Features.Auth.Rules;
using CleanArchitecture.Application.Interfaces.AutoMapper;
using CleanArchitecture.Application.Interfaces.Tokens;
using CleanArchitecture.Application.Interfaces.UnitOfWorks;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;


namespace CleanArchitecture.Application.Features.Auth.Commands.Login
{
    public class LoginCommandHandler : BaseHandler, IRequestHandler<LoginCommandRequest, LoginCommandResponse>
    {
        private readonly UserManager<User> userManager;
        private readonly ITokenService tokenService;
        private readonly AuthRules AuthRules;
        private readonly IConfiguration configuration;

        public LoginCommandHandler(IMapper mapper,
                                   IUnitOfWork unitOfWork,
                                   IHttpContextAccessor httpContextAccessor,
                                   UserManager<User> userManager,
                                   ITokenService tokenService,
                                   AuthRules AuthRules,
                                   IConfiguration configuration
                                  )

            : base(unitOfWork, mapper, httpContextAccessor)
        {
            this.userManager = userManager;
            this.tokenService = tokenService;
            this.AuthRules = AuthRules;
            this.configuration = configuration;
        }

        public async Task<LoginCommandResponse> Handle(LoginCommandRequest request, CancellationToken cancellationToken)
        {
            User? user = await userManager.FindByEmailAsync(request.Email);
            bool CheckPassword = await userManager.CheckPasswordAsync(user, request.Password);

            await AuthRules.EmailOrPasswordShouldnotbeInvalidAsync(user, CheckPassword);
            IList<string> Roles = await userManager.GetRolesAsync(user);

            JwtSecurityToken securityToken = await tokenService.CreateToken(user, Roles);
            string _RefreshToken = tokenService.GenerateRefreshToken();

            int.TryParse(configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);

            user.RefreshToken = _RefreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays);

            await userManager.UpdateAsync(user);
            await userManager.UpdateSecurityStampAsync(user);

            string Token = new JwtSecurityTokenHandler().WriteToken(securityToken);

            await userManager.SetAuthenticationTokenAsync(user, "Default", "AccessToken", Token);

            return new()
            {
                Token = Token,
                RefreshToken = _RefreshToken,
                Expiration = securityToken.ValidTo
            };
        }
    }
}
